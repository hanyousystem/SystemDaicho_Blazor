﻿using System.Net.Http.Json;
using SystemDaicho_Blazor.Models;
using Microsoft.JSInterop;
using SystemDaicho_Blazor.Shared;
using Microsoft.AspNetCore.Components;

namespace SystemDaicho_Blazor.Pages
{
    public partial class GaiseiSystem
    {
        private List<Gaisei> CurrentData= new();
        private List<Gaisei> CurrentDataWithoutFilter; // フィルター前のカレントデータ
                                                       //双方向バインドフィルター用
        private string systemidfilter = ""; //ID
        private string shukankashitsufilter = ""; //課室名

        private const string EditURL = "system_Gaisei/Edit/";

        private string GetUpdateURL(string id)
        {
            return EditURL + id;
        }


        protected override async Task OnInitializedAsync()
        {
            //初期化
            await RefleshDataAsync();
        }

        private async Task<List<Gaisei>> GetCurrentDataAsync()
        {
            return (await Http.GetFromJsonAsync<Gaisei[]>(Common.URLs.API_Gaisei)).ToList();
        }

        private async Task RefleshDataAsync()
        {
            CurrentData = await GetCurrentDataAsync();
            CurrentDataWithoutFilter = CurrentData;
        }

        private void Filterfn()
        {
            CurrentData = CurrentDataWithoutFilter;
            if (string.IsNullOrWhiteSpace(systemidfilter) == false)
            {
                CurrentData = CurrentData.Where(data => data.id.ToLower().Contains(systemidfilter.Trim().ToLower())).ToList();
            }
            if (string.IsNullOrWhiteSpace(shukankashitsufilter) == false)
            {
                CurrentData = CurrentData.Where(data => data.shukanKashitsu.ToLower().Contains(shukankashitsufilter.Trim().ToLower())).ToList();
            }

        }

        private async Task DeleteClickAsync(Gaisei data)
        {
            if (data.shukanKashitsu != Common.UserAD.sectionName)
            {
                await JS.InvokeVoidAsync("alert", $"所属課室のIDを指定してください。");
                return;
            }

            if (await JS.InvokeAsync<bool>("confirm", $"{data.id}を削除しますか？"))
            {
                data.isDelete = true;
                await Http.PutAsJsonAsync(Common.URLs.API_Gaisei + $"/{data.id}", data);
                await Http.PostAsJsonAsync(Common.URLs.Log, Common.CreateLogData("削除", Common.DaichoType.Gaisei.ToString(), data.id));
                await RefleshDataAsync();
            }
        }

        [Inject]
        protected NavigationManager NavManager { get; set; }
        protected void CallFormEdit(string url)
        {
            NavManager.NavigateTo(url);
        }
    }
}
