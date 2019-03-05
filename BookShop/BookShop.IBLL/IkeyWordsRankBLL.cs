using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.IBLL
{
    public partial interface IkeyWordsRankBLL:IBaseBLL<Model.keyWordsRank>
    {
        void DeleteKeyWordsRank();
        void InsertKeyWordsRank();
        List<string> GetSearchWord(string msg);
    }
}
