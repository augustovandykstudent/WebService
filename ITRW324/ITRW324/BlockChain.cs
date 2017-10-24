using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Runtime.Serialization.Formatters.Binary;

namespace ITRW324
{
    [Serializable()]

    public class ObjectToSerialize:ISerializable
    {
        private BlockChain blockChain;

        public BlockChain BlockChain
        {
            get { return this.blockChain; }
            set { this.blockChain = value; }
        }

        public ObjectToSerialize()
        {

        }

        public ObjectToSerialize(SerializationInfo info, StreamingContext ctxt)
        {
            this.blockChain = (BlockChain)info.GetValue("BlockChain", typeof(BlockChain));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("BlockChain", this.blockChain);
        }
    }

    [Serializable()]
    public class Serializer
    {
        public Serializer()
        {
        }

        public void SerializeObject(string fileName, ObjectToSerialize objectToSerialize)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public ObjectToSerialize DeSerializeObject(string fileName)
        {
            ObjectToSerialize objectToSerialize;
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ObjectToSerialize)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }

    [Serializable()]
    public class BlockChain:ISerializable 
    {
        private Block head, tail;
        private BlockChain chain;

        public BlockChain()
        {
            head = null;
            tail = null;
        }

        public bool AddToHead(string sTimeStamp, string sHash, string sUserID)// method that adds first block to blockchain
        {
            head = new Block(sTimeStamp, sHash, sUserID);
            if (tail == null)
            {
                tail = head;
            }
            return true;
        }

        public bool AddToTail(string sTimeStamp, string sHash, string sUserID)
        {
            if (!IsEmpty())
            {
                tail._oNext = new Block(sTimeStamp, sHash, sUserID);
                tail = tail._oNext;
            }
            else
                head = tail = new Block(sTimeStamp, sHash, sUserID);
            return true;
        }

        public bool Append(Block newBlock)
        {
            if (newBlock != null)
            {
                if(!IsEmpty())
                {
                    tail._oNext = new Block(newBlock._sTimeStamp, newBlock._sHash, newBlock._sUserID);
                    tail = tail._oNext;
                }
                else
                {
                    head = tail = new Block(newBlock._sTimeStamp, newBlock._sHash, newBlock._sUserID);
                }
                return true;
            }
            return false;
        }

        public bool Append(string sTimeStamp, string sHash, string sUserID)
        {
            Block block = new Block(sTimeStamp, sHash, sUserID);
            if (block == null)
                return false;//ran out of memory
            else
            {
                if(head == null)
                {
                    head = block;
                    tail = block;
                }
                else
                {
                    tail._oNext = block;
                    tail = block;
                }
            }
            return true;
        }

        public bool IsEmpty()
        {
            if (head == null)
                return true;
            return false;
        }

        public void ShowBlockChain()
        {
            if(head == null)
            {
                Console.WriteLine("null");
                return;
            }

            Block ptr = head;
            while (ptr != null)
            {
                Console.WriteLine("Time Stamp: " + ptr._sTimeStamp + " Hash: " +  ptr._sHash + " User ID: " + ptr._sUserID);
                ptr = ptr._oNext;
            }
        }

        public bool CheckIfHashExists(string sHash)
        {
            Block ptr = head;
            while (ptr != null)
            {
                if (sHash == ptr._sHash)
                    return true;
                ptr = ptr._oNext;
            }
            return false;
        }

        private BlockChain(SerializationInfo info, StreamingContext ctxt)
        {
            this.head = (Block)info.GetValue("Head", typeof(Block));
            this.tail = (Block)info.GetValue("Tail", typeof(Block));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Head", this.head);
            info.AddValue("Tail", this.tail);
        }

        public byte[] GetBlockValues(string sHash)
        {
            // get block with data
            string[] sData = null;
            Block ptr = head;
            while (ptr != null)
            {
                if (sHash == ptr._sHash)
                {
                    sData = new string[3];
                    sData[0] = ptr._sTimeStamp;
                    sData[1] = ptr._sHash;
                    sData[2] = ptr._sUserID;
                }
                ptr = ptr._oNext;
            }
            byte[] bData = Encoding.ASCII.GetBytes(sData[0] + "," + sData[1] + "," + sData[2] + ",");
            return bData;
        }

        public BlockChain GetBlockValuesForUser(string suserID)
        {
            // get all the values of a user
            BlockChain ouserData = new BlockChain();
            Block optr = this.head;

            while (optr != null)
            {
                if (optr._sUserID == suserID)
                {
                    ouserData.Append(optr);
                }
                optr = optr._oNext;
            }
            return ouserData;
        }

        public List<string> GetBlockValuesList()
        {
            // get block with data
            List<string> ldata = new List<string>();
            string[] sData = null;
            Block ptr = head;

            while (ptr != null)
            {
                sData = new string[3];
                sData[0] = ptr._sTimeStamp;
                sData[1] = ptr._sHash;
                sData[2] = ptr._sUserID;
                ldata.Add(sData[0] + "," + sData[1] + "," + sData[2]);
            
                ptr = ptr._oNext;
            }
            return ldata;
        }
    }    
}
