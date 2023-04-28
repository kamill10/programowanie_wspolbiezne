using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Data;
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

        public ICommand ClickButton { get; set; }
        public ICommand ExitClick { get; set; }
        private int _ballsAmount;
        public System.Collections.Generic.IList<Balls> _Balls { get; set; }
        Random random = new Random();
        private ModelAbstractApi modelApi;


        public int BallsAmount
        {
            get => _ballsAmount;
            set
            {
                _ballsAmount = value;

            }

        }
        public System.Collections.Generic.IList<Balls> Balls
        {
            get => _Balls;
            set
            {
                _Balls = value;
            }
        }

        public ViewApi()
        {
            modelApi = ModelAbstractApi.CreateModelApi(DataAbstractApi.CreateDataApi(15, 3, 600));
            _Balls = getBalls();
            ClickButton = new RelayCommand(OnClickButton);
            ExitClick = new RelayCommand(OnExitClick);


        }

        private void OnClickButton()
        {
            modelApi.CreateBall(_ballsAmount);

            if (modelApi.GetBalls().Count == 0)
            {
                throw new NullReferenceException("brak pilek");
            }
           

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

