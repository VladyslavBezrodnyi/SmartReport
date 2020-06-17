using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Mappers
{
    public static class PlaceExtension
    {
        public static PlaceDTO ToPlaceDTO(this Place place)
        {
            PlaceDTO placeDTO = new PlaceDTO
            {
                Id = place.Id,
                Name = place.Name,
                Description = place.Description,
                Code = place.Code
            };
            return placeDTO;
        }
        public static Place ToPlace(this PlaceDTO placeDTO)
        {
            Place place = new Place
            {
                Name = placeDTO.Name,
                Description = placeDTO.Description,
                Code = placeDTO.Code
            };
            return place;
        }
    }
}