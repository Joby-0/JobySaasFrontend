
using BlazorBootstrap;
using JobySaasFrontend.Components.Components;

namespace JobySaasFrontend.Components.Layout;

public partial class NavMenu
{
    private SignUpModal? signUpModal;
    private async Task OpenRegisterModal()
    {
        if (signUpModal is not null)
        {
            await signUpModal.OpenModal();
        }
    }
}