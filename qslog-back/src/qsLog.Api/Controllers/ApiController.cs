using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace qsLog.Presentetion.Controllers
{
    [Authorize]
    public class ApiController: ControllerBase
    {
        protected Guid UserID 
        { 
            get {
                var id = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (id == null) return Guid.Empty;

                return Guid.Parse(id);
            } 
        }
        protected string ProjectApiKey { get; private set; }

        // public void SetProjectApiKey(string apiKey)
        // {
        //     this.ProjectApiKey = apiKey;
        // }
    }
}