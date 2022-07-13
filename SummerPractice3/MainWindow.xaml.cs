using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SummerPractice3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myButton.FontSize = 50;
            myButton.Content = "Hello there";

            myTextBlock.Text = "Hello from cs side!";
            myTextBlock.Foreground = Brushes.BlanchedAlmond;

            var codeBehindTB = new TextBlock();
            codeBehindTB.Text = "Hello world!";
            codeBehindTB.Inlines.Add(" This is added using Inlines!");
            codeBehindTB.Inlines.Add(new Run(" Run text that I added in Code behind")
            {
                Foreground = Brushes.Red,
                TextDecorations = TextDecorations.Underline
            });
            codeBehindTB.TextWrapping = TextWrapping.Wrap;


            //this.Content = codeBehindTB;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var processInfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
            processInfo.UseShellExecute = true;
            Process.Start(processInfo);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myLabel.FontSize += 1;
        }

        private void myButton2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            myLabel.Foreground = Brushes.Yellow;
        }
    }
}
