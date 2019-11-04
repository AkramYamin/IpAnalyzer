using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace IpAnalyzer
{
    public class IpAnalyzer
    {
        public IP ip { get; set; }
        public bool IsValid { get; set; }
        /// <summary>
        /// create new IP from normal or hex ip format 
        /// </summary>
        /// <param name="ip">string or hex ip format </param>
        public IpAnalyzer(string ip)
        {
            this.IsValid = this.IpValidator(ip);
            if (this.IsValid)
            {
                this.ip = IpParser(ip);
                Console.WriteLine(String.Format("new valid ip address created : {0}", this));
            } else
            {
                this.ip = new IP(-1, -1, -1, -1);
                Console.WriteLine(String.Format("Invalid ip address created : {0}", this));
            }
        }
        
        /// <summary>
        /// check if parsed string valid ip or not 
        /// </summary>
        /// <param name="ip">ip string in normal or hex form </param>
        /// <returns>if parsed string can be an ip or not 'boolean'</returns>
        private bool IpValidator(string ip)
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
        private IP IpParser(string ip)
        {
            IP _ip;
            //parse normal form ip
            if (ip.Contains('.'))
            {
                string[] arr = ip.Split('.');
                _ip = new IP(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]));
            } else
            // parse hex form ip
            {
                int[] arr = new int[4];
                ip = ip.Substring(2);
                for (int i = 0; i < 3; i++)
                {
                    arr[i] = int.Parse(ip.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    ip = ip.Substring(2);
                }
                arr[3] = int.Parse(ip, System.Globalization.NumberStyles.HexNumber);
                _ip = new IP(arr[0], arr[1], arr[2], arr[3]);
            }
            return _ip;
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
            return this.ip.MyIpClass;
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
            if (this.ip.CompareTo(obj.ip) == 1)
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
            if (this.ip.CompareTo(obj.ip) == 1 || this.ip.CompareTo(obj.ip) == 0)
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
            if (this.ip.CompareTo(obj.ip) == -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// compare if this ip less than or equal obj ip 
        /// </summary>
        /// <param name="obj">ip to compare with</param>
        /// <returns>true if this ip less than or equal obj ip </returns>
        public bool IsLessThanOrEqual(IpAnalyzer obj)
        {
            if (this.ip.CompareTo(obj.ip) == -1 || this.ip.CompareTo(obj.ip) == 0)
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
            if (this.ip.CompareTo(obj.ip) == 0)
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
            if (this.ip.FirstOctate == 10)
            {
                return true;
            }
            else if (this.ip.FirstOctate == 192 && this.ip.SecondOctet == 168)
            {
                return true;
            }
            else if (this.ip.FirstOctate == 172)
            {
                if (this.ip.SecondOctet >= 16 && this.ip.SecondOctet <= 31)
                {
                    return true;
                }
            }
            return false;
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
                PingReply reply = myPing.Send(this.ip.ToString(), 1000);
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
        /// how to present this IpAnalyzer instance 
        /// </summary>
        /// <returns>instance as string </returns>
        public override string ToString()
        { 
            return this.ip.ToString();
        }

    }
}
