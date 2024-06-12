using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class BasicEmijis
    {
        public static string ParseEmojis(string content)
        {
            content = content.Replace(":D", "https://vn.images.search.yahoo.com/search/images?p=%F0%9F%98%80&fr=mcafee&type=E210VN91215G0&imgurl=https%3A%2F%2Fcdn.whatemoji.org%2Fwp-content%2Fuploads%2F2020%2F07%2FGrinning-Face-With-Big-Eyes-Emoji.png#id=4&iurl=https%3A%2F%2Fcdn.whatemoji.org%2Fwp-content%2Fuploads%2F2020%2F07%2FGrinning-Face-With-Big-Eyes-Emoji.png&action=click");

            return content;
        }
    }
}
