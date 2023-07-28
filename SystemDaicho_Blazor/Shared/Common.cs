using SystemDaicho_Blazor.Models;

namespace SystemDaicho_Blazor.Shared;

public static class Common
{
    //システム共通情報
    public const string ConnectionJsonName = "data/Connection.json";
    public  static URLs URLs { get; private set; }
    public  static UserAD UserAD { get; private set; }

    public static void SettingInfo(URLs urls ,UserAD userAD)
    {
        URLs = urls;
        UserAD = userAD;
    }

    public static Log CreateLogData(string logtype,string daichotype,string dataid)
    {
        return new()
        {
            userID = UserAD.userID,
            section = UserAD.sectionName,
            dateTime = DateTime.Now.ToLocalTime().ToString(),
            operation = logtype,
            daichotype = daichotype,
            dataID = dataid
        };
    }

}

