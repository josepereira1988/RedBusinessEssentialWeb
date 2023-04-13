using AutoMapper;
using Entidades.Logadouro;
using Entidades.Usuarios;
using RedBusinessEssentialWeb.Models;

namespace RedBusinessEssentialWeb.ConvertEntityModel
{
    public class EntityToModel : Profile
    {
        public EntityToModel()
        {
            CreateMap<Usuarios, UsuariosCreateModel>().ReverseMap();
            CreateMap<Pais, PaisCreateModel>().ReverseMap();
        }
    }
}
