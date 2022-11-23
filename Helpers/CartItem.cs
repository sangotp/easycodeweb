using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Helpers
{
    public class CartItem
    {
        public int quantity { set; get; }
        public Course? course { set; get; }
    }
}
