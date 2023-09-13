namespace FAQ
{
    public interface IFAQClient
    {
        public Task<object> GetChatResponseAsync( string message );
    }
}
