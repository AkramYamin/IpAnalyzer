using System.Collections.Generic;

namespace IpAnalyzer
{
    public class Subnet
    {
        public string NetworkAddress { get; set; }
        public string BroadcastAddress { get; set; }

        private string firstUsableIp;

        public string GetFirstUsableIp()
        {
            return firstUsableIp;
        }

        private string lastUsableIp;

        public string GetLastUsableIp()
        {
            return lastUsableIp;
        }


        /// <summary>
        /// create new subnet 
        /// </summary>
        /// <param name="networkAddress">network address for this subnet </param>
        /// <param name="broadcastAddress">broadcast address for this subnet </param>
        /// <param name="firstUsableIp">first usable ip for this subnet </param>
        /// <param name="lastUsableIp">last usable ip for this subnet </param>
        public Subnet(string networkAddress, string broadcastAddress)
        {
            BroadcastAddress = broadcastAddress;
            NetworkAddress = networkAddress;
            firstUsableIp = IpAnalyzer.GetNextIp(NetworkAddress);
            lastUsableIp = IpAnalyzer.GetPreviousIp(broadcastAddress);
        }


        /// <summary>
        /// Get list of all usable ip addresses for this subnet
        /// </summary>
        /// <returns>List of usable ip's </returns>
        public List<string> GetUsableHosts()
        {
            List<string> result = new List<string>();
            string next = IpAnalyzer.GetNextIp(GetFirstUsableIp());
            while(next != GetLastUsableIp())
            {
                result.Add(next);
                next = IpAnalyzer.GetNextIp(next);
            }
            return result;
        }
    }
}
