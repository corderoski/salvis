namespace Salvis.Framework.Services
{
    public interface IConfigurationService
    {
        int TruncatedTextMaxLength { get; }

        int MessagesMaxUnreadShowing { get; }

    }
}
