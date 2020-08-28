using SmartReport.DataService;
using SmartReport.ViewModels.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartReport.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class TabbedForm : ContentPage
    {
        public TabbedForm()
        {
            InitializeComponent();
            StaticHandler.Navigation = this.Navigation;
            //var viewModel = new SignUpPageViewModel();
            //viewModel.Navigation = this.Navigation;
            //this.BindingContext = viewModel;
        }
    }
}