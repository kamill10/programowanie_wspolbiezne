using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public abstract   class LogicAbstractApi
    {
        //refer to Data
        public static LogicAbstractApi CreateLogicAPI()
        {
            return new LogicApi();
        }
        public abstract Balls GenerateBalls(Vector2 position,int radious);
        public abstract Board GenerateBoard(int ballnumber);
        public abstract void TaskRun(Board board);
        public abstract void TaskStop(Board board);
    }
    public class LogicApi : LogicAbstractApi
    {
        private List<Task> _tasks = new List<Task>();
        private CancellationToken _cancelToken;

        public LogicApi()
        {
            
        }

        public CancellationToken CancellationToken => _cancelToken;
        public override Balls GenerateBalls(Vector2 position, int radious)
        {
            return new Balls(position, radious);
        }

        public override Board GenerateBoard(int ballnumber)
        {
            return new Board(ballnumber);
        }

        public override void TaskRun(Board board)
        {
            //So we must get allBalls from Board and Update their position using Tasks
            foreach(var balls in board.Balls)
            {
                Task task = Task.Run(() =>
                {
                    Thread.Sleep(1);
                    while (true)
                    {
                        Thread.Sleep(10);
                        try
                        {
                            _cancelToken.ThrowIfCancellationRequested();
                        }
                        catch(OperationCanceledException)
                        {
                            break;
                        }
                        balls.ChangePosition();
                    }

                }
                );
                _tasks.Add(task);
            }
        }

        public override void TaskStop(Board board)
        {
            _tasks.Clear();
            board.Balls.Clear();
        }

       
    }
}
