using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/cines")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class CinesController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CineDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Cines.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var cines = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<CineDTO>>(cines);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CineDTO>> Get(int Id)
        {
            var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == Id);

            if (cine == null)
            {
                return NotFound();
            }

            return mapper.Map<CineDTO>(cine);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CineCreacionDTO cineCreacionDTO)
        {
            var cine = mapper.Map<Cine>(cineCreacionDTO);
            context.Add(cine);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] CineCreacionDTO cineCreacionDTO)
        {
            var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == Id);

            if (cine == null)
            {
                return NotFound();
            }

            cine = mapper.Map(cineCreacionDTO, cine);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Cines.AnyAsync(x => x.Id == Id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Cine() { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
