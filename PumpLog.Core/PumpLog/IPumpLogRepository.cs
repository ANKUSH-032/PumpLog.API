using PumpLog.Core.comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Core.PumpLog
{
    public interface IPumpLogRepository
    {
        Task<ClsResponse> FuelFillingInsert(FuelFillingInsertDto fillingInsertDto);
        Task<ClsResponse<FuelFillingListDto>> FuelFillingList(JqueryDataTable jqueryDataTable);
        Task<ClsResponse<FuelFillingListDto>> FuelFillingGet(string FuelFillingId);
    }
}
