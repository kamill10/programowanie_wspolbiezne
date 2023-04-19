using System;
using System.Collections.ObjectModel;
using Data;
using Logic;

namespace Presentation.Model
{
    public abstract class ModelAbstractApi
    {


        public static ModelApi CreateModelApi(DataAbstractApi data )
        {
            return new ModelApi( data);
        }
        public abstract void CreateBalls();
        public abstract void TaskRun();
        public abstract void TaskStop();
        public abstract ObservableCollection<Balls> GetBalls();


    }
    public class ModelApi : ModelAbstractApi
    {

        private DataAbstractApi _data;
        private LogicAbstractApi logicApi;
       public  ModelApi(DataAbstractApi data)
        {
            _data = data;
            logicApi = LogicAbstractApi.CreateLogicAPI(_data);
        }


        public override void CreateBalls()
        {
            _data.generateBalls();
        }

        public override ObservableCollection<Balls> GetBalls()
        {
            return _data.getBalls();
        }

        public override void TaskRun()
        {
            if(_data.getBalls().Count == 0)
            {
                throw new NullReferenceException("brak pilek w model");
            }
            logicApi.TaskRun();
        }

        public override void TaskStop()
        {
            logicApi.TaskStop();
        }

    }
}
