using Ydb.Sdk;
using Ydb.Sdk.Auth;
using Ydb.Sdk.Table;

namespace ProjectTestAPI_1.YDB
{
    public static class YDB
    {        
        
    public static Driver MakeDriver(
                string endpoint,
                string database,
                ICredentialsProvider credentialsProvider
    ){
        var config = new DriverConfig(
                endpoint: endpoint,
                database: database,
                credentials: credentialsProvider
            );

             using Driver driver = new Driver(
                config: config
            );
            return driver;
    }
    public static async Task Run(Driver driver){
            await driver.Initialize();
        }
    public static TableClient MakeClient(Driver driver){
        return new TableClient(driver, new TableClientConfig());
    }
    public static AnonymousProvider MakeProvider(){
        return new AnonymousProvider();
    }
    }
}