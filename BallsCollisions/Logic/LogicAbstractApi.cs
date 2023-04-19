using System.Numerics;
using System.Threading;
using Data;
using Microsoft.VisualBasic;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        //refer to Data
        public static LogicAbstractApi CreateLogicAPI(DataAbstractApi data )
        {
            return new LogicApi(data);
        }
      
        public abstract void TaskRun();
        public abstract void TaskStop();
    }
    public class LogicApi : LogicAbstractApi
    {
        private List<Thread> _tasks = new List<Thread>();
        private CancellationToken _cancelToken;
        DataAbstractApi data;
        private bool isCancelled = false;

   

        public LogicApi(DataAbstractApi data)
        {
            this.data = data;
        }

        public CancellationToken CancellationToken => _cancelToken;


        public override void TaskRun()
        {
            isCancelled = false ;
            if(data.getBalls().Count == 0)
            {
                throw new ArgumentNullException("brak pilek w logic");
            }
            //So we must get allBalls from Board and Update their position using Tasks
            foreach (var ball in data.getBalls())
            {
                Thread thread = new Thread(() =>
                {
                    while (!isCancelled)
                    {
                        ball.ChangePosition();
                        Thread.Sleep(10);
                    }
                });
                thread.Start();
                _tasks.Add(thread);
            }
        

        }

    
        public override void TaskStop()
        {
            isCancelled = true;
            data.getBalls().Clear();
            foreach (Thread thread in _tasks)
            {
                thread.Join();
            }
            _tasks.Clear();
        }


    }
}
