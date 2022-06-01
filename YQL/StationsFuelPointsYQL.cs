using System.Text.Json;
using ProjectTestAPI_1.Models;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class StationsFuelPointsYQL
    {
        public StationsFuelPointsYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public Task<IResponse> CreateStationFuelPoint(ulong id,ulong stationId, ulong fuelPointId){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $id AS Uint64;
                    DECLARE $station_id AS Uint64;
                    DECLARE $fuelPoint_id AS Uint64;

                    UPSERT INTO StationsFuelPoints (id,station_id, fuelPoint_id) VALUES
                    ($id,$station_id, $fuelPoint_id);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) },
                    { "$station_id", YdbValue.MakeUint64(stationId) },
                    { "$fuelPoint_id", YdbValue.MakeUint64(fuelPointId)},
                }
            );
            });
            return response;
        }
        public string GetStations_byStationId(ulong station_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuelPoint_id FROM StationsFuelPoints WHERE station_id == {station_id}
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
            StationFuelPoint stationFuelPoint = new StationFuelPoint(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(stationFuelPoint) + ", \n";           
            }
            return s;
        }
        public string GetStationsFuelsPoints(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuelPoint_id FROM StationsFuelPoints
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
            StationFuelPoint stationFuelPoint = new StationFuelPoint(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(stationFuelPoint) + ", \n";           
            }
            return s;
        }
    }
}