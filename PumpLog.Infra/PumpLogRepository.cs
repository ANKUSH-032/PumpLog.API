using Dapper;
using Microsoft.Extensions.Configuration;
using PumpLog.Core.comman;
using PumpLog.Core.PumpLog;
using PumpLog.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PumpLog.Infra
{
    public class PumpLogRepository : IPumpLogRepository
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public PumpLogRepository()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
            _con = _iconfiguration["ConnectionStrings:DataAccessConnection"]!;
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }
        }
        public async Task<ClsResponse> FuelFillingInsert(FuelFillingInsertDto fillingInsertDto)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<ClsResponse>("[dbo].[uspFuelFillingInsert]", new
            {
                fillingInsertDto.DispenserNo,
                fillingInsertDto.QuantityFilled,
                fillingInsertDto.VehicleNumber,
                fillingInsertDto.PaymentMode,
                fillingInsertDto.CreatedBy,
                fillingInsertDto.PaymentProofPath

            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            ClsResponse? response = result.FirstOrDefault();

            return response!;
        }
      
        public async Task<ClsResponse<FuelFillingListDto>> FuelFillingGet(string FuelFillingId)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspFuelFillingGet]", new
            {
                FuelFillingId

            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            ClsResponse<FuelFillingListDto>? response = result.Read<ClsResponse<FuelFillingListDto>>().FirstOrDefault();

            if (response!.Status)
            {
                response.Data = result.Read<FuelFillingListDto>().ToList();

            }

            return response!;
        }
        public async Task<ClsResponse<FuelFillingListDto>> FuelFillingList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspFuelFillingList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<FuelFillingListDto>? response = result.Read<ClsResponse<FuelFillingListDto>>().FirstOrDefault();
            if (response!.Status)
            {
                response.Data = result.Read<FuelFillingListDto>().ToList();
                response.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return response;


        }

    }
}
