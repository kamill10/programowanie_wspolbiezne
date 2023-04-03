using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Presentation.Model;

namespace Presentation.ViewModel
{

    public class ViewApi : INotifyPropertyChanged, System.Windows.Markup.INameScope
    {
        private readonly NameScope _nameScope = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ModelAbstractApi modelApi = ModelAbstractApi.CreateModelApi();
        public ICommand ClickButton { get; set; }
        public ICommand ExitClick { get; set; }
        private int _ballsAmount;
        private Canvas _ballCanvas { get; set; }


        public int BallsAmount
        {
            get => _ballsAmount;
            set
            {
                _ballsAmount = value;
                // Add any additional logic here when the text value is changed
                OnPropertyChanged(nameof(BallsAmount));
            }
        }

        public ViewApi(Canvas ballCanvas)
        {
            ClickButton = new RelayCommand(OnClickButton);
            ExitClick = new RelayCommand(OnExitClick);
            _ballCanvas = ballCanvas;
        }

        private async void OnClickButton()
        {
            Console.WriteLine("test");
            modelApi.CreateBalls(_ballsAmount);
            Console.WriteLine("test");
            Ellipse ellipse = new()
            {
                Width = 25,
                Height = 25,
                Fill = Brushes.Blue,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            _ballCanvas.Children.Add(ellipse);

            modelApi.TaskRun();
            while (true)
            {
                // Update the positions of the balls on the canvas
                foreach (var ball in modelApi.GetBalls())
                {

                    Canvas.SetLeft(ellipse, ball.Position.X);
                    Canvas.SetTop(ellipse, ball.Position.Y);
                }

                // Sleep for a short period of time to avoid overwhelming the UI thread
                await Task.Delay(10);
            }
        }

        private void OnExitClick()
        {
            modelApi.TaskStop();

        }
        public object FindName(string name)
        {
            return _nameScope.FindName(name);
        }

        public void RegisterName(string name, object scopedElement)
        {
            _nameScope.RegisterName(name, scopedElement);
        }

        public void UnregisterName(string name)
        {
            _nameScope.UnregisterName(name);
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private System.Collections.IEnumerable ellipses;

        public System.Collections.IEnumerable Ellipses { get => ellipses; set => SetProperty(ref ellipses, value); }

        private double viewHeight;

        public double ViewHeight { get => viewHeight; set => SetProperty(ref viewHeight, value); }

        private double viewWidth;

        public double ViewWidth { get => viewWidth; set => SetProperty(ref viewWidth, value); }


    }

}

