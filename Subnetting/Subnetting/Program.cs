using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subnetting
{
    class Program
    {
        static void Main(string[] args)
        {
            IPv4 ip = new IPv4();

            byte[] addr = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                byte value = 0;
                string s = "";
                do
                {
                    Console.WriteLine("Inserisci il {0}^ byte dell'indirizzo ip", i + 1);
                    s = Convert.ToString(Console.ReadLine());
                } while (!Byte.TryParse(s, out value));
                addr[i] = value;
            }

            ip.SetIp_address(addr);

            addr = new byte[4];
            bool flag = false;
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    byte value = 0;
                    string s = "";
                    flag = false;
                    do
                    {
                        Console.WriteLine("Inserisci il {0}^ byte della subnet mask", i + 1);
                        s = Convert.ToString(Console.ReadLine());
                        flag = Byte.TryParse(s, out value);
                    } while (!flag || !ip.checkSingleByte(value));
                    addr[i] = value;
                }
            } while (ip.checkSubnetMask(addr));

            ip.SetSubnetMask(addr);

            byte[][] result = new byte[2][];

            Console.WriteLine(ip.Get_CIDR());
            Console.WriteLine(ip.GetIP_addrbool());
            Console.WriteLine(String.Join(".", ip.GetSubnetMask()));
            Console.WriteLine(String.Join(".", ip.GetNetworkAddress()));
            Console.WriteLine(String.Join(".", ip.GetBroadcast()));
            Console.WriteLine(String.Join(".", ip.GetWildcard()));
            Console.WriteLine(String.Join(".", ip.GetFirstHostIP()));
            Console.WriteLine(String.Join(".", ip.GetLastHostIp()));
            Console.WriteLine(ip.GetTotalNumberHost());

            result = ip.GetNumberUsableHost();
            Console.WriteLine(String.Join(".", result[0]) + " --- " + String.Join(".", result[1]));
        }
    }
}
