using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.CrossCuttingConcern.Mappers;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Repositories
{
    public class PlaceRepository: IPlaceRepository
    {
        private ApplicationDbContext _context;
        public PlaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PlaceDTO> Create(PlaceDTO placeDTO)
        {
            Place newPlace = placeDTO.ToPlace();
            await _context.Places.AddAsync(newPlace);
            await _context.SaveChangesAsync();
            return newPlace.ToPlaceDTO();

        }
    }
}
