using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rentacar.Controllers
{
    [Authorize]
    public class ApiControllerBase : Controller
    {
        protected Guid AccountId { get; private set; }
        protected Guid? ApiClientId { get; private set; }
        protected string UserName { get; private set; }
        protected string AccountName { get; private set; }
        protected string PartyIdentification { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity != null && User != null && User.Identity.IsAuthenticated)
            {
                AccountId = Guid.Parse(User.Claims.First(x => string.Equals(x.Type, "AccountId")).Value);
                
                UserName = User.Claims.FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.NameIdentifier, StringComparison.Ordinal))?.Value;

                ApiClientId = Guid.TryParse(User.Claims.FirstOrDefault(x => string.Equals(x.Type, "ApiClientId"))?.Value, out var _clientId) ? _clientId : null;

                AccountName = User.Claims.First(x => string.Equals(x.Type, "AccountName", StringComparison.Ordinal)).Value;
                PartyIdentification = User.Claims.FirstOrDefault(x => string.Equals(x.Type, "PartyIdentification", StringComparison.Ordinal))?.Value;
            }

            base.OnActionExecuting(context);
        }

    }
}