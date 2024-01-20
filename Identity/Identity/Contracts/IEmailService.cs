namespace Identity.Contracts
{
    public interface IEmailService
    {
        public Task SendAsync(string stToEmail, string stSubject, string stBody);
    }
}
