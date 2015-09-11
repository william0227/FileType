using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Program01
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
        public virtual void DetectType()
        {

        }
    }

    //只有header的情况
    class HeaderOnly : FileClass
    {
        private string fileString;                   //接收header的字符串

        //HeaderOnly的构造函数
        public HeaderOnly(string name)
            : base(name)
        {
            fileString = "";
            FileStream fs;                                              //文件流
            fs = new FileStream(name, FileMode.Open, FileAccess.Read); //从文件名读取一个文件流
            string FileString = "";
            fs.Seek(0, SeekOrigin.Begin);              //设置文件开始读取的位置
            BinaryReader reader = new BinaryReader(fs);     //初始化二进制文件读写器，读取文件流
            byte[] b = new byte[20];                   //接收文件内容的byte数组

            //异常处理
            try
            {
                for (int i = 0; i < b.Length; i++)
                {
                    b[i] = reader.ReadByte();                   //将文件内容读入数组
                }
            }
            catch (Exception)
            {
                throw;
            }

            fs.Close();                             //关闭文件流，释放资源
            reader.Dispose();                       //释放二进制读取器的资源

            for (int i = 0; i < b.Length; i++)
            {
                FileString += b[i].ToString("X2");        //将字节数组内容转为字符串(16进制)
            }

            fileString = FileString;
            Console.WriteLine(fileString);
        }


        //判断类型,重写基类中的虚方法
        public override void DetectType()
        {
            //MP4相关类型
            if (fileString.Substring(0, 6) == "000000")
            {
                switch (fileString.Substring(6, 2))
                {
                    case "0C": Console.WriteLine("JP2"); return;
                    case "18": Console.WriteLine("M4V"); return;
                    case "20": Console.WriteLine("M4A"); return;
                    case "14": Console.WriteLine("MOV"); return;
                    case "1C": Console.WriteLine("MP4"); return;
                    default: break;

                }
            }

            //AVI相关类型
            if (fileString.Substring(0, 8) == "52494646")
            {
                switch (fileString.Substring(16, 2))
                {
                    case "41": Console.WriteLine("AVI"); return;
                    case "43": Console.WriteLine("CDA"); return;
                    case "51": Console.WriteLine("QCP"); return;
                    case "57": Console.WriteLine("WMV"); return;
                    default: break;

                }
            }


            //其他类型
            if (fileString.IndexOf("0D444F43") == 0)
            {
                Console.WriteLine("DOC"); return;
            }

            if (fileString.IndexOf("464C5601") == 0)
            {
                Console.WriteLine("FLV"); return;
            }

            if (fileString.IndexOf("494433") == 0)
            {
                Console.WriteLine("MP3"); return;
            }
            if (fileString.IndexOf("504B0304") == 0)
            {
                Console.WriteLine("ZIP"); return;
            }
            if (fileString.IndexOf("377ABCAF271C") == 0)
            {
                Console.WriteLine("7Z"); return;
            }
            if (fileString.IndexOf("3026B2758E66CF11A6D9") == 0)
            {
                Console.WriteLine("ASF/WMA/WMV"); return;
            }
            if (fileString.IndexOf("526172211A0700") == 0)
            {
                Console.WriteLine("RAR"); return;
            }
            if (fileString.IndexOf("435753") == 0)
            {
                Console.WriteLine("SWF"); return;
            }

        }

    }

    /*
        //既有header也有trailer的子类，继承FileClass
        class HeaderAndTrailer : FileClass
        {

            private string fileString_header;                   //接收header的字符串
            private string fileString_trailer;                   //接收trailer的字符串
            private string header;                          //header
            private string trailer;                         //trailer
            private int offset_header;                         //header的偏移量
            private int offset_trailer;                     //trailer的偏移量
            private int byteNum_header;                       //header需读取的字节数
            private int byteNum_trailer;                       //trailer需读取的字节数


            //HeaderOnly的构造函数
            public HeaderAndTrailer(string name, string Header, string Trailer, int Offset_Header, int Offset_Trailer, int ByteNum_Header, int ByteNum_Trailer)
                : base(name)
            {
                fileString_header = "";
                fileString_trailer = "";
                header = Header;
                trailer = Trailer;
                offset_header = Offset_Header;
                offset_trailer = Offset_Trailer;
                byteNum_header = ByteNum_Header;
                byteNum_trailer = ByteNum_Trailer;


                //先读header
                FileStream fs;
                fs = new FileStream(name, FileMode.Open, FileAccess.Read);
                string FileString = "";
                fs.Seek(offset_header, SeekOrigin.Begin);             //header从头读
                BinaryReader reader1 = new BinaryReader(fs);
                byte[] b1 = new byte[byteNum_header];

                //异常处理
                try
                {
                    for (int i = 0; i < b1.Length; i++)
                    {
                        b1[i] = reader1.ReadByte();
                    }
                }
                catch (Exception)
                {
                    throw;
                }




                for (int i = 0; i < b1.Length; i++)
                {
                    FileString += b1[i].ToString("X2");
                }

                fileString_header = FileString;         //获取header




                //再读trailer
                FileString = "";
                fs.Seek(offset_trailer, SeekOrigin.End);              //trailer从尾读
                BinaryReader reader2 = new BinaryReader(fs);
                byte[] b2 = new byte[byteNum_trailer];

                //异常处理
                try
                {
                    for (int i = 0; i < b2.Length; i++)
                    {
                        b2[i] = reader2.ReadByte();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                reader1.Dispose();
                reader2.Dispose();                       //释放二进制读取器的资源
                fs.Close();                             //关闭文件流

                for (int i = 0; i < b2.Length; i++)
                {
                    FileString += b2[i].ToString("X2");
                }

                fileString_trailer = FileString;        //获取trailer
            }



            //判断是否为该类型,重写基类中的虚方法
            public override bool IsThisType()
            {
                //同时匹配  header 和 trailer
                if (fileString_header.IndexOf(header) != -1 && fileString_trailer.IndexOf(trailer) != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        */

    class Program
    {

        //判断文件类型的函数
        static void FileType(string name)
        {
            FileClass newFile = new HeaderOnly(name);
            newFile.DetectType();
        }


        //主函数，用户只需输入文件名
        static void Main(string[] args)
        {
            string Name = "";

            Console.WriteLine("请输入文件名");
            Name = Console.ReadLine();
            FileType(Name);

            Console.ReadKey();
        }
    }

}