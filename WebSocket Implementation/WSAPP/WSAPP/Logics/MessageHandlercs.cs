using System.Text;
using WSAPP.Services;

namespace WSAPP.Logics
{
    public class MessageHandlercs: IMessageBuilderService
    {
        public static int WorkerID { get; set; }
        public IEnumerable<byte> PrepareMesage(string Message, int RandomFlag)
        {
          

        var arraysegment = new ArraySegment<byte>();

            if (RandomFlag == 0)
            {
                var ping_message = Message;
                var bytes = Encoding.UTF8.GetBytes(ping_message);
                arraysegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

            }

            else if (RandomFlag == 1)
            {
                try
                {
                    Random rnd = new Random();
                    WorkerID = rnd.Next(10, 20);
                    var worker_message = String.Concat(Message, "_", WorkerID);
                    var bytes = Encoding.UTF8.GetBytes((string)worker_message);
                    arraysegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

            }

            else if (RandomFlag == 2)
            {
                var worker_message = String.Concat(Message, "_", WorkerID);
                var bytes = Encoding.UTF8.GetBytes((string)worker_message);
                arraysegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

            }

            return arraysegment;

        }
    }
}
