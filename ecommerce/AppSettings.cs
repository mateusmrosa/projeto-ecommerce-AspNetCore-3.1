using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce
{
    public class AppSettings
    {
        public string StringConexaoMysql { get; set; }
        public string EmialPadrao { get; set; }
        public string DirTemp { get; set; }
        public int CookieTempoVida { get; set; }

    }
}
