using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHLPlayers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string filePath = "data.csv";
                var rows = File.ReadLines(filePath);
                var headers = rows.First().Split(',');
                List<Player> players = rows.Skip(1).Select(row =>
                {
                    var values = row.Split(',');
                    var item = new Player()
                    {
                        Name = values[0],
                        Team = values[1],
                        Pos = values[2],
                        GP = Double.Parse(values[3]),
                        G = Double.Parse(values[4]),
                        A = Double.Parse(values[5]),
                        P = Double.Parse(values[6]),
                        PlusOrMinus = Double.Parse(values[7]),
                        PIM = Double.Parse(values[8]),
                        PPerGP = values[9],
                        PPG = Double.Parse(values[10]),
                        PPP = Double.Parse(values[11]),
                        SHG = Double.Parse(values[12]),
                        SHP = Double.Parse(values[13]),
                        GWG = Double.Parse(values[14]),
                        OTG = Double.Parse(values[15]),
                        S = Double.Parse(values[16]),
                        SPercentage = Double.Parse(values[17]),
                        TOIPerGP = values[18],
                        ShiftsPerGP = Double.Parse(values[19]),
                        FOWPercentage = Double.Parse(values[20])
                    };
                    return item;
                }).ToList();
            
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new App(headers, players));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "NHL Player Viewer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}