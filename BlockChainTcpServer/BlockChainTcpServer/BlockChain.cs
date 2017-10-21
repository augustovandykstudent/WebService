using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainTcpServer
{
    class BlockChain
    {
        private Block head, tail;

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
    }
}
