using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace qsLog.Presentetion.Controllers
{
    [Authorize]
    public class ApiController: ControllerBase
    {
        protected string ProjectApiKey { get; private set; }

        // public void SetProjectApiKey(string apiKey)
        // {
        //     this.ProjectApiKey = apiKey;
        // }
    }
}