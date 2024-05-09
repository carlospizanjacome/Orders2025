using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;
using System.Net;

namespace Orders.Frontend.Pages.Categories
{
    public partial class CategoriesIndex
    {
        [Inject]
        private IRepository Repository { get; set; } = null!;

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        public List<Category>? categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();

        }

        private async Task LoadAsync()
        {

            var responseHttp = await Repository.GetAsync<List<Category>>("api/categories");


            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("error", message, SweetAlertIcon.Error);
                return;

            }
            categories = responseHttp.Response;
        }

        private async Task DeleteAsync(Category category)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Esta seguro de brorrar la Categoria:{category.Name}?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Category>($"api/categories/{category.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();

                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                return;
            }
            await LoadAsync();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios borrado con éxito.");

        }
    }
}
