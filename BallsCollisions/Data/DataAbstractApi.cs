using System.Collections.ObjectModel;

namespace Data
{

    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateDataApi(float radious,float mass,float v )
        {
            return new DataLayer(radious,mass,v);
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
        private float _v;
        private Board board;
   


        public DataLayer( float radious, float mass,float v)
        {
   
            this._ballradious = radious;
            this._ballmass = mass;
            this._v = v;
            board = new Board();
        }

        public override void generateBalls(int _amount)
        {
            board.GenerateBalls(_amount,_ballradious,_ballmass,_v);
        }

        public override ObservableCollection<Balls> getBalls()
        {
            return board.Balls;
        }
    }
}