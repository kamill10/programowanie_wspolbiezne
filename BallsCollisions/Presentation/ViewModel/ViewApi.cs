﻿using System;
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

        private async   void OnClickButton()
        {
           
            modelApi.CreateBalls( _ballsAmount);
            Console.WriteLine("test");
            Ellipse ellipse = new Ellipse();

            modelApi.TaskRun();
            while (true)
            {
                // Update the positions of the balls on the canvas
                foreach (var ball in modelApi.GetBalls())
                {
                    ellipse.Width =ball.Radious ;
                    ellipse.Height = ball.Radious;
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
       

    }

    }
