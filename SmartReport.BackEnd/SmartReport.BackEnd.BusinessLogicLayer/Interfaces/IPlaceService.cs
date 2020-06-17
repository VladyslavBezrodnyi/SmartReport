using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Interfaces
{
    public interface IPlaceService
    {
        Task<PlaceDTO> Create(PlaceDTO placeDTO);
    }
}
