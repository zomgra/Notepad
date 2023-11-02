using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Notepad.Domain.Exceptions;

namespace Notepad.API.Middleware
{
    public static class ProblemDetailsFactoryExtentions
    {
        public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext context,
            ValidationException validationError)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in validationError.Errors)
            {
                modelStateDictionary.AddModelError(error.PropertyName, error.ErrorCode);
            }


            return factory.CreateValidationProblemDetails(
                context,
                modelStateDictionary,
                StatusCodes.Status400BadRequest,
                "Validation failed");
        }

        public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext context,
           NotepadNotFoundException ex) =>
           factory.CreateProblemDetails(context,
               StatusCodes.Status500InternalServerError,
               ex.Message);
        public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext context,
          UnauthorizedAccessException ex) =>
          factory.CreateProblemDetails(context,
              StatusCodes.Status500InternalServerError,
              "You dont have permissions for make it");
    }
}
