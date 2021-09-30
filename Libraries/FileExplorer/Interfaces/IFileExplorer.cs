namespace BurningRiver.Libraries.FileExplorer.Interfaces;

public interface IFileExplorer
{
    Task<IEnumerable<string>> GetFileNamesAsync(string container);
}

public class FileExplorer : IFileExplorer
{
    private readonly string _connectionString;


    public FileExplorer(string connection)
    {
        _connectionString = connection;
    }

    public Task<IEnumerable<string>> GetFileNamesAsync(string container)
    {
        switch (container)
        {
            case "azure":
                ConnectToAzureStorageAccount();

                return;
            default:
                throw new Exception($"{container} not setup");
        }
    }

    private void ConnectToAzureStorageAccount()
    {

    }
}
