using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Dota2Utils
{
    public partial class Form1 : Form
    { 
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
  
        }
         
        private void AddDrag(Control Control) { Control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragFunction); }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        
        private void Form1_Load(object sender, EventArgs e)
        {
            IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents(); 
            
            m_GlobalHook.KeyUp += M_GlobalHook_KeyUp;
 
        }

        Timer agiestT = new Timer();
        Timer roshanT = new Timer();
        private void M_GlobalHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                activeAgies();
            }
            else if (e.KeyCode==Keys.PageDown)
            {
                activeRosh();
            }
            else if (e.KeyCode == Keys.End)
            {
                reset();
            }else if (e.KeyCode == Keys.Home)
            {
                this.TopMost = true;
                this.Visible = true;
            }
        }

        bool active = false;
        int sec = 59;
        int min = 4;

        bool Ractive = false;
        bool RDefSpawn = false;
        int Rsec = 59;
        int Rmin = 10;

        void reset()
        {
            label1.Text = "Idle";
            label1.ForeColor = Color.White;
            agiestT.Stop();
            agiestT.Dispose();
            sec = 59;
            min = 4;
            active = false;

            label2.Text = "Idle";
            label2.ForeColor = Color.White;
            roshanT.Stop();
            roshanT.Dispose();
            Rsec = 59;
            Rmin = 10;
            Ractive = false;
            RDefSpawn = false;

           this.Visible = false;
        }
        void activeAgies()
        {
            if (active)
                return;
            active = true;
            agiestT = new Timer();
            agiestT.Interval = 1000;
            agiestT.Tick += AgiestT_Tick;
            agiestT.Start();
            if (!Ractive)
            {
                activeRosh();
            }
            this.Visible = true;
        }
        void activeRosh()
        {
            if (Ractive)
                return;
            roshanT = new Timer();
            roshanT.Interval = 1000;
            roshanT.Tick += RoshanT_Tick;
            Ractive = true;
            roshanT.Start();
            this.Visible = true;
        }

        private void RoshanT_Tick(object sender, EventArgs e)
        {
    
            if (!Ractive)
            {
                return;
            }
      
            if (Rmin <3 && RDefSpawn==false)
            {
                Invoke(new MethodInvoker(delegate () {
                    label2.ForeColor = Color.Red;
                }));
                RDefSpawn = true;
            }
             
            if (Rmin == 0 && Rsec == 0)
            {
                roshanT.Stop();
                roshanT.Dispose();
                Invoke(new MethodInvoker(delegate () {
                    label2.Text = "DONE";
                }));
                active = false;
                return;
            }
 
            Invoke(new MethodInvoker(delegate () {
                label2.Text = Rmin.ToString("D2") + ":" + Rsec.ToString("D2");
            }));

            if (Rsec <= 0)
            {
                Rsec = 59;
                Rmin--;
            }
            else
            {
                Rsec--;
            }
       
        }

        private void AgiestT_Tick(object sender, EventArgs e)
        {
          
            if (!active)
            {
                return;
            }
            
            if (min == 0 && sec==0)
            {
                agiestT.Stop();
                agiestT.Dispose();
                Invoke(new MethodInvoker(delegate () {
                    label1.Text = "DONE";
                }));
                active = false;
                return;
            }

            Invoke(new MethodInvoker(delegate () {
                label1.Text = min.ToString("D2") + ":" + sec.ToString("D2");
            }));

            if (sec <=0)
            {
                sec = 59;
                min--;
            }
            else
            {
                sec--;
            }
  
        }
 

        private void dragFunction(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                // Checks if Y = 0, if so maximize the form
                if (this.Location.Y == 0) { this.WindowState = FormWindowState.Maximized; }
            }
        }
    }
}
