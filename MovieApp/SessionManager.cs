using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp
{
   
        public static class SessionManager
        {
            public static int? LoggedInUserId { get; set; }  // Giriş yapan kullanıcının ID'si
            public static string LoggedInUsername { get; set; }  // Giriş yapan kullanıcının adı
        }

    }
