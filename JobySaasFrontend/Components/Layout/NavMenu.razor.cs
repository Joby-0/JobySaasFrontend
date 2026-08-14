
using BlazorBootstrap;
using JobySaasFrontend.Components.Components;

namespace JobySaasFrontend.Components.Layout;
public partial class NavMenu
{
    SignUpModal signUpModal = new SignUpModal();
    private async Task OpenRegisterModal()
    {
       await signUpModal.OpenModal();
    }
}