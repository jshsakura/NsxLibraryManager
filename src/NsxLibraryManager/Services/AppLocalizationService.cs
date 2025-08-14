using System.Globalization;

namespace NsxLibraryManager.Services;

public class AppLocalizationService
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
            ["RenameFiles"] = "Rename Files",
            ["TitleDb"] = "Title DB",
            ["FileOrganizer"] = "File Organizer",
            ["LibraryTitles"] = "Library Titles",
            ["MissingUpdates"] = "Missing Updates",
            ["MissingDlc"] = "Missing DLC",
            ["MissingDlcUpdates"] = "Missing DLC Updates",
            ["Collections"] = "Collections",
            ["FtpStatus"] = "FTP Status",
            ["TaskStatus"] = "Task Status",
            ["DuplicateTitles"] = "Duplicate Titles",
            ["Language"] = "Language",
            ["List"] = "List",
            ["Card"] = "Card",
            ["SaveConfiguration"] = "Save Configuration",
            ["DatabasePath"] = "Database Path",
            ["ExportUserData"] = "Export UserData",
            ["ImportUserData"] = "Import UserData",
            ["UITheme"] = "UI Theme",
            ["Misc"] = "Misc"
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
            ["RenameFiles"] = "파일명 일괄변경",
            ["TitleDb"] = "타이틀 DB",
            ["FileOrganizer"] = "파일 정리",
            ["LibraryTitles"] = "라이브러리 게임 목록",
            ["MissingUpdates"] = "업데이트 누락 목록",
            ["MissingDlc"] = "DLC 누락 목록",
            ["MissingDlcUpdates"] = "DLC 업데이트 누락 목록",
            ["Collections"] = "컬렉션",
            ["FtpStatus"] = "FTP 상태",
            ["TaskStatus"] = "작업 상태",
            ["DuplicateTitles"] = "중복 게임 목록",
            ["Language"] = "언어",
            ["List"] = "목록",
            ["Card"] = "카드",
            ["SaveConfiguration"] = "설정 저장",
            ["DatabasePath"] = "데이터베이스 경로",
            ["ExportUserData"] = "사용자 데이터 내보내기",
            ["ImportUserData"] = "사용자 데이터 가져오기",
            ["UITheme"] = "UI 테마",
            ["Misc"] = "기타"
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
public class AppLocalizer
{
    private readonly AppLocalizationService _service;
    
    public AppLocalizer(AppLocalizationService service)
    {
        _service = service;
    }
    
    public string this[string key] => _service.GetString(key);
}