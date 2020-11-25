﻿using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Starship.ViewModel
{
    class SideMenuViewModel : MvxViewModel
    {
        ResourceDictionary dict = Application.LoadComponent(new Uri("/Starship;component/asserts/testicons.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
    }
    public class MenuItemsData
    {
        public PathGeometry PathData { get; set; }
        public string MenuText { get; set; }
        public List<SubMenuItemsData> SubMenuList { get; set; }

        public MenuItemsData()
        {
            Command = new CommandViewModel(Execute);
        }
        public ICommand Command { get; }

        private void Execute()
        {
            string MT = MenuText.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(MT))
                navigateToPage(MT);
        }
        private void navigateToPage(string Menu)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "/View/Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
  
    public class SubMenuItemsData
    {
        public PathGeometry PathData { get; set; }
        public string SubMenuText { get; set; }

        public SubMenuItemsData()
        {
            SubMenuCommand = new CommandViewModel(Execute);
        }
        public ICommand SubMenuCommand { get; }

        private void Execute()
        {
            string SMT = SubMenuText.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(SMT))
                navigateToPage(SMT);
        }
        private void navigateToPage(string Menu)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "View/Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }

}

