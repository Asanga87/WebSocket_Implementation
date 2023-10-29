namespace WSAPP.Services
{
    public interface IMessageBuilderService
    {
        IEnumerable<byte> PrepareMesage(string Message, int RandomFlag);
    }
}
