using ProjectTestAPI_1.YDB;
using Ydb.Sdk;
using Ydb.Sdk.Table;

namespace ProjectTestAPI_1.Services
{
    public class MyYDBService : IHostedService
    {
        public static List<string> fuelNames = new List<string> {"diesel", "a92", "a95", "a98"};
        public static Driver driver;
        public static TableClient Client;
         
        public Task StartAsync(CancellationToken cancellationToken)
        {
            driver = YDB.YDB.MakeDriver("grpc://localhost:2136","/local",YDB.YDB.MakeProvider());
            Task task = YDB.YDB.Run(driver);
            Client = YDB.YDB.MakeClient(driver);
            return task;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}