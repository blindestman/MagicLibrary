using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Jayrock.Json.Conversion;
using System.IO;

namespace MagicLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Jayrock.Json.JsonObject AllCards;
        List<string> strs = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("..//..//AllCards.json")) // make sure there is a file there before we try to do stuff to it
            {
                // open up a new file stream and load it into memory
                StreamReader sr = new StreamReader("..//..//AllCards.json");
                string raw = sr.ReadToEnd(); // non-asynchronous
                sr.Close(); // remember to close the file stream or stuff breaks

                var obj = JsonConvert.Import(raw);
                if (obj is Jayrock.Json.JsonObject)
                {
                    AllCards = ((Jayrock.Json.JsonObject)obj);
                    strs = AllCards.Names.Cast<string>().ToList();
                }
            }
            else
                MessageBox.Show("Cannot find source file!"); // show error message so we know something went wrong
        }
                
        private void btn_Search_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(strs.FindAll(x => x.ToLower().Contains(tb_searchField.Text.ToLower())).ToArray());
        }
    }
}
