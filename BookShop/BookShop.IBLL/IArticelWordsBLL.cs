using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Model;

namespace BookShop.IBLL
{
    public partial interface IArticelWordsBLL:IBaseBLL<ArticelWords>
    {
        bool CheckBanndWord(string msg);
        bool CheckModWord(string msg);
    }
}
