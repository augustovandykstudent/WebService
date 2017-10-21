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
            chain.Append(Convert.ToString(System.DateTime.Now), "Good Good", Convert.ToString(2));
            chain.Append(Convert.ToString(System.DateTime.Now), "DroopDroop", Convert.ToString(4));
            chain.ShowBlockChain();
            Console.WriteLine("Serialization");

            ObjectToSerialize objectToSerialize = new ObjectToSerialize();
            objectToSerialize.BlockChain = chain;

            Serializer serializer = new Serializer();
            serializer.SerializeObject(@"D:\Data\outputFile.txt", objectToSerialize);

            chain = null;

            //deserialization
            objectToSerialize = serializer.DeSerializeObject(@"D:\Data\outputFile.txt");
            chain = objectToSerialize.BlockChain;

            chain.ShowBlockChain();

            Console.ReadLine();//pause
        }

        public bool Validate(string sHash)
        {
            return true;
        }
    }
}
