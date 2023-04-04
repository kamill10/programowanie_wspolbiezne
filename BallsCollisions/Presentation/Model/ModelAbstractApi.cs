﻿using System.Collections.ObjectModel;
using Logic;

namespace Presentation.Model
{
    public abstract class ModelAbstractApi
    {


        public static ModelApi CreateModelApi()
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
        Board board = new();


        public override void CreateBalls(int amount)
        {
            board.GenerateBalls(amount);
        }

        public override ObservableCollection<Balls> Balls()
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
