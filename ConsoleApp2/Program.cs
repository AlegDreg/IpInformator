using System.Text;

namespace IpInform
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IIP iP = new IpInfo("172.30.1.33/16");

            //iP = new IpInfo("172.30.1.33", 16);

            iP.Print();
        }
    }
}