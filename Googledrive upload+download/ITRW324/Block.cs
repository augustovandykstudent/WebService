using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Windows;

namespace ITRW324{
    public class Block{

        private LinkedList<BlockS> BlockList = new LinkedList<BlockS>();
       
        //Default Structure for Block
        public struct BlockS{
            public long blockID ;
            public String time_stamp ;
            public String previousHash ;
            public String hash ;
            public String info ;
        }
        //Public Constructor
        public void BlockAdd(String x,String y)
        {
            Load();
            BlockS tmp = new BlockS();
            tmp.blockID = BlockList.Last.Value.blockID + 1;
            tmp.time_stamp = DateTime.Now.ToString();
            tmp.previousHash = getprevHash();
            tmp.hash = x;
            tmp.info = y;
            if(Search(BlockList,tmp.hash) == null){
                BlockList.AddLast(tmp);                
            }
            Save(BlockList);
        }
        //Get Previous Hash Value
        public String getprevHash(){
            String prev = "";
            prev = BlockList.Last.Value.hash;
            return prev; 
        }
        //Search BlockChain For Value
        private String Search(LinkedList<BlockS> list, String value){
            foreach(BlockS y in list){
                if(y.hash == value){
                    return (y.blockID + " " + y.info + " " + y.time_stamp + " " + y.previousHash);
                }                    
            }
            return null;
        }
        //Save BlockChain
        public void Save(LinkedList<BlockS> link){
            StreamWriter SaveWrite = new StreamWriter(File.OpenWrite(@"C:\Users\User-PC\Desktop\test_read\data.txt"));
            foreach(BlockS x in link){
                SaveWrite.WriteLine(x.blockID+","+x.time_stamp + "," +x.previousHash + "," +x.hash + "," +x.info);
            }
            SaveWrite.Close();
        }

        public void Load(){
            StreamReader lees = new StreamReader(File.OpenRead(@"C:\Users\User-PC\Desktop\test_read\data.txt"));
            string lyn;
            while ((lyn = lees.ReadLine()) != null)
            {
                string[] element = lyn.Split(',');
                BlockS tmp = new BlockS();
                tmp.blockID = Convert.ToInt32(element[0]);
                tmp.time_stamp = element[1];
                tmp.previousHash = element[2];
                tmp.hash = element[3];
                tmp.info = element[4];
                BlockList.AddLast(tmp);
            }
            lees.Close();
        }

    }
}