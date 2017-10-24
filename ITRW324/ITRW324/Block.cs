using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITRW324
{
    [Serializable()]
    public class Block : ISerializable
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

        private Block(SerializationInfo info, StreamingContext ctxt)
        {
            this._sTimeStamp = (string)info.GetValue("TimeStamp", typeof(string));
            this._sHash = (string)info.GetValue("Hash", typeof(string));
            this._sUserID = (string)info.GetValue("UserID", typeof(string));
            this._oNext = (Block)info.GetValue("Next", typeof(Block));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("TimeStamp", this._sTimeStamp);
            info.AddValue("Hash", this._sHash);
            info.AddValue("UserID", this._sUserID);
            info.AddValue("Next", this._oNext);
        }
    }
}
