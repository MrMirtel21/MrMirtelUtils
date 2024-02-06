namespace MrMirtel21.Utils.HttpConnector.Connections
{
    public interface IConnectionBehavior
    {
        T ExecuteRequest<T>(Func<T> request);
    }
}
