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
        private List<Task> _tasks = new List<Task>();
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


         public async  override void TaskRun(Board board)
          {
              //So we must get allBalls from Board and Update their position using Tasks
              foreach (var ball in board.Balls)
              {
                  Task task = Task.Run(async () =>
                  {
                      while (true)
                      {
                          await Task.Delay(10);
                          try
                          {
                              _cancelToken.ThrowIfCancellationRequested();
                          }
                          catch (OperationCanceledException)
                          {
                              break;
                          }
                          ball.ChangePosition();
                      }

                  }
                  );
                  _tasks.Add(task);
              }
          }

    
        public override void TaskStop(Board board)
        {
            board.Balls.Clear();
            _tasks.Clear();
        }


    }
}
