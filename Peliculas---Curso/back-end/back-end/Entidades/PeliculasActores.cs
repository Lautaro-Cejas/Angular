using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class PeliculasActores
    {
        public int PeliculaID {  get; set; }
        public int ActorID { get; set; }
        public Pelicula Pelicula { get; set; }
        public Actor Actor { get; set; }
        [StringLength(maximumLength: 100)]
        public string Personaje { get; set; }
        public int Orden { get; set; }
    }
}
