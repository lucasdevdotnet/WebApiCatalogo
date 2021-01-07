using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }
        public  async Task< IEnumerable<Produto>> GetProdutoPorPreco()
        {
            return  await Get().OrderBy(d => d.Preco).ToListAsync();
        }
        public async Task<PagedList<Produto>>GetProduto(ProdutosParametrs produtoParameters)
        {
            //return Get()
            //   .OrderBy(f => f.Nome)
            //   .Skip((produtoParameters.PageNumber - 1) * produtoParameters.PageSize)
            //  .Take(produtoParameters.PageSize)
            //  .ToList();
            return  await PagedList<Produto>.ToPagedList(Get().OrderBy(f => f.CategoriaId),
             produtoParameters.PageNumber,produtoParameters.PageSize);
        }
    }
}
