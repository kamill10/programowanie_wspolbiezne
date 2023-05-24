using System;
using System.IO;
using System.Threading;
using System.Timers;
using Logic;
using Newtonsoft.Json.Linq;

namespace Data
{
    public class BallsLogger : AbstractBallsLogger, IDisposable
    {
        private string _path;
        private readonly JObject _logObject;
        private readonly Mutex _fileMutex = new Mutex();
        private System.Timers.Timer _writeTimer;

        public BallsLogger()
        {
            string tempPath = Path.GetTempPath();
            _path = Path.Combine(tempPath, "BallsLog.json");
            _logObject = new JObject();
            _logObject["Balls"] = new JArray(); 
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

        public void Logging(Balls ball)
        {
            JObject ballJson = ConvertBallToJson(ball);
            ballJson["Time"] = DateTime.Now.ToString("HH:mm:ss");

            _fileMutex.WaitOne();
            try
            {
                ((JArray)_logObject["Balls"]).Add(ballJson); 
                File.WriteAllText(_path, _logObject.ToString());
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

        private void WriteTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _fileMutex.WaitOne();
            try
            {
                File.WriteAllText(_path, _logObject.ToString());
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



