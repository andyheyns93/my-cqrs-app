using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarCatalog.Api.Contracts.Attributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        public const string DefaultErrorMessage = "The {0} field cannot be empty";
        public NotEmptyGuidAttribute() : base(DefaultErrorMessage) { }
        public override bool IsValid(object value)
        {
            //NotEmpty doesn't necessarily mean required
            if (value is null)
                return true;

            switch (value)
            {
                case Guid guid:
                    return guid != Guid.Empty;
                default:
                    return true;
            }
        }
    }
}
