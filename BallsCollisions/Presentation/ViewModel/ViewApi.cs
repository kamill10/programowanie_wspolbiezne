using System;
using System.Collections;
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
        private ModelAbstractApi modelApi = ModelAbstractApi.CreateModelApi(DataAbstractApi.CreateDataApi(15,3));



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

