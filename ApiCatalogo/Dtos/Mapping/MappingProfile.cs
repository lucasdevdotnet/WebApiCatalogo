using ApiCatalogo.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Dtos.Mapping
{
    public class MappingProfile   : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

        }
    }
}
