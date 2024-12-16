using JobApplication.RIApi.JobApplications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobApplication.Pages
{
    public record JobId(string Id, string StoreIds);

    public static class SessionExtensions
    {
        public static void SetCredentials(this ISession session, JobApplicationCredentials credentials)
        {
            session.SetInt32("ApplicationId", credentials!.Id);
            session.SetString("AuthCode", credentials.AuthCode);
            session.SetString("ExpirationTimestamp", credentials.ExpirationTimestamp);
        }

        public static void SetJobId(this ISession session, JobId jobId)
        {
            session.SetString("JobId", jobId.Id);
            session.SetString("StoreIds", jobId.StoreIds);
        }

        public static JobApplicationCredentials GetCredentials(this ISession session)
        {
            return new JobApplicationCredentials
            {
                Id = session.GetInt32("ApplicationId") ?? -1,
                AuthCode = session.GetString("AuthCode") ?? string.Empty,
                ExpirationTimestamp = session.GetString("ExpirationTimestamp") ?? string.Empty
            };
        }

        public static JobId GetJobId(this ISession session)
        {
            return new JobId(
                session.GetString("JobId") ?? string.Empty,
                session.GetString("StoreIds") ?? string.Empty
            );
        }
    }

    public class ApplyModel(RIJobApplicationsClient apiClient) : PageModel
    {
        public const int ApplicantStep = 1;
        public const int DocumentsStep = 2;
        public const int FinishApplicationStep = 3;
        public const int FinishedApplicationStep = 4;

        public int Step { get; set; } = 1;
        [BindProperty]
        public RIApi.JobApplications.JobApplication Application { get; set; } = new RIApi.JobApplications.JobApplication();
        public BadRequest? ValidationMessages { get; set; }

        // Step 1
        [BindProperty]
        public JobApplicant Applicant { get; set; } = new JobApplicant();
        public List<SelectListItem> GenderTypes { get; } = new List<SelectListItem>() {
            new SelectListItem { Text="Männlich", Value="Male" },
            new SelectListItem { Text="Weiblich", Value="Female" },
            new SelectListItem { Text="Drittes Geschlecht", Value="ThirdGender" },
            new SelectListItem { Text="Anonym", Value="Anonymous" },
            new SelectListItem { Text="Nicht angegeben", Value="NotSpecified" },
        };

        // Step 2
        public JobDocument[] Documents { get; set; } = [];
        [BindProperty]
        public IFormFile? Upload { get; set; }
        [BindProperty]
        public string SelectedDocumentType { get; set; } = string.Empty;
        public List<SelectListItem> DocumentTypes { get; } = new List<SelectListItem>() {
            new SelectListItem { Text="Lebenslauf", Value="Cv" },
            new SelectListItem { Text="Motivationsschreiben", Value="MotivationalLetter" },
            new SelectListItem { Text="Foto", Value="Foto" },
            new SelectListItem { Text="Sonstiges", Value="Misc" },
            new SelectListItem { Text="Zeugnis", Value="GradeSheet" },
        };

        // Step 3
        [BindProperty]
        public JobApplicationDetails JobApplicationDetails { get; set; } = new JobApplicationDetails();

        public async Task OnGetAsync(string jobId, string storeIds)
        {
            var credentials = await apiClient.GetCredentials("key", "answer") ?? new JobApplicationCredentials();
            HttpContext.Session.SetCredentials(credentials);
            HttpContext.Session.SetJobId(new JobId(jobId, storeIds));
            SetStep(ApplicantStep);
            await RefreshApplication(credentials);
        }

        public async Task OnPostApplicantAsync()
        {
            var credentials = HttpContext.Session.GetCredentials();
            var response = await apiClient.PutApplicant(credentials, Applicant);
            if (!response.Succeeded)
            {
                ValidationMessages = response.BadRequest;
                SetStep(ApplicantStep);
            }
            else
            {
                SetStep(DocumentsStep);
            }
            await RefreshApplication(credentials);
        }

        public async Task OnPostUploadAsync()
        {
            var credentials = HttpContext.Session.GetCredentials();
            using var fileStream = new MemoryStream();
            await Upload!.CopyToAsync(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);
            var response = await apiClient.PostDocument(credentials, new JobApplicationDocumentRequest
            {
                DocumentType = SelectedDocumentType,
                DocumentName = Upload.FileName,
                DocumentBlob = Convert.ToBase64String(fileStream.ToArray())
            });
            if (!response.Succeeded)
                ValidationMessages = response.BadRequest;
            SetStep(DocumentsStep);
            await RefreshApplication(credentials);
        }

        public async Task OnPostDeleteDocumentAsync(int documentId)
        {
            var credentials = HttpContext.Session.GetCredentials();
            await apiClient.DeleteDocument(credentials, documentId);
            SetStep(DocumentsStep);
            await RefreshApplication(credentials);
        }

        public async Task OnPostFinishDocumentsAsync()
        {
            var credentials = HttpContext.Session.GetCredentials();
            SetStep(FinishApplicationStep);
            await RefreshApplication(credentials);
        }

        public async Task OnPostFinishApplicationAsync()
        {
            var credentials = HttpContext.Session.GetCredentials();
            var jobId = HttpContext.Session.GetJobId();
            JobApplicationDetails.JobId = jobId.Id;
            JobApplicationDetails.StoreIdList = jobId.StoreIds.Split(',').Select(int.Parse).ToArray();
            await apiClient.Submit(credentials, JobApplicationDetails);
            SetStep(FinishedApplicationStep);
        }

        private async Task RefreshApplication(JobApplicationCredentials credentials)
        {
            Application = await apiClient.GetApplication(credentials) ?? new RIApi.JobApplications.JobApplication();
            Applicant = Application.Applicant;
            Documents = Application.Documents;
        }

        private void SetStep(int nextStep)
        {
            Step = nextStep;
        }
    }
}
