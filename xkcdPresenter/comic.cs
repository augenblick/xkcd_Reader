using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xkcdPresenter
{
    class Comic
    {
        public int Num { get; set; }
        public Uri Image { get; set; }
        public string Title { get; set; }
        public string Alt { get; set; }
    }
}
