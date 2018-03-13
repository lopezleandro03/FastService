using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastService.Models.Login
{
    public class MenuModel
    {
        public List<string> Roles { get; set; }
        public List<MenuItemModel> MenuItems {get;set;}
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public object UserId { get; internal set; }
    }
}
