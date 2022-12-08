using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Vadlidators
{
   public class BookingValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Booking)validationContext.ObjectInstance;

            if(model.StartDateTs > model.EndDateTs)
                return new ValidationResult("Start date should not greater than End date");

            if (model.StartDateTs < DateTime.Now)
                return new ValidationResult("Can't book a room for past time!");

            return ValidationResult.Success;
        }
    }
}
