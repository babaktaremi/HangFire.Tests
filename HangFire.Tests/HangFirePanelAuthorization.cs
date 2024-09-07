

using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;

namespace HangFire.Tests;

public class HangFirePanelAuthorization(IConfiguration configuration)
{
    private  readonly Lazy<IDashboardAuthorizationFilter>
        BasicAuthenticationFilter = new( new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,

            LoginCaseSensitive = true,
            
            Users = new[]
            {
                new BasicAuthAuthorizationUser
                {
                    Login = configuration["Hangfire:UserName"],
                    PasswordClear = configuration["Hangfire:Password"]
                }
            }
        }));
    
       

    public  IDashboardAuthorizationFilter[] SetBasicAuthorizationFilter()
        => [BasicAuthenticationFilter.Value];
}