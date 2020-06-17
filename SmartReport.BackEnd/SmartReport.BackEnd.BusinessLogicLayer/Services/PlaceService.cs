using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Services
{
    public class PlaceService: IPlaceService
    {
        public readonly IUnitOfWork _db;
        public PlaceService(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<PlaceDTO> Create(PlaceDTO placeDTO)
        {
            return await _db.Places.Create(placeDTO);
        }
    }
}
