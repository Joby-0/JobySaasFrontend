using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;
public class OnboardingStateService
{
    public string OrgName { get; set; } = "";
    public SubscriptionPlanDto? SelectedPlan { get; set; }
}