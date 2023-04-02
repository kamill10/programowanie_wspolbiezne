using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Presentation.Model
{
    public abstract  class ModelAbstractApi 
    {
      

        public static  ModelApi  CreateModelApi()
        {
            return new ModelApi();
        }
        public abstract void CreateBalls(int amount);
        public abstract void TaskRun();
        public abstract void TaskStop();
        public abstract ObservableCollection<Balls> GetBalls();
       

    }
    public class ModelApi : ModelAbstractApi
    {
        LogicAbstractApi logicApi = LogicAbstractApi.CreateLogicAPI();
        Board board = new Board();


        public override void CreateBalls(int amount)
        {
            board.GenerateBalls(amount);
     

        }

        public override ObservableCollection<Balls> GetBalls()
        {
            return board.Balls;
        }

        public override void TaskRun()
        {
            logicApi.TaskRun(board);
        }

        public override void TaskStop()
        {
            logicApi.TaskStop(board);
        }
        
    }
}
