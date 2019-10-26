using System.Globalization;
using Cimbalino.Phone.Toolkit.Extensions;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PedroLamas.Quadratic.Model;

namespace PedroLamas.Quadratic.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMainModel _mainModel;
        private readonly INavigationService _navigationService;

        #region Properties

        public string ParameterA
        {
            get
            {
                return _mainModel.ParameterA.ToStringInvariantCulture();
            }
            set
            {
                double parameterA;

                _mainModel.ParameterA = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out parameterA) ? parameterA : 0;

                RaisePropertyChanged(() => ParameterA);
                RaisePropertyChanged(() => Equation);
            }
        }

        public string ParameterB
        {
            get
            {
                return _mainModel.ParameterB.ToStringInvariantCulture();
            }
            set
            {
                double parameterB;

                _mainModel.ParameterB = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out parameterB) ? parameterB : 0;

                RaisePropertyChanged(() => ParameterB);
                RaisePropertyChanged(() => Equation);
            }
        }

        public string ParameterC
        {
            get
            {
                return _mainModel.ParameterC.ToStringInvariantCulture();
            }
            set
            {
                double parameterC;

                _mainModel.ParameterC = double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out parameterC) ? parameterC : 0;

                RaisePropertyChanged(() => ParameterC);
                RaisePropertyChanged(() => Equation);
            }
        }

        public string Equation
        {
            get
            {
                return "{0}x² + {1}x + {2} = 0".FormatWithInvariantCulture(_mainModel.ParameterA, _mainModel.ParameterB, _mainModel.ParameterC);
            }
        }

        public bool AcceptComplexNumberSolutions
        {
            get
            {
                return _mainModel.AcceptComplexNumberSolutions;
            }
            set
            {
                if (_mainModel.AcceptComplexNumberSolutions != value)
                {
                    _mainModel.AcceptComplexNumberSolutions = value;

                    RaisePropertyChanged(() => AcceptComplexNumberSolutions);
                }
            }
        }

        public RelayCommand SolveEquationCommand { get; private set; }

        public RelayCommand ShowAboutCommand { get; private set; }

        #endregion

        public MainViewModel(IMainModel mainModel, INavigationService navigationService)
        {
            _mainModel = mainModel;
            _navigationService = navigationService;

            SolveEquationCommand = new RelayCommand(OnSolveEquationCommand);

            ShowAboutCommand = new RelayCommand(OnShowAboutCommand);
        }

        private void OnSolveEquationCommand()
        {
            _navigationService.NavigateTo("/View/SolutionPage.xaml");
        }

        private void OnShowAboutCommand()
        {
            _navigationService.NavigateTo("/View/AboutPage.xaml");
        }
    }
}