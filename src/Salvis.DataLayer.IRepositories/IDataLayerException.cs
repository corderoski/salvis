namespace Salvis.DataLayer.Repositories
{
    /// <summary>
    /// Represents an exception on a Data Layer.
    /// </summary>
    public interface IDataLayerException : System.Runtime.InteropServices._Exception
    {
        object RelatedObject { get; }
    }
}
