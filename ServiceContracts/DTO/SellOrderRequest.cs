using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO;

public class SellOrderRequest : IValidatableObject, IOrderRequest
{
    [Required]
    public string StockSymbol { get; set; } = string.Empty;

    [Required]
    public string StockName { get; set; } = string.Empty;

    public DateTime DateAndTimeOfOrder { get; set; }

    [Range(1, 1000, ErrorMessage = "Value should be between {0} and {1}")]
    public uint Quantity { get; set; }

    [Range(1, 1000, ErrorMessage = "Value should be between {0} and {1}")]
    public Decimal Price { get; set; }

    // Validation for model property
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> results = new List<ValidationResult>(); // create a list of validation results

        if (DateAndTimeOfOrder < Convert.ToDateTime("01-01-2000"))
            results.Add(new ValidationResult("Date of the order should not be older than Jan 01, 2000"));
        return results;
    }
}
