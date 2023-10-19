using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeePassEnforcedConfigExtender
{
    public partial class Warn_Window : Form
    {
        public TaskCompletionSource<string> UserSelectionTask {get; set;}
        public Warn_Window()
        {
            InitializeComponent();
            UserSelectionTask = new TaskCompletionSource<string>();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            UserSelectionTask.SetResult("cancel");
            this.Close();   
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
            UserSelectionTask.SetResult("continue");
            this.Close();
        }

        private void lbl_warn_text_Click(object sender, EventArgs e)
        {

        }
    }
}
