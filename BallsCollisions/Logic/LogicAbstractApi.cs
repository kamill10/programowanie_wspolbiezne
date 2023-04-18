using System.Numerics;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        //refer to Data
        public static LogicAbstractApi CreateLogicAPI(DataAbstractApi data )
        {
            return new LogicApi(data);
        }
      
        public abstract void TaskRun(Board board);
        public abstract void TaskStop(Board board);
    }
    public class LogicApi : LogicAbstractApi
    {
        private List<Thread> _tasks = new List<Thread>();
        private CancellationToken _cancelToken;
        DataAbstractApi data;

        public LogicApi()
        {
        }

        public LogicApi(DataAbstractApi data)
        {
            this.data = data;
        }

        public CancellationToken CancellationToken => _cancelToken;


        public override void TaskRun(Board board)
        {

            //So we must get allBalls from Board and Update their position using Tasks
            foreach (var ball in board.Balls)
            {
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        if (_cancelToken.IsCancellationRequested)
                        {
                            break;
                        }
                        ball.ChangePosition();
                    }
                });
                thread.Start();
                _tasks.Add(thread);
            }
        }


        public override void TaskStop(Board board)
        {
            board.Balls.Clear();
            _tasks.Clear();
        }


    }
}
