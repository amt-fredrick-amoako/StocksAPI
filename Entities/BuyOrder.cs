using System.ComponentModel.DataAnnotations;

namespace Entities;

public class BuyOrder
{
    [Key]
    public Guid BuyOrderID { get; set; }

    [Required]
    public string StockSymbol { get; set; } = string.Empty;

    [Required]
    public string StockName { get; set; } = string.Empty;

    public DateTime DateAndTmeOfOrder { get; set; }

    [Range(1, 1000, ErrorMessage = "Value should be between {0} and {1}")]
    public uint Quantity { get; set; }

    [Range(1, 1000, ErrorMessage = "Value should be between {0} and {1}")]
    public Decimal Price { get; set; }
}
