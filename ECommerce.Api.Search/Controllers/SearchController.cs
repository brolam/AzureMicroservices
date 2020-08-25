using System;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm trem)
        {
            var result = await searchService.SearchAsync(trem.CustomerId);
            if ( result.Issucess){
                return Ok(result.SearchResults);
            }
            return NotFound();
            
        }
    }
}
