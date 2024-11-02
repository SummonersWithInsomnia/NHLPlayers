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

            toolStripStatusLabel.Text = "F3: Filter";

            var sortablePlayerList = new SortableList<Player>(_players.ToList());
            
            dataGridView.DataSource = sortablePlayerList;
            dataGridView.AutoGenerateColumns = false;

            for (int i = 0; i < _headers.Length; i++)
            {
                dataGridView.Columns[i].HeaderText = _headers[i];
            }
        }

        // Set the data source of the data grid view
        public void SetTableData(SortableList<Player> players)
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