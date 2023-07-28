using System.Net.Http.Json;
using SystemDaicho_Blazor.Models;

namespace SystemDaicho_Blazor.Shared
{
    public partial class MainLayout
    {
        protected override async Task OnInitializedAsync()
        {
            //共通情報の初期化
            URLs url = await Http.GetFromJsonAsync<URLs>(Common.ConnectionJsonName);
            UserID userID = await Http.GetFromJsonAsync<UserID>(url.UserID);
            UserAD userAD = await Http.GetFromJsonAsync<UserAD>(url.UserAD + '/' + userID.userID);
            Common.SettingInfo(url, userAD);
        }
    }
}
