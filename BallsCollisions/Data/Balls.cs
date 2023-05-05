using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Data;

namespace Data
{
    public class Balls : INotifyPropertyChanged
    {
        private Vector2 _valocity;
        private Vector2 _position;
        private float _speed;
        private float _radious;
        private float _mass;

        public float Speed
        {
            get => _speed; set => _speed = value;
        }
        
        public Balls(float speed, float radious, float mass)
        {
            _radious = radious;
            _mass = mass;
            _speed = speed;
        }

        public Balls(Vector2 position, float radious)
        {
            _position = position;
            _radious = radious;
        }

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

        public float Mass
        {
            get => _mass;
            set => _mass = value;
        }

       
        public float X
        {
            get { return _position.X; }
            set => _position.X = value;

        }
        public float Y
        {
            get { return _position.Y; }
            set=>_position.Y =value;
        }
        public Vector2 Valocity
        {
            get => _valocity;
            set => _valocity = value;

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task ChangePosition()
        {
            Position += new Vector2(_valocity.X * _speed, _valocity.Y * _speed);
            if (_position.X + 5 <= 0)
            {
                _valocity = new Vector2(-Valocity.X, Valocity.Y);
                 X += 4*_valocity.X * _speed;
               // X += _radious;
            }
            if (_position.X >= Board._boardWidth - _radious)
            {
                _valocity = new Vector2(-Valocity.X, Valocity.Y);
                X += 4*_valocity.X * _speed;
                //X -= _radious;
            }
            if (_position.Y + 5 <= 0)
            {
                _valocity = new Vector2(Valocity.X, -Valocity.Y);
                Y += 4*_valocity.Y * _speed;
                //Y += _radious;
            }
            if ( _position.Y >= Board._boardHeight - _radious)
            {
                _valocity = new Vector2(Valocity.X, -Valocity.Y);
                 Y +=4* _valocity.Y * _speed;
                //Y -= _radious;
            }

            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
            await Task.Delay(4);
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