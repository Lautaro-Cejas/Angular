using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingsController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public RatingsController(UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioID = usuario.Id;

            var ratingActual = await context.Ratings
                .FirstOrDefaultAsync(
                x => x.PeliculaID == ratingDTO.PeliculaID 
                && 
                x.UsuarioID == usuarioID);

            if (ratingActual == null)
            {
                var rating = new Rating();
                rating.PeliculaID = ratingDTO.PeliculaID;
                rating.Puntuacion = ratingDTO.Puntuacion;
                rating.UsuarioID = usuarioID;
                context.Add(rating);
            } else
            {
                ratingActual.Puntuacion = ratingDTO.Puntuacion;
            }

            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
