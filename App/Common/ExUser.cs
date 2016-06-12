using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataStore.Entity;

namespace AsEasy.Common
{
    public class ExUser
    {
        public LoginUser User { get; set; }

        private List<Permission> _ownedPermissions;
        public List<Permission> OwnedPermissions
        {
            get
            {
                if (_ownedPermissions == null)
                {
                    //此处从DB或缓存加载
                    _ownedPermissions = new List<Permission>();
                    _ownedPermissions.Add(Permission.HomeIndex);
                }

                return _ownedPermissions;
            }
        }
    }
}