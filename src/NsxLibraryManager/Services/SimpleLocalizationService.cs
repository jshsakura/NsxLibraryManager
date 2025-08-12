using System.Globalization;

namespace NsxLibraryManager.Services;

public class SimpleLocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations = new()
    {
        ["en"] = new Dictionary<string, string>
        {
            ["Dashboard"] = "Dashboard",
            ["Games"] = "Games",
            ["Library"] = "Library",
            ["Settings"] = "Settings",
            ["Korean"] = "한국어",
            ["English"] = "English",
            ["BundleRenamer"] = "Bundle Renamer",
            ["PackageRenamer"] = "Package Renamer",
            ["CollectionRenamer"] = "Collection Renamer",
            ["Renamer"] = "Renamer",
            ["Configuration"] = "Configuration",
            ["ScanInputFolder"] = "Scan Input Folder",
            ["RenameFiles"] = "Rename Files"
        },
        ["ko"] = new Dictionary<string, string>
        {
            ["Dashboard"] = "대시보드",
            ["Games"] = "게임",
            ["Library"] = "라이브러리",
            ["Settings"] = "설정",
            ["Korean"] = "한국어",
            ["English"] = "English",
            ["BundleRenamer"] = "번들 이름 일괄변경",
            ["PackageRenamer"] = "패키지 이름 일괄변경",
            ["CollectionRenamer"] = "컬렉션 이름 일괄변경",
            ["Renamer"] = "이름 변경",
            ["Configuration"] = "옵션",
            ["ScanInputFolder"] = "폴더 검색",
            ["RenameFiles"] = "파일명 일괄변경"
        }
    };

    public string GetString(string key)
    {
        var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        
        if (_translations.TryGetValue(culture, out var translations) && 
            translations.TryGetValue(key, out var value))
        {
            return value;
        }
        
        // 영어 fallback
        if (_translations["en"].TryGetValue(key, out var englishValue))
        {
            return englishValue;
        }
        
        return key; // 최후의 fallback
    }
}

// IStringLocalizer 대체용 래퍼
public class SimpleStringLocalizer
{
    private readonly SimpleLocalizationService _service;
    
    public SimpleStringLocalizer(SimpleLocalizationService service)
    {
        _service = service;
    }
    
    public string this[string key] => _service.GetString(key);
}