using SmartReport.DataService;
using SmartReport.Models.Entities;
using SmartReport.Views.AboutUs;
using SmartReport.Views.Navigation;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SmartReport.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string name;

        private string password;

        private string confirmPassword;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel()
        {
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand 
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await AccountDataService.LoginAsync(new LoginModel() { Email = Email, Password = Password });
                        await StaticHandler.Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                    }
                    catch (Exception)
                    {

                    }
                });

            }
        }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand {
            get
            {
                return new Command(async () =>
                {
                    await StaticHandler.Navigation.PushModalAsync(new NavigationPage(new AboutUsSimplePage()));
                    await AccountDataService.RegisterAsync(new RegisterModel() { Email = Email, Name = Name, Password = Password });
                    await StaticHandler.Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                });

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}