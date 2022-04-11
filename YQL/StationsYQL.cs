using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class StationsYQL
    {
        public StationsYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        
        public Task<IResponse> CreateStation(ulong id, bool enable, string? name, string? adress, double lat, double Long){
            var myLoc = new Location(lat,Long);
            Station station = new Station(id,enable,name,adress,myLoc);
            string myJson = JsonSerializer.Serialize(myLoc);
            ulong bl = station.Enable == true ? bl = 1 : bl = 0;
            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $station_id AS Uint64;
                    DECLARE $enable AS Uint64;
                    DECLARE $name AS Utf8;
                    DECLARE $adress AS Utf8;
                    DECLARE $json AS json;

                    UPSERT INTO Stations (station_id, enable, name, adress, location) VALUES
                    ($station_id, CAST($enable as bool), $name, $adress, $json);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$station_id", YdbValue.MakeUint64(station.Station_id) },
                    { "$enable", YdbValue.MakeUint64(bl)},
                    { "$name", YdbValue.MakeUtf8(station.Name) },
                    { "$adress", YdbValue.MakeUtf8(station.Adress) },
                    { "$json", YdbValue.MakeJson(myJson) },
                }
            );
            });
            return response;
        }
        public string GetStation(ulong id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT station_id,enable,name,adress,location FROM Stations WHERE station_id == {id}
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) }
                }
            );
            });
            response.Wait();
            ExecuteDataQueryResponse resp = (ExecuteDataQueryResponse)response.Result;
            var x = resp.Result.ResultSets[0].Rows;
            Station station = new Station(x[0][0].GetOptional().GetUint64(),
                                          x[0][1].GetOptional().GetBool(),
                                          x[0][2].GetOptionalUtf8().ToString(),
                                          x[0][3].GetOptionalUtf8().ToString(),
                                          JsonSerializer.Deserialize<Location>(x[0][4].GetOptionalJson().ToString()));
            return JsonSerializer.Serialize(station);
        }     
        public string GetStations(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT station_id,enable,name,adress,location FROM Stations
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
            Station station = new Station(x[z][0].GetOptional().GetUint64(),
                                          x[z][1].GetOptional().GetBool(),
                                          x[z][2].GetOptionalUtf8().ToString(),
                                          x[z][3].GetOptionalUtf8().ToString(),
                                          JsonSerializer.Deserialize<Location>(x[z][4].GetOptionalJson().ToString()));
                               s += JsonSerializer.Serialize(station) + ", \n";           
            }
            return s;
        }        
    }
}