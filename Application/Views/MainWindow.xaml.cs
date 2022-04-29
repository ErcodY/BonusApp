using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application.Tools.Logger;

namespace Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logger.Init(@$"{Directory.GetCurrentDirectory()}\Logs\");
            Logger.Instance.Log(Logger.LogLevel.Info , "Application started");
            Logger.Instance.Log(Logger.LogLevel.Error , "Error");
            Logger.Instance.Log(Logger.LogLevel.Fatal , "Exit Error");
        }
    }
}