namespace JobySaasFrontend.Models.DTO;
public record SelectSubscriptionRequest(Guid SubscriptionId);

public class SubscriptionCheckoutResult
{
    public string CheckoutUrl { get; set; } // guessing this is a Stripe-style checkout redirect; adjust to actual return shape
    // add other fields your _service.CreateSubscriptionCheckoutAsync actually returns
}