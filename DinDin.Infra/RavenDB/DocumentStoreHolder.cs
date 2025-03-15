using DinDin.Domain.Constantes;
using Raven.Client.Documents;

namespace DinDin.Infra.RavenDB
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> _store = new(CreateDocumentStore);

        private static DocumentStore CreateDocumentStore()
        {
            var serverURL = Environment.GetEnvironmentVariable(ApplicationConstants.SERVER_URL_ENVIRONMENT_VARIABLE)
                ?? throw new Exception($"Environment variable [{ApplicationConstants.SERVER_URL_ENVIRONMENT_VARIABLE}] not found");

            var databaseName = Environment.GetEnvironmentVariable(ApplicationConstants.DATABASE_NAME_ENVIRONMENT_VARIABLE)
                ?? throw new Exception($"Environment variable [{ApplicationConstants.DATABASE_NAME_ENVIRONMENT_VARIABLE}] not found");

            var documentStore = new DocumentStore
            {
                Urls = [serverURL],
                Database = databaseName
            };

            documentStore.Initialize();
            return documentStore;
        }

        public static IDocumentStore Store
        {
            get { return _store.Value; }
        }
    }
}