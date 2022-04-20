using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class StationsPricesYQL
    {
        public StationsPricesYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public Task<IResponse> CreateStationPrice(ulong id,ulong stationId, ulong fuelId, double price){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $id AS Uint64;
                    DECLARE $station_id AS Uint64;
                    DECLARE $fuel_id AS Uint64;
                    DECLARE $price as double;

                    UPSERT INTO StationsPrices (id,station_id, fuel_id, price) VALUES
                    ($id,$station_id, $fuel_id, $price);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) },
                    { "$station_id", YdbValue.MakeUint64(stationId) },
                    { "$fuel_id", YdbValue.MakeUint64(fuelId)},
                    { "$price", YdbValue.MakeDouble(price)},
                }
            );
            });
            return response;
        }
        public string GetStationsPrices_byFuelId(ulong fuel_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,fuel_id,station_id,price FROM StationsPrices WHERE fuel_id == {fuel_id}
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
            StationPrices_JsonByFuelId stationPrices = new StationPrices_JsonByFuelId(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetDouble());
                    s += JsonSerializer.Serialize(stationPrices) + ", \n";           
            }
            return s;
        }
        public string GetStationsPrices_byStationId(ulong station_id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id,price FROM StationsPrices WHERE station_id == {station_id}
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
            StationPrices_JsonByStationId stationPrices = new StationPrices_JsonByStationId(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetDouble());
                    s += JsonSerializer.Serialize(stationPrices) + ", \n";           
            }
            return s;
        }
        public string GetStationsPrices(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT id,station_id,fuel_id,price FROM StationsPrices
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
            StationPrices_JsonByStationId stationFuels = new StationPrices_JsonByStationId(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptional().GetUint64(),
                x[z][2].GetOptional().GetUint64(),
                x[z][3].GetOptional().GetDouble());
                    s += JsonSerializer.Serialize(stationFuels) + ", \n";           
            }
            return s;
        }
    }
}