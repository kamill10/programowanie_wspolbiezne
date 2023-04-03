using System.Windows;
using Presentation.ViewModel;

namespace BallsCollisions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewApi(BallCanvas);
        }


    }
}
