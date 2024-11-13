using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NHLPlayers
{
    public partial class Filter : Form
    {
        private App _parent;
        private string[] _headers;
        private List<Player> _originalList;
        
        private string _filters;
        private string _orders;
        
        private List<Player> _resultList;
        
        public Filter(App parent, string[] headers, List<Player> players)
        {
            InitializeComponent();

            // Initialize data from the parent form
            _parent = parent;
            _headers = headers;
            _originalList = players;
            
            // Initialize filter and order
            _filters = "";
            _orders = "";
        }
        
        private void tbFilters_TextChanged(object sender, EventArgs e)
        {
            _filters = tbFilters.Text;
            getResult();
        }

        private void tbOrders_TextChanged(object sender, EventArgs e)
        {
            _orders = tbOrders.Text;
            
            // Ignore the errors when the user inputs wrong headers
            try
            {
                getResult();
            }
            catch (Exception exception)
            {
            }
        }

        // Get the result by using the filters and orders
        // and set the table data in the parent form
        private void getResult()
        {
            _resultList = new List<Player>(_originalList);

            FilterLinqCmd flc = getFilterLinqCmd();
            OrderLinqCmd olc = getOrderLinqCmd();
            
            string filterLinqCmdCombined = "";
            for (int i = 0; i < flc.Args.Count; i++)
            {
                // Combine the filter linq commands
                // If it's not the last command, add "&&" to the end
                if (i < flc.Args.Count - 1)
                {
                    filterLinqCmdCombined += flc.Cmds[i];
                    filterLinqCmdCombined += "&&";
                }
                // If it's the last command, add nothing to the end
                else
                {
                    filterLinqCmdCombined += flc.Cmds[i];
                }
            }

            if (filterLinqCmdCombined != "")
            {
                _resultList = new List<Player>(_resultList.AsQueryable()
                    .Where(filterLinqCmdCombined, flc.Args.ToArray()).ToList());
                _parent.SetTableData(new SortableList<Player>(_resultList));
            }
            
            string orderLinqCmdCombined = "";
            for (int i = 0; i < olc.Cmds.Count; i++)
            {
                // Combine the order linq commands
                // If it's not the last command, add "," to the end
                if (i < olc.Cmds.Count - 1)
                {
                    orderLinqCmdCombined += olc.Cmds[i];
                    orderLinqCmdCombined += ",";
                }
                // If it's the last command, add nothing to the end
                else
                {
                    orderLinqCmdCombined += olc.Cmds[i];
                }
            }
            
            if (orderLinqCmdCombined != "")
            {
                _resultList = new List<Player>(_resultList.AsQueryable()
                    .OrderBy(orderLinqCmdCombined).ToList());
                _parent.SetTableData(new SortableList<Player>(_resultList));
            }
            
        }
        
        private struct FilterLinqCmd
        {
            public List<string> Cmds;
            public List<string> Args;
        }
        
        // Get the linq commands and arguments for the filters
        private FilterLinqCmd getFilterLinqCmd()
        {
            // Remove all whitespaces
            _filters = Regex.Replace(_filters, @"\s+", "");
            
            // Split the filters
            string[] filterList = _filters.Split(',');
            
            List<string> linqCmds = new List<string>();
            List<string> linqArgs = new List<string>();
            
            if (_filters.Length == 0)
            {
                return new FilterLinqCmd { Cmds = linqCmds, Args = linqArgs };
            }
            
            string[] stringProps = { "Player", "Name", "Team", "Pos", "PPerGP", "P/GP", "TOIPerGP", "TOI/GP" };
            string[] doubleProps = { "GP", "G", "A", "P", "PlusOrMinus", "+/-", "PIM", "PPG", "PPP", "SHG", 
                "SHP", "GWG", "OTG", "S", "SPercentage", "S%", "ShiftsPerGP", "Shifts/GP", "FOWPercentage", "FOW%" };
            string stringPropValuePattern = @"([a-zA-Z/]+)([>=<]{1,2})([a-zA-Z0-9\.\s]+)";
            string doublePropValuePattern = @"([a-zA-Z+\-/%]+)([>=<]{1,2})([-]?\d+\.?\d*)";
            
            // Loop through the filter list
            for (int i = 0; i < filterList.Length; i++)
            {
                // Check if the filter contains a string property
                if (stringProps.Any(sp => filterList[i].Contains(sp)))
                {
                    Match m = Regex.Match(filterList[i], stringPropValuePattern);
                    if (m.Success)
                    {
                        string left = m.Groups[1].Value;
                        string right = m.Groups[3].Value;

                        // Convert the left side to the correct property name
                        switch (left)
                        {
                            case "Player":
                                left = "Name";
                                break;
                            case "P/GP":
                                left = "PPerGP";
                                break;
                            case "TOI/GP":
                                left = "TOIPerGP";
                                break;
                        }
                        
                        // Add the linq command and argument to the lists
                        linqCmds.Add(left + ".Contains(@" + linqCmds.Count + ")");
                        linqArgs.Add(right);
                    }
                }
                else if (doubleProps.Any(dp => filterList[i].Contains(dp)))
                {
                    Match m = Regex.Match(filterList[i], doublePropValuePattern);
                    if (m.Success)
                    {
                        string left = m.Groups[1].Value;
                        string middle = m.Groups[2].Value;
                        string right = m.Groups[3].Value;

                        // Convert the left side to the correct property name
                        switch (left)
                        {
                            case "+/-":
                                left = "PlusOrMinus";
                                break;
                            case "S%":
                                left = "SPercentage";
                                break;
                            case "Shifts/GP":
                                left = "ShiftsPerGP";
                                break;
                            case "FOW%":
                                left = "FOWPercentage";
                                break;
                        }
                        
                        // Add the linq command and argument to the lists
                        linqCmds.Add(left + middle + "@" + linqCmds.Count);
                        linqArgs.Add(right);
                    }
                }
            }
            
            return new FilterLinqCmd { Cmds = linqCmds, Args = linqArgs };
        }
        
        private struct OrderLinqCmd
        {
            public List<string> Cmds;
        }
        
        // Get the linq commands for the orders
        private OrderLinqCmd getOrderLinqCmd()
        {
            // Remove all whitespaces
            _orders = Regex.Replace(_orders, @"\s+", " ");
            
            // Split the orders
            string[] orderList = _orders.Split(',');
            
            List<string> linqCmds = new List<string>();
            
            if (_orders.Length == 0)
            {
                return new OrderLinqCmd { Cmds = linqCmds };
            }
            
            string orderPattern = @"([a-zA-Z+\-/%]+)([\s]+)([a-zA-Z]+)";
            
            for (int i = 0; i < orderList.Length; i++)
            {
                if (_headers.Any(h => orderList[i].Contains(h)))
                {
                    Match match = Regex.Match(orderList[i], orderPattern);
                    if (match.Success)
                    {
                        string left = match.Groups[1].Value;
                        string right = match.Groups[3].Value;

                        switch (left)
                        {
                            case "Player":
                                left = "Name";
                                break;
                            case "P/GP":
                                left = "PPerGP";
                                break;
                            case "TOI/GP":
                                left = "TOIPerGP";
                                break;
                            case "+/-":
                                left = "PlusOrMinus";
                                break;
                            case "S%":
                                left = "SPercentage";
                                break;
                            case "Shifts/GP":
                                left = "ShiftsPerGP";
                                break;
                            case "FOW%":
                                left = "FOWPercentage";
                                break;
                        }

                        switch (right)
                        {
                            case "asc":
                            case "ASC":
                                right = "asc";
                                break;
                            case "des":
                            case "DES":
                            case "desc":
                            case "DESC":
                                right = "desc";
                                break;
                            default:
                                right = "asc";
                                break;
                        }
                        
                        linqCmds.Add(left + " " + right);
                    }
                }
            }
            
            return new OrderLinqCmd { Cmds = linqCmds };
        }

        private void Filter_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parent.SetTableData(new SortableList<Player>(_originalList));
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                Hide();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}