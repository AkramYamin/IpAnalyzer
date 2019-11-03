using System;

namespace IpAnalyzer
{
    public partial class IP: IComparable<IP>
    {
        public int FirstOctate { get; set; }
        public int SecondOctet { get; set; }
        public int ThirdOctet { get; set; }
        public int FourthOctet { get; set; }

        public IpClass MyIpClass { get; set; }

        public IP(int first, int second, int third, int fourth)
        {
            this.FirstOctate = first;
            this.SecondOctet = second;
            this.ThirdOctet = third;
            this.FourthOctet = fourth;

            if (FirstOctate >=1 && FirstOctate <= 127)
            {
                this.MyIpClass = IpClass.A;
            }
            else if (FirstOctate >= 128 && FirstOctate <= 191)
            {
                this.MyIpClass = IpClass.B;
            }
            else if (FirstOctate >= 192 && FirstOctate <= 223)
            {
                this.MyIpClass = IpClass.C;
            }
            else if (FirstOctate >= 224 && FirstOctate <= 239)
            {
                this.MyIpClass = IpClass.D;
            } else
            {
                this.MyIpClass = IpClass.E;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", FirstOctate, SecondOctet, ThirdOctet, FourthOctet);
        }


        /// <summary>
        /// compare to ip's 
        /// </summary>
        /// <param name="obj">IP obj to compare with</param>
        /// <returns>return 1 if this ip greator than obj, -1 if its less than, 0 if both are equal </returns>
        public int CompareTo(IP obj)
        {
            string ThisOctates = this.FirstOctate + "" + this.SecondOctet + "" + this.ThirdOctet + "" + this.FourthOctet;
            string ObjOctates = obj.FirstOctate + "" + obj.SecondOctet + "" + obj.ThirdOctet + "" + obj.FourthOctet;
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
