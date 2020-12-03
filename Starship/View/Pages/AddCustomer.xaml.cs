﻿using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using Starship.ViewModel;
using System.Windows.Controls;

namespace Starship.View.Pages
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>

    public partial class AddCustomer : UserControl
    {
        public AddCustomer()
        {
            InitializeComponent();
            DataContext = new AddCustomerViewModel();
        }

    }
}
