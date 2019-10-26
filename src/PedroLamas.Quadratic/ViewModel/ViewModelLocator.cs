using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PedroLamas.Quadratic.Model;

namespace PedroLamas.Quadratic.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            Register<INavigationService, NavigationService>();
            Register<IWebBrowserService, WebBrowserService>();
            Register<IEmailComposeService, EmailComposeService>();
            Register<ISmsComposeService, SmsComposeService>();
            Register<IShareLinkService, ShareLinkService>();
            Register<IShareStatusService, ShareStatusService>();
            Register<IMarketplaceReviewService, MarketplaceReviewService>();
            Register<IMarketplaceSearchService, MarketplaceSearchService>();

            Register<IMainModel, MainModel>();

            Register<MainViewModel>();
            Register<SolutionViewModel>();
            Register<AboutViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SolutionViewModel Solution
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SolutionViewModel>();
            }
        }

        public AboutViewModel About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }

        public static void Cleanup()
        {
        }

        #region Auxiliary Methods

        private void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TInterface>())
            {
                SimpleIoc.Default.Register<TInterface, TClass>();
            }
        }

        private void Register<TClass>()
            where TClass : class
        {
            if (!SimpleIoc.Default.IsRegistered<TClass>())
            {
                SimpleIoc.Default.Register<TClass>();
            }
        }

        #endregion
    }
}