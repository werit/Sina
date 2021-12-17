using System;
using System.Threading.Tasks;
using sina.test.common.CommonTestUtils;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace sina.test.planning.TestUtils.PlanningFixtures
{
    [CollectionDefinition(CommonFixtures.DatabaseCollectionName)]
    public class DatabaseCollection : ICollectionFixture<TestSuiteSetupPlanning>
    {
    }

    public class TestSuiteSetupPlanning : IDisposable
    {
        private static int TestDbPort = 54322;
        public const string DatabaseSchemaName = "planningdb";
        public const string DatabaseUsername = "mmadmin";
        public const string DatabasePassword = "mmkoko";
        private static string TestDockerHost = "localhost";

        public static readonly string DbaConnectionString =
            $"Server={TestDockerHost}; Port={TestDbPort}; Database={DatabaseSchemaName}; Username={DatabaseUsername}; Password={DatabasePassword};";

        private const string TestDbName = "db-test-planning";
        private const string MmNetworkName = "planning-test";
        private const int ExpectedMsDelayNeededForDatabaseReadiness = 3000;
        private const string DockerName = "docker";

        private bool isDisposed;

        private readonly IMessageSink diagnosticMessageSink;


        public TestSuiteSetupPlanning(IMessageSink diagnosticMessageSink)
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
                TestDockerHost = "pg_planning";
                TestDbPort = 5432;
                Console.WriteLine($"Port changed to: {TestDbPort}");
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