using System;
using System.Net.Http;
using DatabaseSandbox.core;
using DatabaseSandbox.core.Interfaces;
using DatabaseSandbox.core.Utility;
using Suzianna.Core;
using TechTalk.SpecFlow;
using ServiceLocator = Suzianna.Core.ServiceLocator;

namespace Suzianna.DatabaseSandbox
{
    [Binding]
    public class SandBoxHook
    {
        
        public static string ConnectionString;
        public static string MigrationPath;
        private string _databaseName;

        [BeforeScenario("databaseSandbox")]
        public void BeforeExecute()
        {
            _databaseName = Database.Name;
            var sandboxInterceptor = new SandboxInterceptor(ConnectionString,_databaseName,MigrationPath);

            ServiceLocator.AddOrUpdate<IHttpRequestInterceptor>(sandboxInterceptor);
        }

        [AfterScenario("databaseSandbox")]
        public void AfterExecute()
        {
            //Todo:Change The Name of ServiceLocator in DatabaseSandbox
            var sqlServerDatabase = new SqlServerDatabase(ConnectionString);
            sqlServerDatabase.Drop(_databaseName);
        }
    }
   

    public class SandboxInterceptor : IHttpRequestInterceptor
    {
        private string _connectionstring;
        private string _databaseName;
        private string _migrationPath;

        public SandboxInterceptor(string connectionstring, string databaseName, string migrationPath)
        {
            _connectionstring = connectionstring;
            _databaseName = databaseName;
            _migrationPath = migrationPath;
        }
        public void Process(HttpRequestMessage requestMessage)
        {
            requestMessage.SetSandBoxHeader(_connectionstring, _databaseName, _migrationPath);
        }
    }
}
