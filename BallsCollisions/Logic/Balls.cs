﻿using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Balls : INotifyPropertyChanged
    {
        private Vector2 _valocity;
        private Vector2 _position;
<<<<<<< HEAD
        private static int _speed = 1000;
=======
        private int _speed;
>>>>>>> 485ec004485d741a6c6438375cc32ecffb347ba2
        private float _radious;
        private float _X;
        private float _Y;

        public int Speed
        {
            get => _speed; set => Speed = value;
        }

        public Balls(Vector2 position, float radious)
        {
            _position = position;
            _radious = radious;
        }
        public Balls(float radious)
        {
            _radious = radious;
        }

        public Balls() { }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        public float Radious
        {
            get => _radious;
            set => _radious = value;
        }
        public float X
        {
            get { return _position.X; }
            set => X = value;
        }
        public float Y
        {
            get { return _position.Y; }
            set=>Y=value;
        }
        public Vector2 Valocity
        {
            get => _valocity;
            set => _valocity = value;

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void ChangePosition()
        {
            Position += new Vector2(_valocity.X * _speed, _valocity.Y * _speed);
            if (_position.X < _radious-10|| _position.X > Board._boardWidth - _radious)
            {
                _valocity.X = -_valocity.X;
            }
            if (_position.Y < _radious || _position.Y > Board._boardHeight - _radious)
            {
                _valocity.Y = -_valocity.Y;
            }
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));

        }


        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

}