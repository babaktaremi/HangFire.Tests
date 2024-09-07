

using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;

namespace HangFire.Tests;

public class HangFirePanelAuthorization
{
    private static readonly Lazy<IDashboardAuthorizationFilter>
        BasicAuthenticationFilter = new( new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,

            LoginCaseSensitive = true,
            
            Users = new[]
            {
                new BasicAuthAuthorizationUser
                {
                    Login = "admin",

                    PasswordClear = "admin"
                }
            }
        }));
    
       

    public static IDashboardAuthorizationFilter[] SetBasicAuthorizationFilter()
        => [BasicAuthenticationFilter.Value];
}