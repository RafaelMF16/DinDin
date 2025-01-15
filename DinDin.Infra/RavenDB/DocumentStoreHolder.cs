using DinDin.Domain.Constantes;
using Raven.Client.Documents;

public class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateDocumentStore);

    private static IDocumentStore CreateDocumentStore()
    {
        var serverURL = Environment.GetEnvironmentVariable(ApplicationConstants.SERVER_URL_ENVIRONMENT_VARIABLE)
            ?? throw new Exception($"Environment variable [{ApplicationConstants.SERVER_URL_ENVIRONMENT_VARIABLE}] not found");

        var databaseName = Environment.GetEnvironmentVariable(ApplicationConstants.DATABASE_NAME_ENVIRONMENT_VARIABLE)
            ?? throw new Exception($"Environment variable [{ApplicationConstants.DATABASE_NAME_ENVIRONMENT_VARIABLE}] not found");

        IDocumentStore documentStore = new DocumentStore
        {
            Urls = new[] { serverURL },
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