using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class Categoriarepository : Repository<Categoria>, ICategoriaRepository
    {
        public Categoriarepository(AppDbContext contexto) : base(contexto)
        {
        }
        public async Task <IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return  await Get().Include(c => c.Produtos).ToListAsync();
        }
    }
}
