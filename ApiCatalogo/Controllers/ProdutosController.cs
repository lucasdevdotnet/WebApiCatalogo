using ApiCatalogo.Dtos;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]            // aqui define na documentação do swagger os retornos 200, 400 500  et
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitofWork _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitofWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;

        }
        [HttpGet("menorpreco")]
        public  async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosPrecos()
        {
            var produtos = await  _uof.ProdutoRepository.GetProdutoPorPreco();
            var produtosDto = _mapper.Map<List<ProdutoDto>>(produtos);
            return produtosDto;
        }

        // GET: api/Produtos
        [HttpGet]
        //  [ServiceFilter(typeof(ApiLoggingFilter))]     log de erros 
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos([FromQuery]ProdutosParametrs produtoParamets)
        {
            // throw new Exception("Erro.......");
            var produto =  await _uof.ProdutoRepository.GetProduto(produtoParamets);
            var metadata = new
            {
                produto.TotalCount,
                produto.PageSize,
                produto.CurrrentPage,
                produto.TotalPages,
                produto.HastNext,
                produto.HastPrevious,
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var produtoDto = _mapper.Map<List<ProdutoDto>>(produto);
            return produtoDto;
        }


        [HttpGet("{id}/{param2?}")]
        //  [ServiceFilter(typeof(ApiLoggingFilter))]     log de erros 
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Get))]
        //https://localhost:44372/api/produtos?PageNumber=1&_pageSize=1
        public  async Task<ActionResult<ProdutoDto>> ObterProduto(int id, string param2)
        {
            var meuParametro = param2;
            var produto = await  _uof.ProdutoRepository.GetById(d => d.ProdutoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            var produtoDto = _mapper.Map<ProdutoDto>(produto);
            return produtoDto;
        }

        // PUT: api/Produtos/5
        [HttpPut("{id}", Name = "ObterProduto")]
        //  [ServiceFilter(typeof(ApiLoggingFilter))]     log de erros 
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Put))]
        public async Task< ActionResult> Alterar(int id, [FromBody]ProdutoDto produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();

            }
            var produto = _mapper.Map<Produto>(produtoDto);
            _uof.ProdutoRepository.Update(produto);
          await   _uof.Commit();
            return Ok(produtoDto);

        }

        [HttpPost]
        //  [ServiceFilter(typeof(ApiLoggingFilter))]     log de erros 
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult>Post([FromBody]ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _uof.ProdutoRepository.Add(produto);
            await _uof.Commit();
            var produtoDTO = _mapper.Map<ProdutoDto>(produto);
            return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produtoDTO);
        }
        [HttpDelete("{id}")]
        //  [ServiceFilter(typeof(ApiLoggingFilter))]     log de erros 
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<ProdutoDto>> DeleteProduto(int id)
        {
            var produto = await _uof.ProdutoRepository.GetById(d => d.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }
             _uof.ProdutoRepository.Delete(produto);
            await  _uof.Commit();
            var produtoDto = _mapper.Map<ProdutoDto>(produto);
            return produtoDto;
        }
    }
}
