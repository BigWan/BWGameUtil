using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigRogue.Persistent {

    /// <summary>
    /// 从文件中来的数据记录
    /// </summary>
    public interface IRecord {

        int id { get; }

        bool isEmpty();

        void InitFromLine(string s);
        
    }
}
