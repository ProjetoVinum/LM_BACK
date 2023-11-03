using LivroMente.API.Integration.Response;

namespace LivroMente.API.Integration.Interfaces
{
    public interface IViaCepIntegration
    {
       Task<ViaCepResponse> ObterDadosViaCep(string cep);  
    }
}