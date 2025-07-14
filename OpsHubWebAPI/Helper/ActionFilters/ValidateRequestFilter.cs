using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpsHubCommonUtility.Response;
using System.Linq;

namespace OpsHubWebAPI.Helper.ActionFilters
{
    public class ValidateRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(m => m.Value.Errors.Any())
                    .SelectMany(m => m.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(new ResultWithDataDTO<dynamic>
                {
                    BusinessErrorMessage = "Validation Failed.",
                    ValidationMessages = errors,
                    IsSuccessful = false,
                    IsBusinessError = true
                });
            }
        }
    }

}
