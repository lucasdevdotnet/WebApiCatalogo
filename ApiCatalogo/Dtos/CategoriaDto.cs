using ApiCatalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Dtos
{
    public class CategoriaDto
    {
        public CategoriaDto()
        {

        }
 
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public ICollection<ProdutoDto> Produtos { get; set; }
    }
}
