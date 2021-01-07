using ApiCatalogo.Context;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private ProdutoRepository _produtoRepo;
        private Categoriarepository _categoriaRepo;
        public AppDbContext _contex;

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_contex);
            }
        }

        public ICategoriaRepository CategoriaRepository 
        {
            get 
            {
                return _categoriaRepo = _categoriaRepo ?? new Categoriarepository(_contex);
            }
        }

        public UnitOfWork(AppDbContext contexto)
        {
            _contex = contexto;

        }

        public async Task Commit()
        {
            await _contex. SaveChangesAsync();
        }
        public void Dispose()
        {
            _contex.Dispose();
        }
    }
}
