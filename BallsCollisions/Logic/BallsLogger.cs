﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Timers;
using Data;
using Newtonsoft.Json.Linq;


namespace Logic
{
    public class BallsLogger : AbstractBallsLogger, IDisposable
    {
        private string _path;
        private ConcurrentQueue<JObject> loggingQueue = new ConcurrentQueue<JObject>();
        private readonly JArray _logArray;
        private readonly Mutex _fileMutex = new Mutex();
        private System.Timers.Timer _writeTimer;

        public BallsLogger()
        {
            string tempPath = Path.GetTempPath();
            _path = Path.Combine(tempPath, "BallsLog.json");
            _logArray = new JArray();
            if (!File.Exists(_path))
            {
                FileStream myFile = File.Create(_path);
                myFile.Close();
            }

            _writeTimer = new System.Timers.Timer();
            _writeTimer.Interval = 10; 
            _writeTimer.Elapsed += WriteTimerElapsed;
            _writeTimer.Start();
        }

        public void EnqueueToLoggingQueue(Balls ball)
        {
            JObject ballJson = ConvertBallToJson(ball);
            loggingQueue.Enqueue(ballJson);
        }

        private void WriteTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _fileMutex.WaitOne();
            try
            {
                while (loggingQueue.TryDequeue(out JObject ballJson))
                {
                    ballJson["Time"] = DateTime.Now.ToString("HH:mm:ss");

                    _logArray.Add(ballJson);
                }

                File.WriteAllText(_path, _logArray.ToString());
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to the log file: " + ex.Message);
            }
            finally
            {
                _fileMutex.ReleaseMutex();
            }
        }

        private JObject ConvertBallToJson(Balls ball)
        {
            JObject ballJson = JObject.FromObject(ball);
            return ballJson;
        }

        public void Dispose()
        {
            _writeTimer.Stop(); 
            _writeTimer.Dispose();
        }
    }
}

