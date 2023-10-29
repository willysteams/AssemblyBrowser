using System.Collections.ObjectModel;
using System.Windows;
using AssemblyBrowserCore;

namespace AssemblyBrowserGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }
    }
}