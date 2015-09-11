using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newProgram
{
    //声明一个文件类————基类
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
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
