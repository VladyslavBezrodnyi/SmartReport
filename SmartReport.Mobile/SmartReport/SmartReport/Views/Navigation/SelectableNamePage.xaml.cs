using System;
using System.Collections.ObjectModel;
using System.Linq;
using SmartReport.DataService;
using SmartReport.Models.Navigation;
using SmartReport.ViewModels.Navigation;
using Syncfusion.DataSource.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmartReport.Views.Navigation
{
    /// <summary>
    /// Page showing the list of selectable name with grouping.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectableNamePage
    {


        public SelectableNamePage()
        {
            InitializeComponent();
            Set();
        }

        private async void Set()
        {
            try
            {
                var missedTask = await TaskDataService.GetMissedTasksAsync();
                var model = new SelectableNamePageViewModel()
                {
                    SelectableName = new ObservableCollection<Contact>(missedTask.Select(x => new Contact() { Name = x.Name, Id = x.Id, BackgroundColor = "#dc9737" }).ToList())
                };
                this.BindingContext = model;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>x
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        /// <summary>
        /// Invoked when search button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Invoked when back to title button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Invokes when search box Animation completed.
        /// </summary>
        private void SearchBoxAnimationCompleted()
        {
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        private void SearchExpandAnimationCompleted()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Setting IsSelected property to false at entry time.
            if (BindingContext is SelectableNamePageViewModel)
            {
                var viewModel = BindingContext as SelectableNamePageViewModel;

                foreach (var item in viewModel.SelectableName)
                {
                    item.IsSelected = false;
                }
            }
        }
    }
}