using JobApplication.RIApi.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobApplication.Pages
{
    public class DetailsModel(RIJobOfferClient apiClient) : PageModel
    {
        public JobOffer Job { get; set; } = new JobOffer();
        public JobDescription JobDescription { get; set; } = new JobDescription();
        public string JobStoreIds { get; set; } = string.Empty;

        public async Task OnGetAsync(string jobDescriptionId, string jobId)
        {
            var jobOfferWithDescription = await apiClient.GetJobDetails(jobDescriptionId, jobId);
            if (jobOfferWithDescription != null) {
                Job = jobOfferWithDescription.Job;
                JobDescription = jobOfferWithDescription.Description;
                JobStoreIds = string.Join(",", Job.Stores.Select(s => s.Id.ToString()));
            }
        }
    }
}
