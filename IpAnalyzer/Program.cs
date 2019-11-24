using System;

namespace IpAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            IpAnalyzer ip = new IpAnalyzer(0xffffffff);
            Console.WriteLine(ip.IsAlive());
            Console.WriteLine(ip.GetBinaryNotaion());

        }
    }
}

