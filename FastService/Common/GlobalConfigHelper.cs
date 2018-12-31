using FastService.Models;
using FastService.Models.Notificacion;
using FastService.Models.Reports;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace FastService.Common
{
    public class GlobalConfigHelper
    {
        private FastServiceEntities _db { get; set; }

        public GlobalConfigHelper()
        {
            _db = new FastServiceEntities();
        }

        public string GetVal(string key)
        {
            return _db.GlobalConfig.Where(x => x.Key == key).Select(y => y.Value).FirstOrDefault();
        }

        public void SetVal(string key, string value)
        {
            var entity = _db.GlobalConfig.Where(x => x.Key == key).FirstOrDefault();
            entity.Value = value;

            _db.SaveChanges();
        }

    }
}