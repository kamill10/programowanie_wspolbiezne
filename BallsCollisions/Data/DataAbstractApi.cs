using System.Collections.ObjectModel;
using Logic;

namespace Data
{

    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateDataApi(int amount,float radious,float mass)
        {
            return new DataLayer(amount,radious,mass);
        }
        public DataAbstractApi()
        {

        }
        public abstract void generateBalls();
        public abstract ObservableCollection<Balls> getBalls();

    }
    public class DataLayer : DataAbstractApi
    {
        private int _amount;
        private float _ballradious;
        private float _ballmass;
        private Board board;
   


        public DataLayer(int amount, float radious, float mass)
        {
            this._amount = amount;
            this._ballradious = radious;
            this._ballmass = mass;
            board = new Board();
        }

        public override void generateBalls()
        {
            board.GenerateBalls(_amount,_ballradious,_ballmass);
        }

        public override ObservableCollection<Balls> getBalls()
        {
            return board.Balls;
        }
    }
}