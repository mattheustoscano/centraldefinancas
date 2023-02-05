using System.Text;

namespace CentralDeFinancas.Presentation.Mvc.Services
{
    public class IntegrationService
    {
        private readonly string? _apiUrl;

        public IntegrationService(string? apiUrl)
        {
            this._apiUrl = apiUrl;
        }

        public async Task<string> PostAsync(string endpoint, StringContent content)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.PostAsync(this._apiUrl + endpoint, content);
                
                var builder = new StringBuilder();
                using (var response = result.Content)
                {
                    Task<string> task = response.ReadAsStringAsync();

                    builder.Append(task.Result);
                }
            
                var jsonResult = builder.ToString();

                //verificando se a resposta é sucesso.
                if (result.IsSuccessStatusCode)
                {
                    return jsonResult;
                }
                else
                {
                    throw new Exception(jsonResult);
                }

            }
        }

    }
}
