using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class StationsFuelsYQL
    {
        public StationsFuelsYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public Task<IResponse> CreateStationFuel(ulong id,ulong stationId, ulong fuelId,String fuelName){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $id AS Uint64;
                    DECLARE $station_id AS Uint64;
                    DECLARE $fuel_id AS Uint64;
                    DECLARE $fuel_name AS Utf8;

                    UPSERT INTO StationsFuels (id, station_id, fuel_id, fuel_name) VALUES
                    ($id, $station_id, $fuel_id, $fuel_name);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) },
                    { "$station_id", YdbValue.MakeUint64(stationId) },
                    { "$fuel_id", YdbValue.MakeUint64(fuelId)},
                    { "$fuel_name", YdbValue.MakeUtf8(fuelName)},
                }
            );
            });
            return response;
        }
        public string GetStationFuels_byFuelId(ulong fuel_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,fuel_id,station_id,fuel_name FROM StationsFuels WHERE fuel_id == {fuel_id}
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
            StationFuel_JsonByFuel stationFuels = new StationFuel_JsonByFuel(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUtf8());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
        public string GetStationFuels_byStationId(ulong station_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id,fuel_name FROM StationsFuels WHERE station_id == {station_id}
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
            StationFuel_JsonByStation stationFuels = new StationFuel_JsonByStation(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUtf8());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
        public string GetStationFuels(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id,fuel_name FROM StationsFuels
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
            StationFuel_JsonByStation stationFuels = new StationFuel_JsonByStation(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUtf8());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
        public List<StationFuel_JsonByStation> GetStationFuels_byStationId_ListObject_NotJson(ulong station_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id,fuel_name FROM StationsFuels WHERE station_id == {station_id}
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
            List<StationFuel_JsonByStation> list = new List<StationFuel_JsonByStation>();
            for(int z = 0;z < x.Count();z++){
            StationFuel_JsonByStation stationFuels = new StationFuel_JsonByStation(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetUtf8());
                    list.Add(stationFuels);          
            }
            return list;
        }
    }
}