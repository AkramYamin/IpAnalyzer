using System;

namespace IpAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            IpAnalyzer ip = new IpAnalyzer(0x7f000001);
            Console.WriteLine(ip.IsValidIp());
        }
    }
}

