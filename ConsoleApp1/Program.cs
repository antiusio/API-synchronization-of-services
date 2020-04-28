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
            Worker w = new Worker();
            w.Work();
            
        }
    }
}
