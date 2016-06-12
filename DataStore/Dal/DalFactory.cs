using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Dal
{
    public class DalFactory
    {
        private static readonly ILoginUserService _loginUserService = new LoginUserService();

        public static ILoginUserService LoginUserService
        {
            get { return _loginUserService; }
        }
    }
}
