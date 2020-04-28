using System;
using JiraApiCore;
using WorkUaApiCore;
using DataProcessing;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker w = new Worker("d29ya3VhQGtsaW9iYS5jb206S1V5QWEkUXU0RkI4VyN0MA==");
            w.Work();

            //ApiWork api = new ApiWork("d29ya3VhQGtsaW9iYS5jb206S1V5QWEkUXU0RkI4VyN0MA==");
            //api.GetJobs();
            //api.GetResponses();
            //ApiJira apiJira = new ApiJira();
            //apiJira.AddTextRespToJira(api.Responses[0].ToString());
            //;
            //Console.WriteLine("Hello World!");
        }
    }
}
