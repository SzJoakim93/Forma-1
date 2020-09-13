using System.ComponentModel.DataAnnotations;

namespace Forma_1.Helpers
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int minValue;

        public MinValueAttribute(int minValue)
        {
            this.minValue = minValue;
            this.ErrorMessage = "Value must be greater or equal than " + minValue; 
        }

        public override bool IsValid(object value)
        {
            return (int) value >= minValue;
        }
    }
}
