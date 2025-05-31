using Newtonsoft.Json.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Docs.v1;

namespace DocumentLibrary.Config
{
    public class DocumentConnector
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

        public DocsService GetDocsService()
        {
            string scope = DocsService.Scope.Documents;
            var credential = GetCredential(scope);
            return new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DocsConnecter"
            });
        }
    }
}
