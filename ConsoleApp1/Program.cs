using System;
using WorkUaApiCore;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Api api = new Api("d29ya3VhQGtsaW9iYS5jb206S1V5QWEkUXU0RkI4VyN0MA==");
            api.GetJobs();
            api.GetResponses();
            ;
            //Console.WriteLine("Hello World!");
        }
    }
}
