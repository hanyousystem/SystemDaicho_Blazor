using System.Text.Json;
using System.Text.Unicode;
using SystemDaicho_Blazor.Models;
using System.Text;

namespace SystemDaicho_Blazor.Shared;

public static class Common
{
    //システム共通情報
    public const string ConnectionJsonName = "data/Connection.json";

    public  static URLs URLs { get; private set; }
    public  static UserAD UserAD { get; private set; }

    public enum DaichoType : byte 
    {
        Naisei=1,
        Gaisei=2
    }

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

    //Web Assemblyはサンドボックス外の基盤となるハードウェア、オペレーティングシステム、またはファイルシステムにアクセスできません。
    //そのため、クライアントの外部ファイルは参照できません。
    //(サーバーへの http 呼び出しを介して wwwroot フォルダー内の静的ファイルは取得できます。)
    //public static async Task ReadJson()
    //{

    //    string  jsonStr = await ReadAllLineAsync(ConnectionJsonName, "utf - 8");

    //    URLs = new ();
    //    URLs = JsonSerializer.Deserialize<URLs>(jsonStr);

    //}
   
    //public static async Task<string> ReadAllLineAsync(string filePath, string encodingName)
    //{
    //    return await new StreamReader(filePath).ReadToEndAsync();
    //}
}

