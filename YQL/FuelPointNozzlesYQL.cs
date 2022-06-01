using System.Text.Json;
using ProjectTestAPI_1.Models;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class FuelPointNozzlesYQL
    {
        public FuelPointNozzlesYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public Task<IResponse> CreateFuelPointNozzle(ulong id,ulong fuelPointId, ulong nozzleId, ulong fuelId){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $id AS Uint64;
                    DECLARE $fuelPoint_id AS Uint64;
                    DECLARE $nozzle_id AS Uint64;
                    DECLARE $fuel_id AS Uint64;

                    UPSERT INTO FuelPointsNozzles (id,fuelPoint_id, nozzle_id,fuel_id) VALUES
                    ($id,$fuelPoint_id, $nozzle_id, $fuel_id);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) },
                    { "$fuelPoint_id", YdbValue.MakeUint64(fuelPointId)},
                    { "$nozzle_id", YdbValue.MakeUint64(nozzleId) },
                    { "$fuel_id", YdbValue.MakeUint64(fuelId) },
                }
            );
            });
            return response;
        }
        public string GetNozzles_byFuelPointId(ulong fuelPointId){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,fuelPoint_id,nozzle_id,fuel_id FROM FuelPointsNozzles WHERE fuelPoint_id == {fuelPointId}
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                }
            );
            });
            response.Wait();
            ExecuteDataQueryResponse resp = (ExecuteDataQueryResponse)response.Result;
            var x = resp.Result.ResultSets[0].Rows;
            string s = "";
            for(int z = 0;z < x.Count();z++){
            FuelPointNozzle fuelPointNozzle = new FuelPointNozzle(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(fuelPointNozzle) + ", \n";           
            }
            return s;
        }
        public string GetFuelPointNozzles(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,fuelPoint_id,nozzle_id,fuel_id FROM FuelPointsNozzles
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                }
            );
            });
            response.Wait();
            ExecuteDataQueryResponse resp = (ExecuteDataQueryResponse)response.Result;
            var x = resp.Result.ResultSets[0].Rows;
            string s = "";
            for(int z = 0;z < x.Count();z++){
            FuelPointNozzle fuelPointNozzle = new FuelPointNozzle(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(fuelPointNozzle) + ", \n";           
            }
            return s;
        }
        
    }
}