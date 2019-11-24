using System;

namespace IpAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            IpAnalyzer ip = new IpAnalyzer("213.6.2.253", 30);
            Console.WriteLine("Networkaddress : " + ip.GetNetworkAddress());
            Console.WriteLine("Broadcast address : " + ip.GetBroadcastAddress());
            Console.WriteLine("Total number of hosts : " + ip.GetNumberOfHosts());
            Console.WriteLine("number of usable hosts : " + ip.GetNumberOfValidHosts());
            Console.WriteLine("number of usable hosts : " + ip.GetNumberOfValidHosts());
            Console.WriteLine(ip.ToString());
            Console.WriteLine("Wildcard mask : " + ip.GetWildcardMask());
            Console.WriteLine("Binary ip address : " + ip.GetBinaryNotaion());
            Console.WriteLine("Class : " + ip.GetClass());
            Console.WriteLine("CIDR notation : " + ip.GetCIDRNotation());
            Console.WriteLine("Is public : " + (ip.IsPublic()));
            Console.WriteLine("Is private : " + (ip.IsPrivate()));
            Console.WriteLine("Short : " + ip.GetShort());
            Console.WriteLine("Hex : " + (ip.ip));
            Console.WriteLine("\n\n\n\n\n");
            foreach (var subnet in ip.GetSubnets()) 
            {
                Console.WriteLine(subnet.NetworkAddress + "           " + subnet.GetFirstUsableIp() + " -- " + subnet.GetLastUsableIp() + "    " + subnet.BroadcastAddress);
            }
        }
    }
}

