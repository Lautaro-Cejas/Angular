using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using back_end.Utilidades;

namespace back_end.DTOs
{
    public class PeliculaCreacionDTO
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public string Trailer { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public IFormFile Poster { get; set; }
        [ModelBinder(binderType: typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }
        [ModelBinder(binderType: typeof(TypeBinder<List<int>>))]
        public List<int> CinesIDs { get; set; }
        [ModelBinder(binderType: typeof(TypeBinder<List<ActorPeliculaCreacionDTO>>))]
        public List<ActorPeliculaCreacionDTO> Actores { get; set; }
    }
}
