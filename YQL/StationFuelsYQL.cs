using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class StationFuelsYQL
    {
        public StationFuelsYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public Task<IResponse> CreateStationFuel(ulong id,ulong stationId, ulong fuelId){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $id AS Uint64;
                    DECLARE $station_id AS Uint64;
                    DECLARE $fuel_id AS Uint64;

                    UPSERT INTO StationsFuels (id,station_id, fuel_id) VALUES
                    ($id,$station_id, $fuel_id);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) },
                    { "$station_id", YdbValue.MakeUint64(stationId) },
                    { "$fuel_id", YdbValue.MakeUint64(fuelId)},
                }
            );
            });
            return response;
        }
        public string GetStations_byFuelId(ulong fuel_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,fuel_id,station_id FROM StationsFuels WHERE fuel_id == {fuel_id}
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
            StationFuels_JsonByFuel stationFuels = new StationFuels_JsonByFuel(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
        public string GetStations_byStationId(ulong station_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id FROM StationsFuels WHERE station_id == {station_id}
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
            StationFuels_JsonByStation stationFuels = new StationFuels_JsonByStation(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
        public string GetStationsFuels(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id FROM StationsFuels
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
            StationFuels_JsonByStation stationFuels = new StationFuels_JsonByStation(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
    }
}