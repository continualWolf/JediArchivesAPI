using JediArchives.DataStorage.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JediArchives.Helper;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class JediAuthorizeAttribute : Attribute, IAuthorizationFilter {
    private readonly HashSet<string> _allowedRoles;

    public JediAuthorizeAttribute(params JediRanks[] allowedRanks) {
        _allowedRoles = allowedRanks.Select(r => r.ToString()).ToHashSet();
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        var user = context.HttpContext.User;

        if (!user.Identity?.IsAuthenticated ?? false) {
            context.Result = new UnauthorizedResult();
            return;
        }

        var role = user.FindFirstValue(ClaimTypes.Role);

        if (role is null || !_allowedRoles.Contains(role)) {
            context.Result = new ForbidResult();
        }
    }
}
