using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace back_end.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());

            CreateMap<CineCreacionDTO, Cine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));
            CreateMap<Cine, CineDTO>()
                .ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
                .ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X));

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapearPeliculasGeneros))
                .ForMember(x => x.PeliculasCines, options => options.MapFrom(MapearPeliculasCines))
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapearPeliculasActores));
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapearPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapearPeliculasActores))
                .ForMember(x => x.Cines, options => options.MapFrom(MapearPeliculasCines));

            CreateMap<IdentityUser, UsuarioDTO>();
        }

        private List<CineDTO> MapearPeliculasCines(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<CineDTO>();

            if (pelicula.PeliculasCines != null)
            {
                foreach (var cinePeliculas in pelicula.PeliculasCines)
                {
                    resultado.Add(new CineDTO()
                    {
                        Id = cinePeliculas.CineID,
                        Nombre = cinePeliculas.Cine.Nombre,
                        Latitud = cinePeliculas.Cine.Ubicacion.Y,
                        Longitud = cinePeliculas.Cine.Ubicacion.X
                    });
                }
            }

            return resultado;
        }

        private List<PeliculaActorDTO> MapearPeliculasActores(Pelicula pelicula, PeliculaDTO peliculaDTO)
        { 
            var resultado = new List<PeliculaActorDTO>();

            if (pelicula.PeliculasActores != null)
            {
                foreach (var actorPeliculas in pelicula.PeliculasActores)
                {
                    resultado.Add(new PeliculaActorDTO() 
                    { 
                        Id = actorPeliculas.ActorID, 
                        Nombre = actorPeliculas.Actor.Nombre,
                        Foto = actorPeliculas.Actor.Foto,
                        Orden = actorPeliculas.Orden,
                        Personaje = actorPeliculas.Personaje
                    });
                }
            }

            return resultado;
        }

        private List<GeneroDTO> MapearPeliculasGeneros(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<GeneroDTO>();

            if (pelicula.PeliculasGeneros != null)
            {
                foreach (var generoPeliculas in pelicula.PeliculasGeneros)
                {
                    resultado.Add(new GeneroDTO() 
                    {
                        Id = generoPeliculas.GeneroID, 
                        Nombre = generoPeliculas.Genero.Nombre 
                    });
                }
            }

            return resultado;
        }

        private List<PeliculasActores> MapearPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }
            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorID = actor.Id, Personaje = actor.Personaje });
            }

            return resultado;
        }

        private List<PeliculasGeneros> MapearPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if (peliculaCreacionDTO.GenerosIDs == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroID = id });
            }

            return resultado;
        }

        private List<PeliculasCines> MapearPeliculasCines(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasCines>();

            if (peliculaCreacionDTO.CinesIDs == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.CinesIDs)
            {
                resultado.Add(new PeliculasCines() { CineID = id });
            }

            return resultado;
        }
    }
}
