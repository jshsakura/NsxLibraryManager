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
            ["Misc"] = "Misc",
            ["GamesByCategory"] = "Games by Category",
            ["ContentDistribution"] = "Content Distribution",
            ["PackageTypeDistribution"] = "Package Type Distribution",
            ["TopPublishers"] = "Top Publishers",
            ["Content"] = "Content",
            ["ReleaseDate"] = "Release Date",
            ["NumberOfPlayers"] = "Number of Players",
            ["Genre"] = "Genre",
            ["Publisher"] = "Publisher",
            ["ESRBRating"] = "ESRB Rating",
            ["FileSize"] = "File Size",
            ["Languages"] = "Languages",
            ["Type"] = "Type",
            ["InLibrary"] = "In Library",
            ["ReadMore"] = "Read More",
            ["ReadLess"] = "Read Less",
            ["Name"] = "Name",
            ["FileName"] = "File Name",
            ["Size"] = "Size",
            ["Date"] = "Date",
            ["Version"] = "Version",
            ["Owned"] = "Owned",
            ["Updates"] = "Updates",
            ["ShortVersion"] = "Short Version"
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
            ["Misc"] = "기타",
            ["GamesByCategory"] = "카테고리별 게임",
            ["ContentDistribution"] = "콘텐츠 분포",
            ["PackageTypeDistribution"] = "패키지 유형 분포",
            ["TopPublishers"] = "주요 퍼블리셔",
            ["Content"] = "콘텐츠",
            ["ReleaseDate"] = "출시일",
            ["NumberOfPlayers"] = "플레이어 수",
            ["Genre"] = "장르",
            ["Publisher"] = "퍼블리셔",
            ["ESRBRating"] = "ESRB 등급",
            ["FileSize"] = "파일 크기",
            ["Languages"] = "언어",
            ["Type"] = "유형",
            ["InLibrary"] = "라이브러리에 있음",
            ["ReadMore"] = "더 보기",
            ["ReadLess"] = "간략히",
            ["Name"] = "이름",
            ["FileName"] = "파일명",
            ["Size"] = "크기",
            ["Date"] = "날짜",
            ["Version"] = "버전",
            ["Owned"] = "보유",
            ["Updates"] = "업데이트",
            ["ShortVersion"] = "짧은 버전"
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