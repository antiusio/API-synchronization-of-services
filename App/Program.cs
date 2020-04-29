using System;
using System.IO;
using DataProcessing;

namespace NetCore.Docker
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputDataFile = Directory.GetCurrentDirectory() + @"/Data/InputData.json";

            Worker w = new Worker(inputDataFile);
            w.Work();
            Console.WriteLine("New Responses: {0}", w.NewResponses);
            Console.ReadKey();
        }
    }
}
