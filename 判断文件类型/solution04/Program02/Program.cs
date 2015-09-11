using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Program02
{
    //文件类————基类
    class FileClass
    {
        private string name;        //文件名成员

        //基类的构造函数
        public FileClass(string Name)
        {
            name = Name;
        }

        //判断类型虚方法，在基类中不实现
        public virtual bool IsThisType()
        {
            return false;
        }
    }


    //ZIP子类
    class ZipClass : FileClass
    {
       
        private string header;                      //文件头
        private int offset;                         //偏移量
        private int byteNum;                        //需读取的字节数

        //ZIP类的构造函数
        public ZIP(string path)
            : base(path)
        {
            header = "504B0304";
            offset = 0;
            byteNum = 4;



    
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
