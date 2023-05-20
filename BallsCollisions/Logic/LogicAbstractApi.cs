using System.Collections.ObjectModel;
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
        public abstract  ObservableCollection<Balls> getBalls();
    }
    public class LogicApi : LogicAbstractApi
    {
        private List<Task> _tasks = new List<Task>();
        private CancellationToken _cancelToken;
        DataAbstractApi data;
        private bool isCancelled = false;
        private AbstractBallsLogger logger = new BallsLogger();
   

        public LogicApi(DataAbstractApi data)
        {
            this.data = data;
        }

        public CancellationToken CancellationToken => _cancelToken;

        public override ObservableCollection<Balls> getBalls()
        {
            return data.getBalls();
        }

        public override void TaskRun()
        {
            isCancelled = false ;
            if(data.getBalls().Count == 0)
            {
                throw new ArgumentNullException("brak pilek w logic");
            }
                foreach (var ball in data.getBalls())
                {
                   Task task= new Task(async () =>
                    {

                        while (!isCancelled)
                        {
                            await ball.ChangePosition();
                            lock (data)
                            {
                                BallService.Collide(ball, data.getBalls());
                            } 
                        }
                        logger.EnqueueToLoggingQueue(ball);
                    });
                task.Start();
                    _tasks.Add(task);
                }

        }

    
        public override void TaskStop()
        {
            isCancelled = true;
            data.getBalls().Clear();
          
            _tasks.Clear();
        }

    }
}
