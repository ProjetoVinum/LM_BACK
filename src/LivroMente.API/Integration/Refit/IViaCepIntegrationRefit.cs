using LivroMente.API.Integration.Response;
using Refit;

namespace LivroMente.API.Integration.Refit
{
    public interface IViaCepIntegrationRefit
    {
        [Get("/ws/{cep}/json")]
         Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
    }
}