using System;
using System.Net.Http;
using System.Threading.Tasks;
using OneApiApp.Models;
using OneApiApp.Services.Interface;

namespace OneApiApp.Services
{
    public class GoogleSearchService : StatisticSearchService, IGoogleSearchService
    {
        private const string _searchEngines = @"https://google.com.au";
        private const string _tagSearch = "//div[contains(@class, 'TbwUpd NJjxre')]";
        public GoogleSearchService(HttpClient httpClient) : base(httpClient, _tagSearch)
        {
        }
        public async Task<SearchResult> Search(string keywords, string tagUrl)
        {
            var returnResult = new SearchResult { Keywords = keywords, Url = tagUrl };
            var searchUrl = $"{_searchEngines}/search?q={keywords}&num=100";
            returnResult.Results = await base.SearchByUrl(searchUrl, tagUrl); ;
            returnResult.Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            returnResult.SearchEngine = _searchEngines;
            return returnResult;
        }
    }
}
