using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpInform
{
    internal class IpInfo : IIP
    {
        public double numberNetworkNodes { get; set; }
        public int numberBitsInNodePart { get; set; }
        public string netAddress { get; set; }
        public string networkMask { get; set; }
        public string addressFirstNode { get; set; }
        public string lastnameAddress { get; set; }
        public string broadcast { get; set; }

        string currentIp;

        public IpInfo(string ip)
        {
            currentIp = ip;

            Process();
        }

        public IpInfo(string ip, int maskBits)
        {
            currentIp = ip + "/" + maskBits;

            Process();
        }

        private void Process()
        {
            string[] k = currentIp.Split('/');

            string[] bytes = k[0].Split('.');

            if (k.Length != 2 || bytes.Length != 4)
                new ArgumentException("Переданы неправильные параметры");

            StringBuilder bytes_in_binary = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                bytes_in_binary.Append(AddNesnachachieNuli(Convert.ToString(Convert.ToInt32(bytes[i]), 2)) + ".");
            }

            bytes_in_binary = bytes_in_binary.Remove(bytes_in_binary.Length - 1, 1);

            string mask = k[1];

            var adres = 32 - Convert.ToInt32(mask);

            numberBitsInNodePart = adres;

            numberNetworkNodes = Math.Pow(2.0, Convert.ToInt32(adres)) - 2;

            for (int i = bytes_in_binary.Length - 1; i > Convert.ToInt32(mask); i--)
            {
                if (bytes_in_binary[i] == '1')
                {
                    bytes_in_binary[i] = '0';
                }
            }

            string[] vs = bytes_in_binary.ToString().Split('.');

            for (int i = 0; i < 4; i++)
            {
                netAddress += Convert.ToInt32(DeleteNeznach(vs[i]), 2) + ".";
            }

            netAddress = netAddress.Remove(netAddress.Length - 1, 1);

            string[] fd = GetMask(mask).Split('.');

            for (int i = 0; i < 4; i++)
            {
                networkMask += Convert.ToInt32(fd[i], 2) + ".";
            }
            networkMask = networkMask.Remove(networkMask.Length - 1, 1);

            string[] kss = netAddress.Split('.');

            string r = "";

            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToInt32(kss[i]) == 0)
                {
                    r += "255.";
                }
                else
                {
                    r += kss[i] + ".";
                }
            }

            broadcast = r;
            broadcast = broadcast.Remove(broadcast.Length - 1, 1);

            var t = broadcast[broadcast.Length - 1].ToString();

            lastnameAddress = broadcast.Remove(broadcast.Length - 1, 1);
            lastnameAddress += (Convert.ToInt32(t) - 1);


            string rr = "";
            int flag = -1;

            for (int i = 0; i < 4; i++)
            {
                if (kss[i] == "0")
                {
                    flag = i;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (i != flag)
                {
                    rr += kss[i] + ".";
                }
                else
                {
                    rr += (int)(Convert.ToInt32(kss[i]) + 1) + ".";
                }
            }

            rr = rr.Remove(rr.Length - 1, 1);

            addressFirstNode = rr;
        }

        public void Print()
        {
            Console.WriteLine("IP - " + currentIp);
            Console.WriteLine("Адрес сети - " + netAddress);
            Console.WriteLine("Маска сети - " + networkMask);
            Console.WriteLine("Количество узлов - " + numberNetworkNodes);
            Console.WriteLine("Количество бит в узловой части - " + numberBitsInNodePart);
            Console.WriteLine("Адрес первого узла - " + addressFirstNode);
            Console.WriteLine("Адрес последнего узла - " + lastnameAddress);
            Console.WriteLine("broadcast - " + broadcast);
        }

        private string GetMask(string mask)
        {
            string res = "";
            int flag = 0;

            for (int i = 0; i < Convert.ToInt32(mask); i++)
            {
                res += "1";
                flag++;

                if (flag % 8 == 0)
                    res += ".";
            }

            for (int i = Convert.ToInt32(mask); i < 32; i++)
            {
                res += "0";
                flag++;

                if (flag % 8 == 0)
                    res += ".";
            }

            return res;
        }

        private string AddNesnachachieNuli(string s)
        {
            while (s.Length != 8)
            {
                s = "0" + s;
            }

            return s;
        }

        private string DeleteNeznach(string s)
        {
            string v = "";
            int flag = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    flag = i;
                else
                    break;
            }

            for (int i = flag; i < s.Length; i++)
            {
                v += s[i];
            }

            return v;
        }
    }
}
