using ApiCatalogo.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Models
{
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [PrimeiraLetraMaiusculaAtribute]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 1000, ErrorMessage = "o preço deve esta entre {1} e {2}")]
        public decimal Preco { get; set; }
        public string ImageUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Nome))
            {

                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra do nome não pode ser maiúscula",
                        new[]
                        {
                            nameof(this.Nome) }
                       );

                }
            }
            if (this.Estoque <= 0)
            {
                yield return new ValidationResult("O Estoque deve ser maior que zero",
                      new[]
                      {
                            nameof(this.Nome) }
                     );

            }
        }
    }
}
