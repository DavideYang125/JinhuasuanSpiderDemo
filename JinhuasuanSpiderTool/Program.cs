using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace JinhuasuanSpiderTool
{
    class Program
    {
        static void Main(string[] args)
        {
            DataHandle.SyncData();
            return;
            ImgHandle.ReplaceImgUrl();
            return;
           
        }
    }
}
