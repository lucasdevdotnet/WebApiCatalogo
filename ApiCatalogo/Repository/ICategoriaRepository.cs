using ApiCatalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepositorycs<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
