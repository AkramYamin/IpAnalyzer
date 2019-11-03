using System;

namespace IpAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            IpAnalyzer ip;
            // valid ip in hex 127.0.0.1
            ip = new IpAnalyzer("0x7f000001");
            // valid ip in hex  192.168.0.0
            ip = new IpAnalyzer("0xc0a80000");
            // valid ip in hex 255.255.255.255
            ip = new IpAnalyzer("0xffffffff");

            // Invalid ip in hex 
            ip = new IpAnalyzer("0xfffffff");
            // Invalid ip in hex 
            ip = new IpAnalyzer("0xfffffgff");
            // Invalid ip in hex 
            ip = new IpAnalyzer("0xfffffffkk");



            // try to ping google
            ip = new IpAnalyzer("8.8.8.8");
            Console.WriteLine(ip.IsAlive());
        }
    }
}
