using Newtonsoft.Json.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Slides.v1;

namespace GoogleSlideLibrary.Config
{
    public class SlidesConnecter
    {
        private static readonly string jsonPath = Path.Combine(AppContext.BaseDirectory, "client_secret.json");
        private JObject jObject = JObject.Parse(File.ReadAllText(jsonPath));
        private string serviceAccountEmail => jObject["client_email"].ToString();
        private string certificate => jObject["private_key"].ToString();

        public ServiceAccountCredential GetCredential(string Scope)
        {
            return new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = new[] { Scope }
                }.FromPrivateKey(certificate));
        }

        public SlidesService GetSlidesService()
        {
            string scope = SlidesService.Scope.Presentations;
            var credential = GetCredential(scope);
            return new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SlidesConnecter"
            });
        }
    }
}
