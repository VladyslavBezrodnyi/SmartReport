using Xamarin.Forms;
using SmartReport.Models.Navigation;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;
using SmartReport.Models.Entities;
using System;
using System.Linq;
using SmartReport.DataService;
using SmartReport.Views.Navigation;
using SmartReport.Views.Transaction;

namespace SmartReport.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for selectable name page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class SelectableNamePageViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SelectableNamePageViewModel"/> class.
        /// </summary>
        public SelectableNamePageViewModel()
        {

        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        public Command CreateReportCommand
        {
            get
            {
                return new Command(async () => 
                {
                    var model = new ReportModel()
                    {
                        Date = DateTime.Now,
                        ReportText = Description,
                        Tasks = this.SelectableName.Where(x => x.IsSelected).Select(x => new TaskModel() { Id = x.Id }).ToList()
                    };
                    if(model.Tasks.Count != 0)
                    {
                        await TaskDataService.CreateReport(model);
                        await StaticHandler.Navigation.PushModalAsync(new NavigationPage(new PaymentSuccessPage()));
                    }

                } );
            }
        }

        /// <summary>
        /// Gets or sets a collction of value to be displayed in selectable name page
        /// </summary>
        [DataMember(Name = "selectableName")]
        public ObservableCollection<Contact> SelectableName { get; set; }

        public string Description { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the selectable names list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }

        #endregion
    }
}
