
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using SystemDaicho_Blazor.Models;
using SystemDaicho_Blazor.Shared;

namespace SystemDaicho_Blazor.Pages
{
    public partial class NaiseiSystem_Edit
    {
        private Naisei CurrentData;
        private bool AddMode;

        [Parameter]
        public string Paramid { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            //初期化
            //URLのidに値がある場合は編集モード、ない場合は追加モードとする
            if (Paramid == null)
            {
                AddMode = true;
                //追加モードは新しいモデルを作成
                CurrentData = new()
                {
                    shukanKashitsu = Common.UserAD.sectionName
                };
            }
            else
            {
                AddMode = false;
                // 編集モードは最新のデータを取得
                CurrentData = await Http.GetFromJsonAsync<Naisei>(Common.URLs.API_Naisei + $"/{Paramid}"); 
            }
        }
 
        private async Task UpdateDataAsync()
        {
            await Http.PutAsJsonAsync(Common.URLs.API_Naisei + $"/{CurrentData.id}", CurrentData);
            await Http.PostAsJsonAsync(Common.URLs.Log, Common.CreateLogData("更新", "1", CurrentData.id));
            await JS.InvokeVoidAsync("alert", $"{CurrentData.id}を更新しました。");
            ReturnClick();
        }
        private async Task AddDataAsync()
        {
            CurrentData.id = (await Http.GetFromJsonAsync<MaxID>(Common.URLs.API_Naisei + $"/nextID")).id;//新規ID取得
            await Http.PostAsJsonAsync(Common.URLs.API_Naisei, CurrentData);
            await Http.PostAsJsonAsync(Common.URLs.Log, Common.CreateLogData("挿入","1",CurrentData.id));
            await JS.InvokeVoidAsync("alert", $"追加しました。");
            ReturnClick();
        }

        [Inject]
        protected NavigationManager NavManager { get; set; }
        protected void ReturnClick()
        {
            NavManager.NavigateTo("system_Naisei");
        }
    }
}
