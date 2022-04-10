using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YqlScript
{
    public class YQL
    {
        public YQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;

        public Task<IResponse> CreateDbTables(){
            var response = client.SessionExec(async session =>
                {
                    return await session.ExecuteSchemeQuery(@"
                    CREATE TABLE Stations (
                    station_id Uint64,
                    enable bool,
                    name Utf8,
                    adress Utf8,
                    location Json,
                    PRIMARY KEY (station_id)
                );
                    CREATE TABLE Fuels (
                    fuel_id Uint64,
                    name Utf8,
                    fullname Utf8,
                    PRIMARY KEY (station_id)
            ");
            });
            return response;
        }
        
        public Task<IResponse> CreateStation(ulong id, bool enable, string? name, string? adress, double lat, double Long){
            string mJsn = "{ \"Lat\":" + lat.ToString()+",\"Long\":" + Long.ToString()+ " }";
            Station station = new Station(id,enable,name,adress,new Location(lat,Long));
            ulong bl = station.Enable == true ? bl = 1 : bl = 0;
            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $station_id AS Uint64;
                    DECLARE $enable AS Uint64;
                    DECLARE $name AS Utf8;
                    DECLARE $adress AS Utf8;
                    DECLARE $json as Json;

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
                    { "$json", YdbValue.MakeJson(mJsn) },
                }
            );
            });
            return response;
        }
        public string GetStation(ulong id){
            List<string> Result = new List<string>();
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
            var y = resp.Result.ResultSets[0].Columns;
            string s = "{"; 
            s += "\n \"" + y[0].Name + "\" : " + x[0][0].GetOptionalUint64().ToString() + ",";
            s += "\n \"" + y[1].Name + "\" : " + x[0][1].GetOptional().GetBool().ToString() + ",";
            s += "\n \"" + y[2].Name + "\" : \"" + x[0][2].GetOptionalUtf8().ToString() + "\",";
            s += "\n \"" + y[3].Name + "\" : \"" + x[0][3].GetOptionalUtf8().ToString() + "\",";
            s += "\n \"" + y[4].Name + "\" : " + x[0][4].GetOptionalJson().ToString();
            s += "\n }";
            return s;
        }          
    }
}