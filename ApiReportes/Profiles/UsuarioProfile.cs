using AutoMapper;
using Reportes.Entity;

namespace ApiReportes.Profiles
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Login));
        }
    }
}
