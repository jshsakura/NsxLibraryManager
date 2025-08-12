using System.Globalization;
using Microsoft.Extensions.Localization;
using NsxLibraryManager.Resources.Localization;
using NsxLibraryManager.Services.Interface;

namespace NsxLibraryManager.Services;

/// <summary>
/// 다국어 지원 서비스 구현
/// </summary>
public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly ILogger<LocalizationService> _logger;

    private static readonly CultureInfo[] _supportedCultures = 
    {
        new("en"),
        new("ko")
    };

    public LocalizationService(
        IStringLocalizer<SharedResources> localizer,
        ILogger<LocalizationService> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

    public CultureInfo CurrentUICulture => CultureInfo.CurrentUICulture;

    public IEnumerable<CultureInfo> SupportedCultures => _supportedCultures;

    public string GetString(string key)
    {
        try
        {
            var localizedString = _localizer[key];
            
            if (localizedString.ResourceNotFound)
            {
                _logger.LogWarning("Localization key '{Key}' not found for culture '{Culture}'", 
                    key, CurrentUICulture.Name);
                return key; // 키를 그대로 반환 (fallback)
            }

            return localizedString.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting localized string for key '{Key}'", key);
            return key; // 오류 시 키를 그대로 반환
        }
    }

    public string GetString(string key, params object[] args)
    {
        try
        {
            var format = GetString(key);
            return string.Format(format, args);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error formatting localized string for key '{Key}' with args: {Args}", 
                key, string.Join(", ", args));
            return key;
        }
    }

    public bool IsCultureSupported(string culture)
    {
        if (string.IsNullOrWhiteSpace(culture))
            return false;

        return _supportedCultures.Any(c => 
            c.Name.Equals(culture, StringComparison.OrdinalIgnoreCase) ||
            c.TwoLetterISOLanguageName.Equals(culture, StringComparison.OrdinalIgnoreCase));
    }

    public CultureInfo SelectBestCulture(string? acceptLanguageHeader)
    {
        if (string.IsNullOrWhiteSpace(acceptLanguageHeader))
            return _supportedCultures[0]; // 기본값: 영어

        try
        {
            // Accept-Language 헤더 파싱 (예: "ko-KR,ko;q=0.9,en;q=0.8")
            var languages = acceptLanguageHeader
                .Split(',')
                .Select(lang => lang.Split(';')[0].Trim())
                .ToList();

            // 정확한 문화권 매칭 시도
            foreach (var language in languages)
            {
                var matchingCulture = _supportedCultures
                    .FirstOrDefault(c => c.Name.Equals(language, StringComparison.OrdinalIgnoreCase));
                
                if (matchingCulture != null)
                    return matchingCulture;
            }

            // 언어 코드만으로 매칭 시도 (예: "ko-KR" -> "ko")
            foreach (var language in languages)
            {
                var languageCode = language.Split('-')[0];
                var matchingCulture = _supportedCultures
                    .FirstOrDefault(c => c.TwoLetterISOLanguageName.Equals(languageCode, StringComparison.OrdinalIgnoreCase));
                
                if (matchingCulture != null)
                    return matchingCulture;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error parsing Accept-Language header: {Header}", acceptLanguageHeader);
        }

        return _supportedCultures[0]; // 기본값: 영어
    }
}