using ApiCatalogo.Dtos;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Produces("application/json")]         //a aplicar os padrão para formato json 
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitofWork _uof;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitofWork context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _uof = context;
            _configuration = configuration;

        }
        [HttpGet("autor")]
        public string GetAutor()
        {
            var autor = _configuration["autor"];
            var conexao = _configuration["ConnectionStrings: DefaultConnection"];
            return $"Autor : {autor} Conexao: {conexao}";
        }
        /// <summary>
        /// obtem categorias e produtos 
        /// </summary>
        /// <returns></returns>
        ///<remarks> retorna um objeto categoria incluido.</remark>remarks>
        [HttpGet("/produtos")]
        // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterCategoriasProdutos()
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetCategoriasProdutos();
                var categoriaDto = _mapper.Map<List<CategoriaDto>>(categoria);
                return categoriaDto;
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentat obter os dados");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            var categoria = await _uof.CategoriaRepository.Get().ToListAsync();
            var categoriaDto = _mapper.Map<List<CategoriaDto>>(categoria);
            return categoriaDto;
        }
        /// <summary>
        ///obtem categoria por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        // aqui define na documentação do swagger os retornos
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetById(d => d.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"A categoria com id={id} não foi encontrada");
                }
                var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
                return Ok(categoriaDto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentat obter os dados");
            }

        }

        [HttpPut("{id}")]
        // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutCategoria(int id, CategoriaDto categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest($"Não foi possivel atualizar a categoria com id={id}");
            }
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            _uof.CategoriaRepository.Update(categoria);
            await _uof.Commit();
            return Ok(categoriaDto);
        }
        /// <summary>
        /// Incluir nova categoria
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        ///<remarks> retorna um objeto categoria incluido.</remark>remarks>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Categoria>> PostCategoria(CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uof.CategoriaRepository.Add(categoria);
                await _uof.Commit();

                return CreatedAtAction("GetCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error ao tentatcriar uma nova categoria");
            }
        }
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),            // aqui define na documentação do swagger os retornos 200, 400 500  etcs 
            nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<CategoriaDto>> DeleteCategoria(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetById(d => d.CategoriaId == id);
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            if (categoria == null)
            {
                return NotFound();
            }

            _uof.CategoriaRepository.Delete(categoria);
            await _uof.Commit();
            return categoriaDto;
        }


    }
}
