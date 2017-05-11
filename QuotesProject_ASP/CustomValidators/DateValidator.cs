using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotesProject_ASP.CustomValidators
{
    public class DateValidator
    {
        public static ValidationResult ValidateEndTimeRange(DateTime endTime)
        {
            int result = DateTime.Compare(endTime, DateTime.Now);
            if (result < 0)
            {
                int earlier = DateTime.Compare(endTime, new DateTime(1900, 01, 01));
                if (earlier < 0)
                {
                    return new ValidationResult("Date is out of range");
                }
            }
            else if (result == 0)
                return ValidationResult.Success;
            else
                return new ValidationResult("Date is out of range");

            return ValidationResult.Success;
        }
    }
}