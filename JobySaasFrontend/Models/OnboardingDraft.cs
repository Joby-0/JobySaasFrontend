using JobySaasFrontend.Models.DTO;

public class OnboardingDraft
{
    public string OrgName { get; set; } = "";
    public SubscriptionPlanDto? SelectedPlan { get; set; }
}