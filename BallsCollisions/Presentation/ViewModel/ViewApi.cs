using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Logic;
using Presentation.Model;

namespace Presentation.ViewModel
{

    public class ViewApi : INotifyPropertyChanged
    {
        private readonly NameScope _nameScope = new();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        readonly ModelAbstractApi modelApi = ModelAbstractApi.CreateModelApi();
        public ICommand ClickButton { get; set; }
        public ICommand ExitClick { get; set; }
        private int _ballsAmount;
        public System.Collections.Generic.IList<Balls> _Balls { get; set; }

       
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
        public System.Collections.Generic.IList<Balls> Balls
        {
            get => _Balls;
            set
            {
                _Balls = value;
                OnPropertyChanged(nameof(_Balls));
            }
        }

        public ViewApi()
        {
            ClickButton = new RelayCommand(OnClickButton);
            ExitClick = new RelayCommand(OnExitClick);
            _Balls = getBalls();
        }

        private void OnClickButton()
        {
            modelApi.CreateBalls(_ballsAmount);
            modelApi.TaskRun();

        }
        public System.Collections.Generic.IList<Balls> getBalls()
        {
            return modelApi.GetBalls();
        }

        private void OnExitClick()
        {
            modelApi.TaskStop();

        }
       

     

    }

}

