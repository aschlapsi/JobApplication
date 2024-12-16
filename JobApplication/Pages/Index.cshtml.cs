using JobApplication.RIApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobApplication.Pages
{
    public class IndexModel(RIJobOfferClient apiClient) : PageModel
    {
        public JobOfferSearchResponse? JobOfferSearchResponse { get; set; }
        public JobOfferFiltersResponse JobOfferFiltersResponse { get; set; } = new JobOfferFiltersResponse();

        [BindProperty]
        public string SearchTerm { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task OnPostSearchAsync()
        {
            var searchFilter = new JobOfferSearchFilter(SearchTerm);
            JobOfferSearchResponse = await apiClient.SearchJobAsync(searchFilter);
        }
    }
}
