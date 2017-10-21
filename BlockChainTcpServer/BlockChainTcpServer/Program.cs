using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleTCP.Server;
using SimpleTCP;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlockChainTcpServer
{
    class Program
    {
        private static SimpleTcpServer _server;
        private static BlockChain _chain;
        private static ObjectToSerialize _objectToSerialize;
        private static Serializer _serializer;
        private static string _action = null;
        private static byte[] bDataReceived;

        static void Main(string[] args)
        {
            // initiates an instance of the blockchain which exists on HDD
            try
            {
                _objectToSerialize = _serializer.DeSerializeObject(@"D:\Data\Blockchain.dat");
                _chain = _objectToSerialize.BlockChain;
            }
            catch (Exception eException)
            {
                Console.WriteLine("New Block Chain Created");
                _chain = new BlockChain();
            }

            //initialize server
            _server = new SimpleTcpServer();
            _server.Delimiter = 0x13;
            _server.StringEncoder = Encoding.UTF8;
            _server.DataReceived += server_DataReceived;

            Console.WriteLine("\nStart The Server?");
            char cChoice = Convert.ToChar(Console.ReadLine());
            if (cChoice == 'y')// start the server
                StartServer();

            while (_server.IsStarted)
            {

            }

            Console.ReadLine();
            
        }

        public static void server_DataReceived(object sender, SimpleTCP.Message e)
        {
            string sReceivedMessage = e.MessageString;
            bool bReturn = false;

            if (_action == "Validate")
            {
                bDataReceived = e.Data;
            }
                // determine which method to run from request
                if (sReceivedMessage.Contains("Validate"))
                {
                    _action = "Validate";
                }
                else if (sReceivedMessage.Contains("AddToBlockChain"))
                {
                    _action = "AddToBlockChain";
                }
        }

        public bool Validate(string sHash)
        {
            bool bValid = _chain.CheckIfHashExists(sHash);

            _action = null;// set the action variable to null so the server knows to listen for next request first
            return bValid;
        }

        public bool AddToBlockChain(string sHash, string sUserID)
        {
            if (sHash != null & sUserID != null)// validation of information received
            {
                if (_chain == null)
                {
                    _chain.AddToHead(Convert.ToString(DateTime.Now), sHash, sUserID);
                }
                else
                {
                    _chain.Append(Convert.ToString(DateTime.Now), sHash, sUserID);
                }
            }
            _action = null;// set the action variable to null so the server knows to listen for next request first

            return true;

            // broadcast of blockchain to keep it decentralised and updates everyones
            byte[] bChain = ObjectToByteArray(_chain);
            _server.Broadcast(bChain);
        }

        public static void StartServer()
        {
            Console.WriteLine("\nStarting Server");
            System.Net.IPAddress ip = System.Net.IPAddress.Parse("10.0.0.10");
            try
            {
                _server.Start(ip, Convert.ToInt32("4410"));
            }
            catch(Exception eException)
            {
                Console.WriteLine(eException.ToString());
            }
        }

        public byte[] ObjectToByteArray(object obj)// changes blockchain structure to a byte[]
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, _chain);
                return ms.ToArray();
            }
        }
    }
}
