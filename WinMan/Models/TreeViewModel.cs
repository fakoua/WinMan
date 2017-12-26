using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMan.Models
{
    public class TreeViewModel
    {
        public string text { get; set; }
        public string id { get; set; }
        public string icon { get; set; }
        public bool children { get; set; }
    }
}
