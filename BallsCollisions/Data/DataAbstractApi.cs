using System.Collections.ObjectModel;
using Logic;

namespace Data
{

    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateDataApi(float radious,float mass)
        {
            return new DataLayer(radious,mass);
        }
        public DataAbstractApi()
        {

        }
        public abstract void generateBalls(int _amount);
        public abstract ObservableCollection<Balls> getBalls();

    }
    public class DataLayer : DataAbstractApi
    {
        private float _ballradious;
        private float _ballmass;
        private Board board;
   


        public DataLayer( float radious, float mass)
        {
   
            this._ballradious = radious;
            this._ballmass = mass;
            board = new Board();
        }

        public override void generateBalls(int _amount)
        {
            board.GenerateBalls(_amount,_ballradious,_ballmass);
        }

        public override ObservableCollection<Balls> getBalls()
        {
            return board.Balls;
        }
    }
}