using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneApiApp.Models;
using OneApiApp.Services.Interface;

namespace OneApiApp.Controllers
{
    [Route("")]
    public class StatisticSearchController : Controller
    {
        public static readonly string SearchResult = "SearchResult";
        private readonly ILogger<StatisticSearchController> _logger;
        private readonly IGoogleSearchService _googleSearchService;
        private readonly IBingSearchService _bingSearchService;
        private readonly IMemoryCache _memoryCache;

        public StatisticSearchController(ILogger<StatisticSearchController> logger,
                                         IMemoryCache memoryCache,
                                         IGoogleSearchService googleSearchService,
                                         IBingSearchService bingSearchService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _googleSearchService = googleSearchService;
            _bingSearchService = bingSearchService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation(1, $"Getting Statistic");

            return View("Index");
        }

        [HttpGet]
        [Route("/{keywords}/{tagurl}")]
        public async Task<IActionResult> Get(string keywords, string tagurl)
        {
            _logger.LogInformation(1, $"Getting Statistic");
            var response = new List<SearchResult>();
            try
            {
                if (_memoryCache.TryGetValue(SearchResult, out List<SearchResult> cacheEntry))
                {
                    response = cacheEntry;
                }
                else
                {
                    response.Add(await _googleSearchService.Search(keywords, tagurl));
                    response.Add(await _bingSearchService.Search(keywords, tagurl));
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                    _memoryCache.Set(SearchResult, response, cacheEntryOptions);
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

