using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastService.Models.Login
{
    public class MenuItemModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
    }
}
