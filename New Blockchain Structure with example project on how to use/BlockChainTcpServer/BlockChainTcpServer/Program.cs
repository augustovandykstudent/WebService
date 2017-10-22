using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

            GetReference().AddToBlockChain("123", "1");

            //deserialization
            byte[] bChain = GetReference().GetBlockChain();
            chain = objectToSerialize.BlockChain;

            chain.ShowBlockChain();

            Console.ReadLine();//pause
        }

        public bool Validate(string sHash)
        {
            return true;
        }

        private BlockChain Deserialize(byte[] param)
        {
            if (param == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(param);
            BlockChain chain = (BlockChain)bf.Deserialize(ms);

            return chain;
            
        }

        private static ServiceReference1.ServiceSoapClient GetReference()
        {
            return new ServiceReference1.ServiceSoapClient();
        }
    }
}
