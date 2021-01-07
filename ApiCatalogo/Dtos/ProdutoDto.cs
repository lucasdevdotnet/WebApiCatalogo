
namespace ApiCatalogo.Dtos
{
    public class ProdutoDto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string ImageUrl { get; set; }
        public int CatagoriaId { get; set; }
    }
}
