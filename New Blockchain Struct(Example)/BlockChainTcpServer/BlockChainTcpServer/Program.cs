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
            BlockChain chain;
            ObjectToSerialize objectSerialize = new ObjectToSerialize();
            Serializer serializer = new Serializer();
            byte[] bData = GetReference().GetUserBlockChainInfo("1");
            MemoryStream memstream = new MemoryStream(bData);
            objectSerialize = serializer.DeSerializeObject(memstream);
            chain = (BlockChain)objectSerialize.BlockChain;
            memstream.Close();

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
