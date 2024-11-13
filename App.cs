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
        // ---- Gary Simwawa's Code ----
        //To store the sorting order (ASC or DESC) on each column in the grid view
        private Dictionary<DataGridViewColumn, SortOrder> _columnSortOrder = new Dictionary<DataGridViewColumn, SortOrder>();

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];

            //Determine the sort order, switching to ASC and DESC every click
            SortOrder sortOrder;
            if (_columnSortOrder.ContainsKey(column))
            {
                sortOrder = _columnSortOrder[column] == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                sortOrder = SortOrder.Ascending;
            }

            //Update sort order for the column
            _columnSortOrder[column] = sortOrder;

            //Sort grid based on column and sorting order
            dataGridView.Sort(column, sortOrder == SortOrder.Ascending
                ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }
        //Daniel's Part
        //method to handle keyboard shortcuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                HandleFilterForm(); // this handles the filter form
                return true; // Indicate method has been called
            }

            // base method for other key events
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
