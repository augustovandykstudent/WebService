using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainTcpServer
{
    class Block
    {
        public string _sTimeStamp, _sHash, _sUserID;
        public Block _oNext;

        public Block()
            : this (null, null, null, null)
        {          
        }

        public Block(string sTimeStamp, string sHash, string sUserID)
            : this(sTimeStamp, sHash, sUserID, null)
        {
        }

        public Block(string sTimeStamp, string sHash, string sUserID, Block oPtr)
        {
            _sTimeStamp = sTimeStamp;
            _sHash = sHash;
            _sUserID = sUserID;
            _oNext = oPtr;
        }
    }
}
