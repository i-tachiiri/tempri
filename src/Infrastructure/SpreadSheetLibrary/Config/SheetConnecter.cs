using Newtonsoft.Json.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
namespace SpreadSheetLibrary.Config
{
    public class SheetConnecter
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
        public SheetsService GetSheetsService()
        {
            string scope = SheetsService.Scope.Spreadsheets;
            var credential = GetCredential(scope);
            return new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SheetConnecter" 
            });
        }
    }
}
