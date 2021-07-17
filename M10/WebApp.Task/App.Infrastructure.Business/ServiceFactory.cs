using App.Services.Interfaces;

namespace App.Infrastructure.Business
{
    public static class ServiceFactory
    {
        public enum ServiceType
        {
            MailSender,
            MessageSender
        }

        public delegate ISender ServiceResolver(ServiceType serviceType);
    }
}