using System.Globalization;

namespace NsxLibraryManager.Services.Interface;

/// <summary>
/// 다국어 지원을 위한 서비스 인터페이스
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// 현재 문화권 정보를 가져옵니다
    /// </summary>
    CultureInfo CurrentCulture { get; }

    /// <summary>
    /// 현재 UI 문화권 정보를 가져옵니다
    /// </summary>
    CultureInfo CurrentUICulture { get; }

    /// <summary>
    /// 지원되는 문화권 목록을 가져옵니다
    /// </summary>
    IEnumerable<CultureInfo> SupportedCultures { get; }

    /// <summary>
    /// 지정된 키에 대한 지역화된 문자열을 가져옵니다
    /// </summary>
    /// <param name="key">리소스 키</param>
    /// <returns>지역화된 문자열</returns>
    string GetString(string key);

    /// <summary>
    /// 지정된 키에 대한 지역화된 문자열을 가져오고 매개변수를 포맷합니다
    /// </summary>
    /// <param name="key">리소스 키</param>
    /// <param name="args">포맷 매개변수</param>
    /// <returns>포맷된 지역화 문자열</returns>
    string GetString(string key, params object[] args);

    /// <summary>
    /// 문화권이 지원되는지 확인합니다
    /// </summary>
    /// <param name="culture">확인할 문화권</param>
    /// <returns>지원 여부</returns>
    bool IsCultureSupported(string culture);

    /// <summary>
    /// 브라우저 언어 설정을 기반으로 최적의 문화권을 선택합니다
    /// </summary>
    /// <param name="acceptLanguageHeader">Accept-Language 헤더</param>
    /// <returns>선택된 문화권</returns>
    CultureInfo SelectBestCulture(string? acceptLanguageHeader);
}