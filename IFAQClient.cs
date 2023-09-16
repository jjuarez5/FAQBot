namespace FAQ
{
    public interface IFAQClient
    {
        public Task<object> GetChatResponseAsync( Message message);
    }
}
