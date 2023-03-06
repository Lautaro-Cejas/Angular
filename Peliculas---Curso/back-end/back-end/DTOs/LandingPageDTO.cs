using System.Collections.Generic;

namespace back_end.DTOs
{
    public class LandingPageDTO
    {
        public List<PeliculaDTO> EnCines { get; set; }
        public List<PeliculaDTO > ProximosEstrenos { get; set; }
    }
}
