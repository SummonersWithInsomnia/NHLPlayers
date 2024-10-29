using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHLPlayers
{
    public partial class App : Form
    {
        private Filter _filter;
        private string[] _headers;
        private List<Player> _players;
        
        public App(string[] headers, List<Player> players)
        {
            InitializeComponent();
            
            _headers = headers;
            _players = players;
            
            _filter = new Filter(this, _headers, _players);
        }

        // Set the data source of the data grid view
        public void SetTableData(List<Player> players)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke((MethodInvoker) delegate { SetTableData(players); });
            }
            else
            {
                dataGridView.DataSource = null;
                dataGridView.DataSource = players;
            }
        }
    }
}