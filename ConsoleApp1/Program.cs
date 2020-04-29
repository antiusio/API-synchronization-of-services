using System;
using JiraApiCore;
using WorkUaApiCore;
using DataProcessing;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputDataFile = Directory.GetCurrentDirectory() + @"\Data\InputData.json";
            
            Worker w = new Worker(inputDataFile);
            w.Work();
            Console.WriteLine("New Responses: {0}",w.NewResponses);
            Console.ReadKey();
            //https://hooks.zapier.com/hooks/catch/6241182/o5je7gk/
        }
        
    }
}
