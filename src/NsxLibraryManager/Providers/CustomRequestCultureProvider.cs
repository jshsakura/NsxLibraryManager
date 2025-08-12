using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NsxLibraryManager.Services.Interface;

namespace NsxLibraryManager.Providers;

/// <summary>
/// 커스텀 요청 문화권 제공자
/// 브라우저 설정, 쿠키, 로컬 스토리지 등을 종합적으로 고려하여 최적의 문화권을 선택합니다.
/// </summary>
public class CustomRequestCultureProvider : RequestCultureProvider
{
    private readonly IServiceProvider _serviceProvider;

    public CustomRequestCultureProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var localizationService = scope.ServiceProvider.GetService<ILocalizationService>();
            
            if (localizationService == null)
                return null;

            // 1. 쿠키에서 문화권 확인
            var cookieCulture = GetCultureFromCookie(httpContext);
            if (!string.IsNullOrEmpty(cookieCulture) && localizationService.IsCultureSupported(cookieCulture))
            {
                return new ProviderCultureResult(cookieCulture);
            }

            // 2. Accept-Language 헤더에서 최적 문화권 선택
            var acceptLanguage = httpContext.Request.Headers.AcceptLanguage.FirstOrDefault();
            var bestCulture = localizationService.SelectBestCulture(acceptLanguage);
            
            return new ProviderCultureResult(bestCulture.Name);
        }
        catch (Exception)
        {
            // 오류 발생 시 기본 문화권 반환
            return new ProviderCultureResult("en");
        }
    }

    private static string? GetCultureFromCookie(HttpContext httpContext)
    {
        var cookieValue = httpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
        
        if (string.IsNullOrEmpty(cookieValue))
            return null;

        try
        {
            var culture = CookieRequestCultureProvider.ParseCookieValue(cookieValue);
            return culture?.Cultures.FirstOrDefault()?.Value;
        }
        catch
        {
            return null;
        }
    }
}