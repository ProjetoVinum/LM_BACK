using LivroMente.API.Integration.Interfaces;
using LivroMente.API.Integration.Refit;
using LivroMente.API.Integration.Response;

namespace LivroMente.API.Integration
{
    public class ViaCepIntegration : IViaCepIntegration
    {
        private readonly IViaCepIntegrationRefit _viaCepIntegrationRefit;
        public ViaCepIntegration(IViaCepIntegrationRefit viaCepIntegrationRefit)
        {
            _viaCepIntegrationRefit = viaCepIntegrationRefit;
        } 
        public async Task<ViaCepResponse> ObterDadosViaCep(string cep)
        {
          var responseData = await _viaCepIntegrationRefit.ObterDadosViaCep(cep);
          if (responseData != null && responseData.IsSuccessStatusCode)
          {
            return responseData.Content;
          }
          return null;
        }

    }
}