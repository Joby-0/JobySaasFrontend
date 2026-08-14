using BlazorBootstrap;
namespace JobySaasFrontend.Components.Components;
public partial class SignUpModal
{
    public Modal modal = new Modal();
    public async Task OpenModal()
    {
       await modal.ShowAsync();
    }
}