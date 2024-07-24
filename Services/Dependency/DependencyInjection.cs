using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.PaymentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<ITutorRepository, TutorRepository>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<IComplaintService, ComplaintService>();

            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletService>();

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassService, ClassService>();

            services.AddScoped<ITutorAdRepository, TutorAdRepository>();
            services.AddScoped<ITutorAdService, TutorAdService>();

            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<MomoService>();
            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

            return services;
        }
    }
}
