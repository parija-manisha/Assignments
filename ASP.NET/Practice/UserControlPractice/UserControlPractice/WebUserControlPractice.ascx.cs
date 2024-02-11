using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserControlPractice
{
    public partial class WebUserControlPractice : System.Web.UI.UserControl
    {
        public string PageName
        {
            get; set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = _header;
            Property.Text = PageName;
        }
        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

       
    }
}