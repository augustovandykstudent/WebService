using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlockChainTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockChain chain = new BlockChain();
            chain.AddToHead(Convert.ToString(System.DateTime.Now), "BlahBlah", Convert.ToString(1));
            chain.Append(Convert.ToString(System.DateTime.Now), "DroopDroop", Convert.ToString(1));
            chain.ShowBlockChain();
            Console.ReadLine();//pause
        }

        public bool Validate(string sHash)
        {
            return true;
        }
    }
}
