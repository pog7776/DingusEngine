using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DingusEngine
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();

            TextWriter writer = new StringWriter();
            Console.SetOut(writer);
        }
    }
}
