using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace IpAnalyzer
{
    public class IpAnalyzer : IComparable<IpAnalyzer>
    {
        public string ip { get; set; }
        public bool IsValid { get; set; }

        public string subnet { get; set; }

        /// <summary>
        /// create new ip in hex format and subnet mask 
        /// </summary>
        /// <param name="ip">ip in hex </param>
        /// <param name="cidr">subnet mask in cidr</param>
        public IpAnalyzer(uint ip, int cidr)
            :this(ip)
        {
            if (!SubnetUtils.IsValidCIDR(cidr))
            {
                throw new ArgumentException("Invalid CIDR", "cidr");
            }
            subnet = SubnetUtils.CIDRToSubnetMask(cidr);
        }



        /// <summary>
        /// create new ip and subnet mask 
        /// </summary>
        /// <param name="ip">ip </param>
        /// <param name="cidr">subnet mask in cidr</param>
        public IpAnalyzer(string ip, int cidr)
            : this(ip)
        {
            if (!SubnetUtils.IsValidCIDR(cidr))
            {
                throw new ArgumentException("Invalid CIDR", "cidr");
            }
            subnet = SubnetUtils.CIDRToSubnetMask(cidr);

        }



        /// <summary>
        /// create new ip in hex format and subnet mask 
        /// </summary>
        /// <param name="ip">ip in hex </param>
        /// <param name="subnetMask">subnet mask</param>
        public IpAnalyzer(uint ip, string subnetMask)
            : this(ip)
        {
            if (!SubnetUtils.IsValidSubnetMask(subnet))
            {
                throw new ArgumentException("Invalid subnet mask", "subnetMask");
            }
            subnet = subnetMask;
        }


        /// <summary>
        /// create new ip and subnet mask 
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="subnetMask">subnet mask</param>
        public IpAnalyzer(string ip, string subnetMask)
            : this(ip)
        {
            if (!SubnetUtils.IsValidSubnetMask(subnetMask))
            {
                throw new ArgumentException("Invalid subnet mask", "subnetMask");
            }
            subnet = subnetMask;
        }


        /// <summary>
        /// create new ip from hex format 
        /// </summary>
        /// <param name="ip">hex ip format</param>
        public IpAnalyzer(uint ip)
            : this( "0x" + ip.ToString("X"))
        {       }

        /// <summary>
        /// create new IP from normal ip format 
        /// </summary>
        /// <param name="ip">string ip format </param>
        public IpAnalyzer(string ip)
        {
            this.IsValid = IpValidator(ip);
            if (this.IsValid)
            {
                this.ip = IpParser(ip);
                Console.WriteLine(String.Format("new valid ip address created : {0}", this));
            } else
            {
                this.ip = "00000000";
                Console.WriteLine(String.Format("Invalid ip address created : {0}", this));
            }
        }
        
        /// <summary>
        /// check if parsed string valid ip or not 
        /// </summary>
        /// <param name="ip">ip string in normal or hex form </param>
        /// <returns>if parsed string can be an ip or not 'boolean'</returns>
        private static bool IpValidator(string ip)
        { 
            // if string contains '.'
            if(ip.Contains('.'))
            {
                string[] arr = ip.Split('.');
                int[] result = new int[arr.Length];
                // if its four actets
                if (arr.Length != 4)
                {
                    return false;
                }
                    for (int i = 0; i < arr.Length; i++)
                    {
                        //if each octet in range 0-255
                        if (int.TryParse(arr[i], out result[i]))
                        {
                        if (int.Parse(arr[i]) <= 255 && int.Parse(arr[i]) >= 0)
                        {
                            return true;
                        }
                        }
                            return false;
                    }
            }
            //if its in hex form 
            else if(ip.StartsWith("0X") || ip.StartsWith("0x"))
            {
                //if its length not equal 10
                if(ip.Length != 10)
                {
                    return false;
                }
                int[] arr = new int[4];
                ip = ip.Substring(2);
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        arr[i] = int.Parse(ip.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    ip = ip.Substring(2);
                }
                try
                {
                    arr[3] = int.Parse(ip, System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception)
                {
                    return false;
                }
                for (int i = 0; i < 4; i++)
                    {
                        //if each value between 0-255
                        if(arr[i] >= 0 &&
                            arr[i] <=255)
                        {
                            return true;
                        }
                    }
            }
                return false;
        }

        /// <summary>
        /// convert from string to IP instance 
        /// </summary>
        /// <param name="ip">ip string in normal or hex form </param>
        /// <returns>IP instance</returns>
        private string IpParser(string ip)
        {
            string _ip;
            //parse normal form ip
            if (ip.Contains('.'))
            {
                _ip = IpStringToHex(ip);
            } else
            // parse hex form ip
            {
                _ip = ip.Substring(2);
            }
            return _ip;
        }



        /// <summary>
        /// conver from normal ip format to hex format
        /// </summary>
        /// <param name="ip">ip in normal format</param>
        /// <returns>ip in hex format as string</returns>
        private string IpStringToHex(string ip)
        {
            string[] arr = ip.Split('.');
            var result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result += int.Parse(arr[i]).ToString("X2");
            }
            return result;
        }



        /// <summary>
        /// conver from hex ip format to string format
        /// </summary>
        /// <param name="ip">ip in hex format</param>
        /// <returns>ip in normal format as string</returns>
        private string IpHexToString(string ip)
        {
            int[] arr = new int[4];
            for (int i = 0; i < 3; i++)
            {
                arr[i] = int.Parse(ip.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                ip = ip.Substring(2);
            }
            arr[3] = int.Parse(ip, System.Globalization.NumberStyles.HexNumber);
            var result = arr[0].ToString() + "." + arr[1].ToString() + "." + arr[2].ToString() + "." + arr[3].ToString();
            return result;
        }

        /// <summary>
        /// get first octet for this ip 
        /// </summary>
        /// <returns>int value of first octet</returns>
        private int GetFirstOctet()
        {
            return int.Parse(ip.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// get second octet for this ip 
        /// </summary>
        /// <returns>int value of second octet</returns>
        private int GetSecondOctet()
        {
            return int.Parse(ip.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// get third octet for this ip 
        /// </summary>
        /// <returns>int value of third octet</returns>
        private int GetThirdOctet()
        {
            return int.Parse(ip.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// get fourth octet for this ip 
        /// </summary>
        /// <returns>int value of fourth octet</returns>
        private int GetFourthOctet()
        {
            return int.Parse(ip.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }



        /// <summary>
        /// check if this ip valid or invalid
        /// </summary>
        /// <returns>true if its valid ip, false if its invalid ip </returns>
        public bool IsValidIp()
        {
            return this.IsValid;
        }

        /// <summary>
        /// get IP class [A, B, C, D, E]
        /// </summary>
        /// <returns>IPClass instance </returns>
        public IpClass GetClass()
        {
            var FirstOctate = GetFirstOctet();

            if (FirstOctate >= 1 && FirstOctate < 127)
            {
                return IpClass.A;
            }
            else if (FirstOctate == 127)
            {
                return IpClass.LOOPBACK;
            }
            else if (FirstOctate >= 128 && FirstOctate <= 191)
            {
                return  IpClass.B;
            }
            else if (FirstOctate >= 192 && FirstOctate <= 223)
            {
                return IpClass.C;
            }
            else if (FirstOctate >= 224 && FirstOctate <= 239)
            {
                return IpClass.D;
            }
            else
            {
                return IpClass.E;
            }
        }

        /// <summary>
        /// check if IP loopback or not 
        /// </summary>
        /// <returns>true if its loopback, false if not</returns>
        public bool IsLoopBack()
        {
            if (this.GetClass() == IpClass.LOOPBACK)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if ip in multicast ip (class D) or not 
        /// </summary>
        /// <returns>true if ip class equals to D, false if not</returns>
        public bool IsMulticast()
        {
            if (this.GetClass().Equals(IpClass.D))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// compare if this ip greator than obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip greator than obj ip </returns>
        public bool IsGreator(IpAnalyzer obj)
        {
            if (this.CompareTo(obj) == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// compare if this ip greator than or equal obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip greator than or equal obj ip </returns>
        public bool IsGreatorOrEqual(IpAnalyzer obj)
        {
            if (this.CompareTo(obj) == 1 || this.CompareTo(obj) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// compare if this ip less than obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip greator than obj ip </returns>
        public bool IsLess(IpAnalyzer obj)
        {
            if (this.CompareTo(obj) == -1)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// get binary notation for ip 
        /// </summary>
        /// <returns>ip in binary notation</returns>
        public string GetBinaryNotaion()
        {
            return SubnetUtils.IpToBinary(ToString());
        }



        /// <summary>
        /// compare if this ip less than or equal obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip less than or equal obj ip </returns>
        public bool IsLessThanOrEqual(IpAnalyzer obj)
        {
            if (this.CompareTo(obj) == -1 || this.CompareTo(obj) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// compare if this ip equal obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip equal obj ip </returns>
        public bool IsEqual(IpAnalyzer obj)
        {
            if (this.CompareTo(obj) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if this ip private or not 
        /// private ip range 
        /// 10.0.0.0 to 10.255.255.255
        /// 172.16.0.0 to 172.31.255.255
        /// 192.168.0.0 to 192.168.255.255
        /// </summary>
        /// <returns>true if its private ip, false if not </returns>
        public bool IsPrivate()
        {
            var FirstOctate = GetFirstOctet();
            var SecondOctet = GetSecondOctet();
            if (FirstOctate == 10)
            {
                return true;
            }
            else if (FirstOctate == 192 && SecondOctet == 168)
            {
                return true;
            }
            else if (FirstOctate == 172)
            {
                if (SecondOctet >= 16 && SecondOctet <= 31)
                {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// Check if this ip public or not 
        /// </summary>
        /// <returns>true if its public ip, otherwise false</returns>
        public bool IsPublic()
        {
            return !IsPrivate();
        }


        /// <summary>
        /// check if this ip live or not by pinging it 
        /// </summary>
        /// <returns>return true if ping replay ststus is true,  false if other</returns>
        public bool IsAlive()
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send(ToString(), 1000);
                if (reply != null)
                {
                    return (reply.Status == 0 ?true: false);

                }else
                {
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("ERROR: You have Some TIMEOUT issue");
                return false;
            }
        }



        /// <summary>
        /// calculate number of hosts for each subnet in this subnet mask 
        /// </summary>
        /// <returns>max number of hosts</returns>
        public int GetNumberOfHosts()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            return (int)Math.Pow(2, 32-SubnetUtils.SubnetMaskToCIDR(subnet));
        }


        /// <summary>
        /// calculate number of valid hosts for each subnet in this subnet mask 
        /// </summary>
        /// <returns>max number of valid hosts</returns>
        public int GetNumberOfValidHosts()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            return GetNumberOfHosts()-2;
        }


        /// <summary>
        /// calculate number of subnets for this subnet mask 
        /// </summary>
        /// <returns>number of subnets</returns>
        public int GetNumberOfSubnets()
        {
            if (GetClass() == IpClass.A)
            {
                return (int)Math.Pow(2, SubnetUtils.SubnetMaskToCIDR(subnet) - 8);
            }
            else if (GetClass() == IpClass.B)
            {
                return (int)Math.Pow(2, SubnetUtils.SubnetMaskToCIDR(subnet) - 16);
            }
            else
            {
                return (int)Math.Pow(2, SubnetUtils.SubnetMaskToCIDR(subnet) - 24);
            }
        }


        /// <summary>
        /// calculate network address for this ip and subnet mask 
        /// </summary>
        /// <returns>network address </returns>
        public string GetNetworkAddress()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            string[] subnetBinary = subnet.Split('.');
            string[] ipBinary = ToString().Split('.');
            var result = "";
            for (int i = 0; i < 4; i++)
            {
                result += (int.Parse(subnetBinary[i]) & int.Parse(ipBinary[i])) + ".";
            }
            return result.Substring(0, result.Length - 1);
        }



        /// <summary>
        /// calculate brpadcast address for this ip and subnet mask 
        /// </summary>
        /// <returns>broadcast address</returns>
        public string GetBroadcastAddress()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            var networkBinary = GetNetworkAddress().Split('.');
            var wildcardBinary = GetWildcardMask().Split('.');
            var result = "";
            for (int i = 0; i < 4; i++)
            {
                result += (int.Parse(networkBinary[i]) | int.Parse(wildcardBinary[i])) + ".";
            }
            return result.Substring(0, result.Length - 1);
        }


        /// <summary>
        /// sonvert subnet mask to cidr 
        /// </summary>
        /// <returns>CIDR value of subnet mask </returns>
        public string GetCIDRNotation()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            return "/" + SubnetUtils.SubnetMaskToCIDR(subnet);
        }


        /// <summary>
        /// get short fo ip and subnet mask like (255.255.255.255/14)
        /// </summary>
        /// <returns>ip and CIDR notation</returns>
        public string GetShort()
        {
            return ToString() + GetCIDRNotation();
        }


        /// <summary>
        /// get subnet mask 
        /// </summary>
        /// <returns>subnet mask </returns>
        public string GtSubnetMask()
        {
            return subnet;
        }


        /// <summary>
        /// calculate wildcard mask for this subnet mask 
        /// </summary>
        /// <returns>wildcard mask in stirng format</returns>
        public string GetWildcardMask()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            var octets = subnet.Split('.');
            var result = "";
            for (int i = 0; i < 4; i++)
            {
                result += 255 - int.Parse(octets[i]) + ".";
            }
            return result.Substring(0, result.Length - 1);
        }



        /// <summary>
        /// return next ip for this ip 
        /// </summary>
        /// <param name="ip">ip in normal form </param>
        /// <returns>next ip</returns>
        public static string GetNextIp(string ip)
        {
            if (!IpValidator(ip))
            {
                throw new ArgumentException("You just passed an invalid ip address");
            }
            else
            {
                if (ip == "255.255.255.255")
                {
                    return ip;
                }
                var stringOctets = ip.Split('.');
                var intOctets = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    intOctets[i] = int.Parse(stringOctets[i]);
                }

                if (intOctets[3] + 1 <= 255)
                {
                    intOctets[3] += 1;
                } else
                {
                    intOctets[3] = 0;
                    if (intOctets[2] + 1 <= 255)
                    {
                        intOctets[2] += 1;
                    }
                    else
                    {
                        intOctets[2] = 0;
                        if (intOctets[1] + 1 <= 255)
                        {
                            intOctets[1] += 1;
                        }
                        else
                        {
                            intOctets[1] = 0;
                            if (intOctets[0] + 1 <= 255)
                            {
                                intOctets[0] += 1;
                            }
                            else
                            {
                                intOctets[0] = 0;
                            }
                        }
                    }
                }
                return intOctets[0] + "." + intOctets[1] + "." + intOctets[2] + "." + intOctets[3];
            }
        }




        /// <summary>
        /// return previous ip for this ip 
        /// </summary>
        /// <param name="ip">ip in normal form </param>
        /// <returns>previous ip</returns>
        public static string GetPreviousIp(string ip)
        {
            if (!IpValidator(ip))
            {
                throw new ArgumentException("You just passed an invalid ip address");
            }
            else
            {
                if (ip == "0.0.0.0")
                {
                    return ip;
                }
                var stringOctets = ip.Split('.');
                var intOctets = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    intOctets[i] = int.Parse(stringOctets[i]);
                }

                if (intOctets[3] - 1 >= 0)
                {
                    intOctets[3] -= 1;
                }
                else
                {
                    intOctets[3] = 255;
                    if (intOctets[2] - 1 >= 0)
                    {
                        intOctets[2] -= 1;
                    }
                    else
                    {
                        intOctets[2] = 255;
                        if (intOctets[1] - 1 >= 0)
                        {
                            intOctets[1] -= 1;
                        }
                        else
                        {
                            intOctets[1] = 255;
                            if (intOctets[0] - 1 >= 0)
                            {
                                intOctets[0] -= 1;
                            }
                            else
                            {
                                intOctets[0] = 0;
                            }
                        }
                    }
                }
                return intOctets[0] + "." + intOctets[1] + "." + intOctets[2] + "." + intOctets[3];
            }
        }


        /// <summary>
        /// get all subnets for this subnet mask as List of Subnet objects
        /// </summary>
        /// <returns>list of subnets </returns>
        public List<Subnet> GetSubnets()
        {
            if (subnet == null)
            {
                throw new NullReferenceException("No subnet mask provided for this IpAnalyzer Object");
            }
            IpAnalyzer temp = new IpAnalyzer(ToString(), subnet);
            List<Subnet> result = new List<Subnet>();
            for (int i = 0; i < GetNumberOfSubnets(); i++)
            {
                result.Add(new Subnet(temp.GetNetworkAddress(), temp.GetBroadcastAddress()));
                temp = new IpAnalyzer(IpAnalyzer.GetNextIp(temp.GetBroadcastAddress()), subnet);
            }
            return result;
        }

        /// <summary>
        /// how to present this IpAnalyzer instance 
        /// </summary>
        /// <returns>instance as string </returns>
        public override string ToString()
        {
            return IpHexToString(ip);
        }



        /// <summary>
        /// compare to ip's 
        /// </summary>
        /// <param name="obj">IP obj to compare with</param>
        /// <returns>return 1 if this ip greator than obj, -1 if its less than, 0 if both are equal </returns>
        public int CompareTo(IpAnalyzer obj)
        {
            string ThisOctates = this.GetFirstOctet() + "" + this.GetSecondOctet() + "" + this.GetThirdOctet() + "" + this.GetFourthOctet();
            string ObjOctates = obj.GetFirstOctet() + "" + obj.GetSecondOctet() + "" + obj.GetThirdOctet() + "" + obj.GetFourthOctet();
            if (int.Parse(ThisOctates) > int.Parse(ObjOctates))
            {
                return 1;
            }
            else if (int.Parse(ThisOctates) < int.Parse(ObjOctates))
            {
                return -1;
            }
            return 0;

        }

    }
}
