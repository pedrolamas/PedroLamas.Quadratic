using System;
using System.Collections.Generic;
using System.Linq;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PedroLamas.Quadratic.Model;

namespace PedroLamas.Quadratic.ViewModel
{
    public class SolutionViewModel : ViewModelBase
    {
        private readonly IMainModel _mainModel;
        private readonly INavigationService _navigationService;
        private readonly IEmailComposeService _emailComposeService;
        private readonly ISmsComposeService _smsComposeService;
        private readonly IShareStatusService _shareStatusService;

        private int _decimals;

        #region Properties

        public IEnumerable<SolutionItem> SolutionEntries
        {
            get
            {
                return GetSolution();
            }
        }

        public int Decimals
        {
            get
            {
                return _decimals;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (Set(() => Decimals, ref _decimals, value))
                {
                    EquationItem.Decimals = _decimals;

                    RaisePropertyChanged(() => SolutionEntries);
                }
            }
        }

        public RelayCommand IncrementDecimalsCommand { get; private set; }

        public RelayCommand DecrementDecimalsCommand { get; private set; }

        public RelayCommand ShareByEmailCommand { get; private set; }

        public RelayCommand ShareBySmsCommand { get; private set; }

        public RelayCommand ShareOnSocialNetworkCommand { get; private set; }

        public RelayCommand ShowAboutCommand { get; private set; }

        #endregion

        public SolutionViewModel(IMainModel mainModel, INavigationService navigationService, IEmailComposeService emailComposeService, ISmsComposeService smsComposeService, IShareStatusService shareStatusService)
        {
            _mainModel = mainModel;
            _navigationService = navigationService;
            _emailComposeService = emailComposeService;
            _smsComposeService = smsComposeService;
            _shareStatusService = shareStatusService;

            IncrementDecimalsCommand = new RelayCommand(() => Decimals++);
            DecrementDecimalsCommand = new RelayCommand(() => Decimals--);

            ShareByEmailCommand = new RelayCommand(OnShareByEmailCommand);

            ShareBySmsCommand = new RelayCommand(OnShareBySmsCommand);

            ShareOnSocialNetworkCommand = new RelayCommand(OnShareOnSocialNetworkCommand);

            ShowAboutCommand = new RelayCommand(OnShowAboutCommand);

            Decimals = 3;
        }

        private void OnShareByEmailCommand()
        {
            _emailComposeService.Show("Quadratic Solver", GetMessageForSharing());
        }

        private void OnShareBySmsCommand()
        {
            _smsComposeService.Show(string.Empty, GetMessageForSharing());
        }

        private void OnShareOnSocialNetworkCommand()
        {
            _shareStatusService.Show(GetMessageForSharing());
        }

        private void OnShowAboutCommand()
        {
            _navigationService.NavigateTo("/View/AboutPage.xaml");
        }

