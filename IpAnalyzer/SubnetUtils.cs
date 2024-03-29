﻿using System;
using System.Collections.Generic;

namespace IpAnalyzer
{
    public class SubnetUtils
    {

        private static Dictionary<int, string> CIDRSubnetMask = new Dictionary<int, string>()
        {
            { 1, "128.0.0.0" },
            { 2, "192.0.0.0" },
            { 3, "224.0.0.0" },
            { 4, "240.0.0.0" },
            { 5, "248.0.0.0" },
            { 6, "252.0.0.0" },
            { 7, "254.0.0.0" },
            { 8, "255.0.0.0" },
            { 9, "255.128.0.0" },
            { 10, "255.192.0.0" },
            { 11, "255.224.0.0" },
            { 12, "255.240.0.0" },
            { 13, "255.248.0.0" },
            { 14, "255.252.0.0" },
            { 15, "255.254.0.0" },
            { 16, "255.255.0.0" },
            { 17, "255.255.128.0" },
            { 18, "255.255.192.0" },
            { 19, "255.255.224.0" },
            { 20, "255.255.240.0" },
            { 21, "255.255.248.0" },
            { 22, "255.255.252.0" },
            { 23, "255.255.254.0" },
            { 24, "255.255.255.0" },
            { 25, "255.255.255.128" },
            { 26, "255.255.255.192" },
            { 27, "255.255.255.224" },
            { 28, "255.255.255.240" },
            { 29, "255.255.255.248" },
            { 30, "255.255.255.252" },
            { 31, "255.255.255.254" },
            { 32, "255.255.255.255" }
        };

        private static Dictionary<string, int> SubnetMaskCIDR = new Dictionary<string, int>()
        {
            { "128.0.0.0", 1 },
            { "192.0.0.0", 2 },
            { "224.0.0.0", 3 },
            { "240.0.0.0", 4 },
            { "248.0.0.0", 5 },
            { "252.0.0.0", 6 },
            { "254.0.0.0", 7 },
            { "255.0.0.0", 8 },
            { "255.128.0.0", 9 },
            { "255.192.0.0", 10 },
            { "255.224.0.0", 11 },
            { "255.240.0.0", 12 },
            { "255.248.0.0", 13 },
            { "255.252.0.0", 14 },
            { "255.254.0.0", 15 },
            { "255.255.0.0", 16 },
            { "255.255.128.0", 17 },
            { "255.255.192.0", 18 },
            { "255.255.224.0", 19 },
            { "255.255.240.0", 20 },
            { "255.255.248.0", 21 },
            { "255.255.252.0", 22 },
            { "255.255.254.0", 23 },
            { "255.255.255.0", 24 },
            { "255.255.255.128", 25 },
            { "255.255.255.192", 26 },
            { "255.255.255.224", 27 },
            { "255.255.255.240", 28 },
            { "255.255.255.248", 29 },
            { "255.255.255.252", 30 },
            { "255.255.255.254", 31 },
            { "255.255.255.255", 32 }
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
            return SubnetMaskCIDR[subnetMask];
        }


        /// <summary>
        /// convert subnet mask to binary 
        /// </summary>
        /// <param name="subnet">subnet mask string</param>
        /// <returns>binary format for subnet mask</returns>
        public static string IpToBinary(string subnet)
        {
            var result = "";
            foreach (var octet in subnet.Split('.'))
            {
                result += Convert.ToString(int.Parse(octet), 2).PadLeft(8, '0') + ".";
            }
            return result.Substring(0, result.Length - 1);
        }


        /// <summary>
        /// convert fro cidr to subnet mask
        /// </summary>
        /// <param name="cidr">cidr</param>
        /// <returns>subnet mask</returns>
        public static string CIDRToSubnetMask(int cidr)
        {
            return CIDRSubnetMask[cidr];
        }
    }
}