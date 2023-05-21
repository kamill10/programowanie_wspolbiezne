using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq.Expressions;

namespace Data
{
    public class BallsLogger : AbstractBallsLogger
    {
        private string _path;
        private ConcurrentQueue<JObject> loggingQueue = new ConcurrentQueue<JObject>();
        private Mutex mutex = new Mutex();
        private readonly JArray _logArray;
        private readonly Mutex _ballsMutex = new Mutex();
        private readonly Mutex _fileMutex = new Mutex();

        public BallsLogger()
        {
            string tempPath = Path.GetTempPath();
            _path = tempPath + "BallsLog.json";
            _logArray = new JArray();
            if (!File.Exists(_path))
            {
                FileStream myFile = File.Create(_path);
                myFile.Close();
            }     
        }

        public void EnqueueToLoggingQueue(Balls ball)
        {
            JObject ballJson = ConvertBallToJson(ball);
            loggingQueue.Enqueue(ballJson);
            ProcessLoggingQueue();
        }

        private void ProcessLoggingQueue()
        {
            while (loggingQueue.TryDequeue(out JObject ballJson))
            {
                _ballsMutex.WaitOne();
                _fileMutex.WaitOne();
                try
                {                   
                    ballJson["Time"] = DateTime.Now.ToString("HH:mm:ss");

                    _logArray.Add(ballJson);

                    File.WriteAllText(_path, _logArray.ToString());
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error writing to the log file: " + ex.Message);
                }
                finally
                {
                    _fileMutex.ReleaseMutex();
                    _ballsMutex.ReleaseMutex();
                }
            }
        }

        private JObject ConvertBallToJson(Balls ball)
        {
            // Implement the logic to convert the Balls object to a JObject (JSON object)
            // You can use a library like Newtonsoft.Json to conveniently perform the conversion

            // Sample implementation using Newtonsoft.Json library:
            JObject ballJson = JObject.FromObject(ball);
            return ballJson;
        }

        ~BallsLogger()
        {
            _fileMutex.WaitOne();
            _fileMutex.ReleaseMutex();
        }

    }
}