        private IEnumerable<SolutionItem> GetSolution()
        {
            var a = _mainModel.ParameterA;
            var b = _mainModel.ParameterB;
            var c = _mainModel.ParameterC;
            var complex = _mainModel.AcceptComplexNumberSolutions;

            yield return SolutionItem.WithEquation("initial equation", true, new[] 
            {
                EquationItem.With("{0}x²", a),
                EquationItem.With("{0}x", b),
                EquationItem.With("{0}", c)
            });

            if (a == 0 || b == 0 || c == 0 || Math.Abs(a) == 1 || Math.Abs(b) == 1 || Math.Abs(c) == 1)
            {
                yield return SolutionItem.WithEquation("simplify equation", new[] 
                {
                    EquationItem.With("{0}x²", a),
                    EquationItem.With("{0}x", b),
                    EquationItem.With("{0}", c)
                });
            }

            if (a == 0 && b == 0)
            {
                if (c == 0)
                {
                    yield return SolutionItem.WithString("solve", "x = any number");
                }
                else
                {
                    yield return SolutionItem.WithString("different constant values", "impossible equation");
                }

                yield break;
            }

            if (c != 0)
            {
                yield return SolutionItem.WithEquation("constant term to right hand side", new[] 
                {
                    EquationItem.With("{0}x²", a),
                    EquationItem.With("{0}x", b)
                }, new[] 
                {
                    EquationItem.With("{0}", -c)
                });
            }

            if (a == 0)
            {
                if (b != 1)
                {
                    yield return SolutionItem.WithEquation("divide both sides by coeficient of x", new[] 
                    {
                        EquationItem.With("({0} / {1})x", b, b)
                    }, new[] 
                    {
                        EquationItem.With("{0} / {1}", -c, b)
                    });

                    yield return SolutionItem.WithEquation("simplify equation", new[] 
                    {
                        EquationItem.With("x")
                    }, new[] 
                    {
                        EquationItem.With("{0} / {1}", -c, b)
                    });
                }

                var r = -c / b;

                yield return SolutionItem.WithEquation("solve", new[] 
                {
                    EquationItem.With("x")
                }, new[] 
                {
                    EquationItem.With("{0}", r)
                });

                yield break;
            }

            if (a != 1)
            {
                if (b == 0)
                {
                    yield return SolutionItem.WithEquation("divide both sides by coeficient of x²", new[]
                    {
                        EquationItem.With("({0} / {1})x²", a, a)
                    }, new[]
                    {
                        EquationItem.With("{0} / {1}", -c, a)
                    });
                }
                else
                {
                    yield return SolutionItem.WithEquation("divide both sides by coeficient of x²", new[]
                    {
                        EquationItem.With("({0} / {1})x²", a, a),
                        EquationItem.With("({0} / {1})x", b, a)
                    }, new[]
                    {
                        EquationItem.With("{0} / {1}", -c, a)
                    });
                }

                b /= a;
                c /= a;
                a = 1;

                yield return SolutionItem.WithEquation("simplify equation", new[] 
                {
                    EquationItem.With("x²", a),
                    EquationItem.With("{0}x", b)
                }, new[] 
                {
                    EquationItem.With("{0}", -c)
                });
            }

            var e = b / 2;
            var d = Math.Pow(e, 2);

            if (b != 0)
            {
                yield return SolutionItem.With("calculate half of the coefficient of x and square it", new FormattableItem[]
                {
                    TextItem.With("d = "),
                    Equation.With(new[] 
                    {
                        EquationItem.With("({0} / 2)²", b)
                    }, new[] 
                    {
                        EquationItem.With("{0}", d)
                    })
                });

                yield return SolutionItem.WithEquation("add it to both sides of equation", new[] 
                {
                    EquationItem.With("x²", a),
                    EquationItem.With("{0}x", b),
                    EquationItem.With("{0}", d)
                }, new[] 
                {
                    EquationItem.With("{0}", -c),
                    EquationItem.With("{0}", d)
                });
            }

            var f = -c + d;

            if (e != 0)
            {
                yield return SolutionItem.WithEquation("complete the square and simplify", new[] 
                {
                    EquationItem.With("(x + {0})²", e),
                }, new[] 
                {
                    EquationItem.With("{0}", f)
                });
            }

            if (f < 0)
            {
                if (!complex)
                {
                    yield return SolutionItem.WithString("square on left equals negative value on right", "impossible equation");

                    yield break;
                }
            }
            else
            {
                complex = false;
            }

            if (e != 0 || f != 0)
            {
                yield return SolutionItem.WithEquation("square root both sides", new[] 
                {
                    EquationItem.With("x"),
                    EquationItem.With("{0}", e),
                }, new[] 
                {
                    EquationItem.With("±√({0})", f)
                });
            }

            if (e != 0)
            {
                yield return SolutionItem.WithEquation("isolate x", new[] 
                {
                    EquationItem.With("x"),
                }, new[] 
                {
                    EquationItem.With("{0}", -e),
                    EquationItem.With("±√({0})", f)
                });
            }

            var g = Math.Sqrt(Math.Abs(f));

            if (e != 0 && g != 0)
            {
                yield return SolutionItem.WithEquation("simplify equation", new[] 
                {
                    EquationItem.With("x"),
                }, new[] 
                {
                    EquationItem.With("{0}", -e),
                    EquationItem.With("±{0}", complex, g)
                });
            }

            if (complex)
            {
                if (g == 0)
                {
                    yield return SolutionItem.WithEquation("solve", new[]
                    {
                        EquationItem.With("x")
                    }, new[]
                    {
                        EquationItem.With("{0}", -e),
                    });
                }
                else
                {
                    yield return SolutionItem.With("solve", new FormattableItem[]
                    {
                        Equation.With(new[]
                        {
                            EquationItem.With("x")
                        }, new[]
                        {
                            EquationItem.With("{0}", -e),
                            EquationItem.With("{0}", complex, g)
                        }),
                        TextItem.With(", "),
                        Equation.With(new[]
                        {
                            EquationItem.With("x")
                        }, new[]
                        {
                            EquationItem.With("{0}", -e),
                            EquationItem.With("-{0}", complex, g)
                        })
                    });
                }

                yield break;
            }

            var rs = new[] { -e - g, -e + g }
                .OrderBy(x => x)
                .ToArray();

            if (g == 0)
            {
                yield return SolutionItem.WithEquation("solve", new[]
                {
                    EquationItem.With("x")
                }, new[]
                {
                    EquationItem.With("{0}", complex, rs[0])
                });
            }
            else
            {
                yield return SolutionItem.With("solve", new FormattableItem[]
                {
                    Equation.With(new[]
                    {
                        EquationItem.With("x")
                    }, new[]
                    {
                        EquationItem.With("{0}", complex, rs[0])
                    }),
                    TextItem.With(", "),
                    Equation.With(new[]
                    {
                        EquationItem.With("x")
                    }, new[]
                    {
                        EquationItem.With("{0}", complex, rs[1])
                    })
                });
            }
        }

        private string GetMessageForSharing()
        {
            //var message = SolutionItem.WithEquation("initial equation", new[]
            //{
            //    EquationItem.With("{0}x²", _mainModel.ParameterA),
            //    EquationItem.With("{0}x", _mainModel.ParameterB),
            //    EquationItem.With("{0}", _mainModel.ParameterC)
            //}).ToString() + ", ";

            //if (_solutions == null || _solutions.Length == 0)
            //{
            //    return message + "no solutions";
            //}
            //else
            //{
            //    return message + string.Join(" and ", _solutions.Select(x => "x = {0}".FormatWithInvariantCulture(x)));
            //}

            var solutionItems = GetSolution()
                .ToArray();

            return solutionItems.First() + ", " + solutionItems.Last();
        }
    }
}