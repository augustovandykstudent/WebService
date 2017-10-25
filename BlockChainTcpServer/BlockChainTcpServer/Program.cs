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
                // loads existing blockchain from hard drive and deserializes it
                _serializer = new Serializer();
                byte[] bdata = File.ReadAllBytes(@"D:\Data\Data.dat");
                MemoryStream memstream = new MemoryStream(bdata);
                _objectToSerialize = _serializer.DeSerializeObject(memstream);
                _chain = (BlockChain)_objectToSerialize.BlockChain;
                memstream.Close();
            }
            catch (Exception eException)
            {
                Console.WriteLine(eException.ToString());
                Console.WriteLine("New Block Chain Created");
                _chain = new BlockChain();
            }

            //initialize server
            _server = new SimpleTcpServer();
            _server.Delimiter = 0x13;
            _server.StringEncoder = Encoding.UTF8;
            _server.DataReceived += server_DataReceived;

            //start server
            StartServer();
            Console.WriteLine("Server Started\n");
            Console.WriteLine("Type:\nquit To Close The Server\nprint To Print The Blockchain\nsave To Save Blockchain");
            
            while(_server.IsStarted)
            {
                string input = Console.ReadLine();
                Console.WriteLine();
                if (input == "quit")
                    _server.Stop();
                if (input == "print")
                    _chain.ShowBlockChain();
                if (input == "save")
                    SaveBlockChain();
            }
        }

        public static void server_DataReceived(object sender, SimpleTCP.Message e)
        {
            Console.WriteLine("\nRequest Received");
            string sReceivedMessage = e.MessageString;
            string[] sData = sReceivedMessage.Split(',');
            if (sData[0].Contains("AddToBlockChain"))
            {
                bool bValid = AddToBlockChain(sData[1], sData[2]);
                _server.BroadcastLine("" + bValid);
                Console.WriteLine("\nRequest Replied");
            }
            if (sData[0].Contains("Validate"))
            {
                bool bValid = Validate(sData[1]);
                _server.BroadcastLine("" + bValid);
                Console.WriteLine("\nRequest Replied");
            }
            if (sData[0].Contains("GetDocumentInfo"))
            {
                byte[] bData = GetDocumentInfo(sData[1]);
                _server.Broadcast(bData);
                Console.WriteLine("\nRequest Replied");
            }
            if (sData[0].Contains("GetBlockChain"))
            {
                byte[] bData = GetBlockChain();
                _server.Broadcast(bData);
                Console.WriteLine("\nRequest Replied");
            }
            if (sData[0].Contains("GetUserBlockChainInfo"))
            {
                byte[] bData = GetUserBlockChainInfo(sData[1]);
                _server.Broadcast(bData);
                Console.WriteLine("\nRequest Replied");
            }

        }

        public static bool Validate(string sHash)
        {
            bool bValid = _chain.CheckIfHashExists(sHash);

            _action = null;// set the action variable to null so the server knows to listen for next request first
            return bValid;
        }

        public static bool AddToBlockChain(string sHash, string sUserID)
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
/*
            // broadcast of blockchain to keep it decentralised and updates everyones
            byte[] bChain = ObjectToByteArray(_chain);
            _server.BroadcastLine("UpdateChain");
            _server.Broadcast(bChain);*/
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

        public static byte[] GetDocumentInfo(string sHash)
        {
            byte[] sData = _chain.GetBlockValues(sHash);
            return sData;
        }

        public static byte[] GetBlockChain()
        {
            Serializer serializer = new Serializer();
            MemoryStream stream;
            BlockChain userInfo = _chain;
            ObjectToSerialize objectSerialize = new ObjectToSerialize();
            objectSerialize.BlockChain = _chain;
            stream = serializer.SerializeObject(objectSerialize);
            FileStream filestream = File.OpenWrite(@"D:\Data\Data.dat");
            byte[] bdata = stream.ToArray();

            return bdata;
        }

        private static bool SaveBlockChain()// serializes the block chain and saves it to the hard drive
        {
            FileStream fileStream;
            MemoryStream stream;
            try
            {
                ObjectToSerialize objectToSerialize = new ObjectToSerialize();
                objectToSerialize.BlockChain = _chain;
                Serializer serializer = new Serializer();
                stream = serializer.SerializeObject(objectToSerialize);
                fileStream = File.OpenWrite(@"D:\Data\Data.dat");
                byte[] bstreamdata = stream.ToArray();
                fileStream.Write(bstreamdata, 0, bstreamdata.Length);
            }
            catch { return false; }
            stream.Close();
            fileStream.Close();
            return true;
        }

        public static byte[] GetUserBlockChainInfo(string suserid)
        {
            Serializer serializer = new Serializer();
            MemoryStream stream;
            BlockChain userInfo = _chain.GetBlockValuesForUser(suserid);
            ObjectToSerialize objectSerialize = new ObjectToSerialize();
            objectSerialize.BlockChain = _chain;
            stream = serializer.SerializeObject(objectSerialize);
            byte[] bdata = stream.ToArray();

            return bdata;
        }
    }
}
