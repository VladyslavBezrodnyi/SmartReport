using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.DataAccessLayer.Interfaces
{
    public interface IPlaceRepository
    {
        Task<PlaceDTO> Create(PlaceDTO placeDTO);
    }
}
