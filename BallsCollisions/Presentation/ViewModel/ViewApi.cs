using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Presentation.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Presentation.ViewModel
{
    
    public class ViewApi : INotifyPropertyChanged
    {
        private readonly NameScope _nameScope = new NameScope();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ModelAbstractApi modelApi = ModelAbstractApi.CreateModelApi();
        public ICommand ClickButton { get; set; }
        public ICommand ExitClick { get; set; }
        private int _ballsAmount;
 


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

        public ViewApi()
        {
            ClickButton = new RelayCommand(OnClickButton);
            ExitClick = new RelayCommand(OnExitClick);
            
        }

        private  void OnClickButton()
        {

            modelApi.CreateBalls(_ballsAmount);


            modelApi.TaskRun();
           
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


    }

    }

