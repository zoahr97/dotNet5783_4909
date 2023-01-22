using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for cartuser.xaml
    /// </summary>
    public partial class cartuser : Window
    {
        private IBl bl = Bl.Instance;
        Cart cart = new Cart();//יצירת אובייקט סל הקניות

        public cartuser()
        {
            InitializeComponent();       
            cart.CustomerName = TCustomerName.Text;
            cart.CustomerEmail = TCustomerName + "@gmail.com";
            cart.CustomerAdress = TCustomerAdress.Text;
            cart.Items = new List<BO.OrderItem>();// רשימת פרטי הזמנה/מוצרים ריקה
            cart.TotalPriceCart = 0.0;//כרגע הסכום הכולל של סל הקניות הינו :0 כי עדיין לא נוספו מוצרים לסל הקניות

        }

       
    }
}
