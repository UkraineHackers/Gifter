using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gifter.DataAccess.HelperMethods
{
    class PrepareData
    {
        public static void getTagsFromString(String tags) {
            String[] t = tags.Split(',');
            foreach (String tag in t)
            {
                String res = tag.Trim();

            }
        }
    }
}
