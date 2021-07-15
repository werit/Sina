using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace recipeAPITest.TestUtils.Fixtures
{
    [CollectionDefinition(CommonFixtures.DatabaseCollectionName)]
    public class DatabaseCollection : ICollectionFixture<TestSuiteSetup>
    {
    }

    public class TestSuiteSetup : IDisposable
    {
        private const int TestDbPort = 5432;
        public const string DatabaseSchemaName = "recipedb";
        public const string DatabaseUsername = "mmadmin";
        public const string DatabasePassword = "mmkoko";

        public static readonly string DbaConnectionString =
            $"Server=localhost; Port={TestDbPort}; Database={DatabaseSchemaName}; Username={DatabaseUsername}; Password={DatabasePassword};";

        private const string TestDbName = "db-test";
        private const string MmNetworkName = "mmnet-test";
        private const int ExpectedMsDelayNeededForDatabaseReadiness = 2000;
        private const string DockerName = "docker";

        private bool isDisposed;

        private readonly IMessageSink diagnosticMessageSink;


        public TestSuiteSetup(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
            isDisposed = false;
            try
            {
                Startup().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        private void CallAndSuppressAnyException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                var message = new DiagnosticMessage($"Action: {action.Method} crasher and will be ignored.");
                diagnosticMessageSink.OnMessage(message);
            }
        }

        private async Task Startup()
        {
            var environmentSetup = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environmentSetup == "CircleCi")
            {
                // if CircleCi, then do not spin anything. Db will be spun in CircleCi.
            }
            else
            {
                ProcessUtils.StartAndWaitForExit(DockerName, $"network create {MmNetworkName}");

                ProcessUtils.StartAndWaitForExit(DockerName, "pull amd64/postgres:12.2-alpine");
                StartTestDb();
            }

            await Task.Delay(ExpectedMsDelayNeededForDatabaseReadiness);

            MigrationUtils.MigrateDb();
        }

        private static void StartTestDb()
        {
            ProcessUtils.StartAndWaitForExit(DockerName,
                $"run --rm -d -p {TestDbPort}:5432 -e POSTGRES_USER={DatabaseUsername} -e POSTGRES_PASSWORD={DatabasePassword} -e POSTGRES_DB={DatabaseSchemaName} --name {TestDbName} amd64/postgres:12.2-alpine");
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            var environmentSetup = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environmentSetup == "CircleCi")
            {
                // if CircleCi, then do not spin anything. Db will be spun in CircleCi.
            }
            else
            {
                CallAndSuppressAnyException(StopTestDb);
                CallAndSuppressAnyException(StopMnNet);
            }

            isDisposed = true;
        }

        private static void StopMnNet()
        {
            ProcessUtils.StartAndWaitForExit(DockerName, $"network rm {MmNetworkName}");
        }

        private static void StopTestDb()
        {
            ProcessUtils.StartAndWaitForExit(DockerName, $"stop {TestDbName}");
        }
    }
}