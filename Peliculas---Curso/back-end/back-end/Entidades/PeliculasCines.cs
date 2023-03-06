namespace back_end.Entidades
{
    public class PeliculasCines
    {
        public int PeliculaID { get; set; }
        public int CineID { get; set; }
        public Pelicula Pelicula { get; set; }
        public Cine Cine { get; set; }
    }
}
