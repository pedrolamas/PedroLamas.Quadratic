using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PedroLamas.Quadratic.ViewModel;

namespace PedroLamas.Quadratic.View
{
    public partial class SolutionPage : PhoneApplicationPage
    {
        public SolutionPage()
        {
            InitializeComponent();

            ShareTypeListPicker.ItemsSource = new[] { "", "Email Message", "SMS Message", "Social Networks" };
        }

        private void ShareApplicationBarItem_OnClick(object sender, EventArgs e)
        {
            ShareTypeListPicker.Open();
        }

        private void ShareTypeListPicker_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (SolutionViewModel)DataContext;

            switch (ShareTypeListPicker.SelectedIndex)
            {
                case 1:
                    vm.ShareByEmailCommand.Execute(null);
                    break;
                case 2:
                    vm.ShareBySmsCommand.Execute(null);
                    break;
                case 3:
                    vm.ShareOnSocialNetworkCommand.Execute(null);
                    break;
            }

            ShareTypeListPicker.SelectedIndex = 0;
        }
    }
}