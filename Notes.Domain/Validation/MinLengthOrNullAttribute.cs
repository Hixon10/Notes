using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Notes.Domain.Validation
{
    public class MinLengthOrNullAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }

        public MinLengthOrNullAttribute(int minLength)
        {
            MinLength = minLength;
        }

        public override Boolean IsValid(Object value)
        {
            return value == null || (value as string).Length > MinLength;
        }
    }
}
