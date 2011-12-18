using System;
using System.Windows.Forms;
using PowerScada.Properties;
using PowerScada;
using PowerScada.Forms;



namespace PowerScada
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Utility.AddEditbuttonCommands();
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;

            if (loginForm.ShowDialog() == DialogResult.OK)
            
                Application.Run(new MainForm());
            }
        }
    }
