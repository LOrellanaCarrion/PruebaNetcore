using AutoMapper;
using Cliente.Api.ViewModel;

namespace Cliente.Api.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente.Api.Modelo.Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<ClienteCreateViewModel, Cliente.Api.Modelo.Cliente > ();
        }
    }
}
