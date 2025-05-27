namespace Service.Contract
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IEmailService EmailService { get; }
        IAutenticationService AutenticationService { get; }
    }
}
