using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

using NsxLibraryManager.Resources.Localization;
using NsxLibraryManager.Services;
using NsxLibraryManager.Services.Interface;

namespace NsxLibraryManager.Extensions;

/// <summary>
/// 다국어 지원을 위한 확장 메서드
/// </summary>
public static class LocalizationExtensions
{
    /// <summary>
    /// 다국어 지원 서비스를 등록합니다
    /// </summary>
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        // 기본 지역화 서비스 등록
        services.AddLocalization(options => 
        {
            options.ResourcesPath = "Resources/Localization";
        });

        // 커스텀 지역화 서비스 등록
        services.AddScoped<ILocalizationService, LocalizationService>();

        // 요청 지역화 옵션 구성
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ko")
            };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            // 문화권 제공자 우선순위 설정 (안전한 기본 제공자 사용)
            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
            options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
            options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
        });

        return services;
    }

    /// <summary>
    /// 문자열이 지역화 키인지 확인합니다
    /// </summary>
    public static bool IsLocalizationKey(this string value)
    {
        return !string.IsNullOrEmpty(value) && 
               (value.Contains('.') || value.All(char.IsUpper));
    }

    /// <summary>
    /// 안전한 지역화 문자열 가져오기
    /// </summary>
    public static string GetLocalizedStringSafe(this IStringLocalizer localizer, string key, string? fallback = null)
    {
        try
        {
            var result = localizer[key];
            return result.ResourceNotFound ? (fallback ?? key) : result.Value;
        }
        catch
        {
            return fallback ?? key;
        }
    }

    /// <summary>
    /// 문화권별 날짜 형식 가져오기
    /// </summary>
    public static string ToLocalizedDateString(this DateTime dateTime, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return dateTime.ToString("d", culture);
    }

    /// <summary>
    /// 문화권별 시간 형식 가져오기
    /// </summary>
    public static string ToLocalizedTimeString(this DateTime dateTime, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return dateTime.ToString("t", culture);
    }

    /// <summary>
    /// 문화권별 파일 크기 형식 가져오기
    /// </summary>
    public static string ToLocalizedFileSizeString(this long bytes, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        
        string[] sizes = culture.TwoLetterISOLanguageName.ToLower() switch
        {
            "ko" => ["바이트", "KB", "MB", "GB", "TB"],
            _ => ["bytes", "KB", "MB", "GB", "TB"]
        };

        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// 문화권별 숫자 형식 가져오기
    /// </summary>
    public static string ToLocalizedNumberString(this int number, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return number.ToString("N0", culture);
    }

    /// <summary>
    /// 문화권별 숫자 형식 가져오기
    /// </summary>
    public static string ToLocalizedNumberString(this long number, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;
        return number.ToString("N0", culture);
    }
}