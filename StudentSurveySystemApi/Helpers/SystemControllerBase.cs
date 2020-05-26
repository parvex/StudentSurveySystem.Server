using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server.Helpers
{
    public class SystemControllerBase : ControllerBase
    {
        public string ModelStateErrorsToString()
        {
            return string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }
    }
}