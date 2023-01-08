using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetting
{
    class IPv4
    {
        protected byte[] ipAddr;
        protected byte[] snMask;

        public IPv4()
        {
            ipAddr = new byte[4];
            snMask = new byte[4];
        }

        public byte[] GetIP_Addr()
        {
            return this.ipAddr;
        }

        public string GetIP_addrbool()
        {
            string ipBool = "";
            for (int i = 0; i < 4; i++)
            {
                ipBool += Convert.ToString(ipAddr[i], 2).PadLeft(8, '0') + ".";
            }
            return ipBool;
        }

        public byte[] GetSubnetMask()
        {
            return this.snMask;
        }

        public void SetIp_address(byte[] ipAddress)
        {
            this.ipAddr = ipAddress;
        }

        public void setSubnetMask(byte[] subnetMask)
        {
            this.snMask = subnetMask;
        }

        public byte[] GetNetworkAddress()
        {
            byte[] netAddr = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                netAddr[i] = (byte)(ipAddr[i] & snMask[i]);
            }

            return netAddr;
        }

        public byte[] GetBroadcast()
        {
            byte[] broadcast = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                broadcast[i] = (byte)(this.GetNetworkAddress()[i] | this.GetWildcard()[i]);
            }
            return broadcast;
        }

        public double GetTotalNumberHost()
        {
            return Math.Pow(2, (32 - this.Get_CIDR()));
        }

        public double GetNumberUsableHost()
        {
            return this.GetTotalNumberHost() - 2;
        }

        public byte[] GetWildcard()
        {
            byte[] wildcard = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                wildcard[i] = (byte)(255 - (int)snMask[i]);
            }
            return wildcard;
        }

        public int GetCIDR()
        {
            int cidr = 0;
            foreach (byte octate in snMask)
            {
                if (octate == 255)
                {
                    cidr += 8;
                }
                else if (octate == 0)
                {
                    break;
                }
                else
                {
                    int bit = octate;
                    while (bit > 0)
                    {
                        cidr += bit & 1;
                        bit >>= 1;
                    }
                }
            }
            return cidr;
        }

        public void SetCIDR(int bits)
        {
            double sig_byte = 0;
            for (int i = 0; i < 4; i++)
            {
                if ((bits - (i * 8)) > 8)
                {
                    sub_mask[i] |= 255;
                }
                else if (bits - (i * 8) > 0)
                {

                    for (int j = 7; j >= 8 - (bits - (i * 8)); j--)
                    {
                        sig_byte += Math.Pow(2, j);
                    }
                    sub_mask[i] = Convert.ToByte(sig_byte);
                    Console.WriteLine(sub_mask[i]);
                }
                else
                {
                    sub_mask[i] |= 0;
                }
            }
        }

        public byte[] GetFirstHostIP()
        {
            byte[] firstAddr = new byte[4];
            firstAddr = this.GetNetworkAddress();
            firstAddr[3] += 1;
            return firstAddr;
        }

        public byte[] GetLastHostIP()
        {
            byte[] lastAddr = new byte[4];
            lastAddr = this.GetBroadcast();
            lastAddr[3] -= 1;
            return lastAddr;
        }

        public bool checkSingleByte(byte single)
        {
            byte[] bytes = new byte[] { 255, 254, 252, 248, 240, 224, 192, 128, 0 };
            return Array.Exists(bytes, element => element == single);                                                                 
        }
        public bool checkSubnetMask(byte[] sm)
        {
            bool check = false;

            for (int i = 0; i < 3; i++)
            {
                if ((sm[i + 1] != 0 && sm[i] != 255))
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
    }
}