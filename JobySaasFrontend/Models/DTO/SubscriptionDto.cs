namespace JobySaasFrontend.Models.DTO;
public record SelectSubscriptionRequest(Guid SubscriptionId);

public class SubscriptionCheckoutResult
{
    public string CheckoutUrl { get; set; } // guessing this is a Stripe-style checkout redirect; adjust to actual return shape
    // add other fields your _service.CreateSubscriptionCheckoutAsync actually returns
}

public class SubscriptionPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string StripePriceId { get; set; }
    public int BillingIntervalInMonths { get; set; }
    public bool IsFree {get; set;} = false;
    public bool ContactSales {get; set;}
    public string Description {get; set;}
    public List<string> Features {get; set;}

}