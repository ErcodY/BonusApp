namespace Base;

public interface IJsonService
{
    public string FilePath { get; init; }
    void Save<T>(IEnumerable<T> items) where T : IModel;
    IEnumerable<T> Load<T>() where T : IModel;
}