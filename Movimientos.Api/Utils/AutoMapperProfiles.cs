using AutoMapper;
using Movimientos.Api.Model;
using Movimientos.Api.ViewModel;

namespace Movimientos.Api.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovimientoViewModel, Movimiento>();
        }
    }
}
