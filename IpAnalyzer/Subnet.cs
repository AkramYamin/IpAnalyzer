using System.Collections.Generic;

namespace IpAnalyzer
{
    public class Subnet
    {

        private static Dictionary<int, string> CIDRSubnetMask = new Dictionary<int, string>()
        {
            { 10, "" }
        };

        private static Dictionary<string, int> SubnetMaskCIDR = new Dictionary<string, int>()
        {
            { "", 10 }
        };

        /// <summary>
        /// check if this subnet mask valid or not 
        /// </summary>
        /// <param name="subnetMask">subnet mask </param>
        /// <returns>valid or not </returns>
        public static bool IsValidSubnetMask(string subnetMask)
        {
            return SubnetMaskCIDR.ContainsKey(subnetMask);
        }

        /// <summary>
        /// check if this CIDR valid or not 
        /// </summary>
        /// <param name="cidr">subnet mask </param>
        /// <returns>valid or not </returns>
        public static bool IsValidCIDR(int cidr)
        {
            return CIDRSubnetMask.ContainsKey(cidr);
        }


        /// <summary>
        /// convert from subnet mask to CIDR 
        /// </summary>
        /// <param name="subnetMask">subnet mask</param>
        /// <returns>subnet as CIDR</returns>
        public static int SubnetMaskToCIDR(string subnetMask)
        {
            return 0;
        }


        /// <summary>
        /// convert fro cidr to subnet mask
        /// </summary>
        /// <param name="cidr">cidr</param>
        /// <returns>subnet mask</returns>
        public static string CIDRToSubnetMask(int cidr)
        {
            return "";
        }
    }
}