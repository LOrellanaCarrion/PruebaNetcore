using AutoMapper;
using Cuentas.Api.Model;
using Cuentas.Api.ViewModel;

namespace Cuentas.Api.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CuentaCreateViewModel,Cuenta>();
            CreateMap<CuentaUpdateViewModel, Cuenta>();
        }
    }
}
