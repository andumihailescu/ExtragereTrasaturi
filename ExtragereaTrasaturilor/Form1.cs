using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using WordFrequency;

namespace ExtragereaTrasaturilor
{
    public partial class Form1 : Form
    {
        Data data;

        public Form1()
        {
            InitializeComponent();

            data = new Data();
        }

        private void processDataBtn_Click(object sender, EventArgs e)
        {
            
            if(chooseFolderCb.Text == "Testing + Training")
            {
                data.ProcessData();
            }
            else
            {
                data.LoadLargeData();
            }
        }
    }
}