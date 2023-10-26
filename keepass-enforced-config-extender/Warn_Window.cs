using System;
using System.Windows.Forms;

namespace KeePassEnforcedConfigExtender
{
    public partial class Warn_Window : Form
    {
 //       public TaskCompletionSource<string> UserSelectionTask {get; set;}
        public bool ContinueSave;
        public Warn_Window()
        {
            InitializeComponent();
            bool ContinueSave;
 //           UserSelectionTask = new TaskCompletionSource<string>();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
 //           UserSelectionTask.SetResult("cancel");
            ContinueSave = false;
            this.Close();   
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
//            UserSelectionTask.SetResult("continue");
            ContinueSave = true;
            this.Close();
        }

        private void lbl_warn_text_Click(object sender, EventArgs e)
        {

        }

        private void Warn_Window_Load(object sender, EventArgs e)
        {

        }

        private void lbl_test2_Click(object sender, EventArgs e)
        {

        }
    }
}
