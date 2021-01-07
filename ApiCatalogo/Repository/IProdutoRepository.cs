using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
   public interface IProdutoRepository : IRepositorycs<Produto>
    {
     Task<IEnumerable<Produto>> GetProdutoPorPreco();
       Task< PagedList<Produto>>GetProduto(ProdutosParametrs produtoParameters);
    }
}
