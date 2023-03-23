using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using CsvHelper;
using System.Globalization;
using System.Reflection;
using System.Media;

namespace flights01
{
    public partial class Form1 : Form
    {
        #region Win32 API Stuff
        // Define the Win32 API methods we are going to use
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        /// Define our Constants we will use
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_STRING = 0x0;

        #endregion

        // The constants we'll use to identify our custom system menu items
        public const Int32 _AboutSysMenuID = 1001;
        string URL_Arrivals = "https://flydata.avinor.no/XmlFeed.asp?TimeFrom=0&TimeTo=4&airport=OSL&direction=A";
        string URL_Departures = "https://flydata.avinor.no/XmlFeed.asp?TimeFrom=0&TimeTo=4&airport=OSL&direction=D";
        public bool lineCounter = true;
        public bool flagArrival = true;
        public string[] IATA = new string[7541];
        public string[] Loc = new string[7541];

        public Form1()
        {
            InitializeComponent();
            lbArrival.DrawItem += new DrawItemEventHandler(lbArrival_DrawItem);
            cbInternational.Checked = true;
            cbSchengen.Checked = true;
            cbDomestic.Checked = false;
            lbArrival.ItemHeight = 34;

            // Get codes
            try
            {
                using (var reader = new StreamReader(MyDirectory() + @"\airport-codes.csv"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        // var records = csv.GetRecords<dynamic>();
                        var recs = csv.GetRecords<APC>();
                        int i = 0;
                        foreach (var linje in recs)
                        {
                            // Do something with values in each row
                            IATA[i] = linje.iata_code;
                            string[] coordinate = linje.coordinates.Split(',');
                            Loc[i] = linje.name + "/" + linje.municipality + " [" + coordinate[1] + " " + coordinate[0] + "]";
                            i++;
                        }
                        lblDebug.Text = IATA[100] + " - " + Loc[100];
                        // lblDebug.Text = i.ToString();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Please copy the required file \"airport-codes.csv\" to the program folder", "File Missing");
                // this.Close();
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }
            }
        }


        public enum WindowMessages
        {
            wmSysCommand = 0x0112
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(this.Handle, false);

            InsertMenu(systemMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty); // <-- Add a menu seperator
            InsertMenu(systemMenuHandle, 6, MF_BYPOSITION, _AboutSysMenuID, "About...");
        }



        protected override void WndProc(ref Message msg)
        {
            // Now we should catch the WM_SYSCOMMAND message and process it.
            // We override the WndProc to catch the WM_SYSCOMMAND message.
            // You don't have to look this message up in the MSDN; it is
            // defined in WindowMessages enumeration.
            // The WParam of the message contains the ID that was pressed.
            // It is the same value you have passed through InsertMenu()
            // or AppendMenu() member functions of my class.
            // Just check for them and do the proper action.
            //
            if (msg.Msg == (int)WindowMessages.wmSysCommand)
            {
                switch (msg.WParam.ToInt32())
                {

                    case _AboutSysMenuID:
                        { // Our about id
                            MessageBox.Show("A small program for displaying\nArrivals at OSL (2022-08-19/ralm)", "About Flight");
                        }
                        break;

                        // TODO: Add more handles, for more menu items

                }
            }
            // Call base class function
            base.WndProc(ref msg);
        }

        public string getLocation(string iata)
        {
            for (int i = 0; i < 7540; i++)
            {
                if (iata.CompareTo(IATA[i]) == 0)
                {
                    return Loc[i];
                }
            }
            return "Unknown";
        }

        public string extractData(string str)
        {
            int start = str.IndexOf(">");
            start++;
            int stop = str.Substring(start).IndexOf("<");
            if (stop > 0)
                return str.Substring(start, stop);
            return "";
        }

        public string fixTime(string str)
        {
            int hour = Int32.Parse(str.Substring(0, 2));
            hour += 2;
            string h = String.Format("{0}", hour);
            if (hour < 10)
                h = " " + h;
            return h + str.Substring(2, 3);
        }

        // <schedule_time>2022-08-18T12:30:00Z</schedule_time>
        //                           =====
        public string parseSchedule(string str)
        {
            string info;

            int start = str.IndexOf(">");
            start++;
            int stop = str.Substring(start).IndexOf("<");
            if (stop > 0)
            {
                info = str.Substring(start + 11, 5);
                info = fixTime(info);
                return info;
            }
                return "";
        }

        public string parseStatus(string line)
        {
            string status_code = "";
            string newTime = "";

            int start = line.IndexOf("=");
            if (start > 0)
            {
                start++;
                start++;
                status_code = line.Substring(start, 1);
            }

            start = line.IndexOf("time");
            if (start > 0)
            {
                start += 17;
                newTime = line.Substring(start, 5);
                newTime = fixTime(newTime);
            }
            if (status_code.Equals("A"))
                return " Arrived: " + newTime;
            else
                return "Expected: " + newTime;
        }

        public void fetchInfo()
        {
            int start = -1;
            String line;
            String fLight = "";
            String fromAirport = "";
            string scheduledArrival = "";
            string arrStatus = "";
            string status = "";
            string flightType = "";
            StreamReader reader;

            lbArrival.Items.Clear();
            if (flagArrival)
                reader = new StreamReader(WebRequest.Create(URL_Arrivals).GetResponse().GetResponseStream());
            else
                reader = new StreamReader(WebRequest.Create(URL_Departures).GetResponse().GetResponseStream());
            while ((line = reader.ReadLine()) != null)
            {
                start = line.IndexOf("status code");
                if (start > 0 )
                    status = parseStatus(line);

                start = line.IndexOf("flight_id");
                if (start > 0)
                {
                    fLight = extractData(line);
                    if (fLight.Length == 5)
                        fLight = fLight + " ";
                }
    
                start = line.IndexOf("airport");
                if (start > 0)
                {
                    fromAirport = extractData(line);
                }

                start = line.IndexOf("dom_int");
                if (start > 0)
                {
                    flightType = extractData(line);
                }


                start = line.IndexOf("schedule");
                if (start > 0)
                {
                    scheduledArrival = parseSchedule(line);
                }

                start = line.IndexOf("arr_dep");
                if (start > 0)
                    arrStatus = extractData(line);

                start = line.IndexOf("</flight>");
                if (start > 0)
                {
                    if (((flightType.Equals("D") && (cbDomestic.Checked == true)))
                        ||
                        ((flightType.Equals("S") && (cbSchengen.Checked == true)))
                        ||
                        ((flightType.Equals("I") && (cbInternational.Checked == true))))
                        if (flagArrival)
                            lbArrival.Items.Add(fLight + " From: " + fromAirport + " [" + flightType + "] Scheduled Arrival: " + scheduledArrival + " " + status);
                        else
                            lbArrival.Items.Add(fLight + " To: " + fromAirport + " [" + flightType + "] Scheduled Departure: " + scheduledArrival + " " + status);
                    arrStatus = "";
                    scheduledArrival = "";
                    fromAirport = "";
                    fLight = "";
                    flightType = "";
                    status = "";
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            flagArrival = !flagArrival;
            fetchInfo();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            tbLastUpdate.Text = now.ToString();
            fetchInfo();
        }

        private void cbDomestic_CheckedChanged(object sender, EventArgs e)
        {
            fetchInfo();
        }

        private void cbSchengen_CheckedChanged(object sender, EventArgs e)
        {
            fetchInfo();
        }

        private void cbInternational_CheckedChanged(object sender, EventArgs e)
        {
            fetchInfo();
        }

        private void lbArrival_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;

            // draw the background color you want
            // mine is set to olive, change it to whatever you want
            lineCounter = !lineCounter;
            if (lineCounter)
                g.FillRectangle(new SolidBrush(Color.Aquamarine), e.Bounds);
            else
                g.FillRectangle(new SolidBrush(Color.Yellow), e.Bounds);
            // draw the text of the list item, not doing this will only show
            // the background color
            // you will need to get the text of item to display
            ListBox lb = (ListBox)sender;
            g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        private string MyDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private void lbArrival_MouseMove(object sender, MouseEventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox == null) return;

            // set tool tip for listbox
            var strTip = string.Empty;
            var index = listbox.IndexFromPoint(e.Location);

            if ((index >= 0) && (index < listbox.Items.Count))
                strTip = listbox.Items[index].ToString();
            if (strTip.Length > 1)
            {

                if (flagArrival)
                {
                    lblDebug.Text = getLocation(strTip.Substring(13, 3));
                    // Clipboard.SetText(strTip.Substring(13, 3));
                }
                else
                {
                    lblDebug.Text = getLocation(strTip.Substring(11, 3));
                    // Clipboard.SetText(strTip.Substring(11, 3));
                }
            }
        }

        private void lbArrival_MouseClick(object sender, MouseEventArgs e)
        {
            
            var listbox = sender as ListBox;
            if (listbox == null) return;

            // set tool tip for listbox
            var strTip = string.Empty;
            var index = listbox.IndexFromPoint(e.Location);

            if ((index >= 0) && (index < listbox.Items.Count))
                strTip = listbox.Items[index].ToString();
            if (strTip.Length > 1)
            {
                int loc0 = 0;
                int loc1 = 0;
                if (flagArrival)
                {
                    // Copy coordinates to clipboard
                    string s = getLocation(strTip.Substring(13, 3));
                    loc0 = s.IndexOf('[');
                    loc1 = s.IndexOf(']');
                    // MessageBox.Show(strTip.Substring(13, 3) + " - " + s.Substring(loc0+1, loc1-loc0-1), "Arrival");
                    Clipboard.SetText(s.Substring(loc0 + 1, loc1 - loc0 - 1));
                    // MessageBox.Show(MyDirectory(), "Test");
                    SystemSounds.Asterisk.Play();
                }
                else
                {
                    string s = getLocation(strTip.Substring(11, 3));
                    loc0 = s.IndexOf('[');
                    loc1 = s.IndexOf(']');
                    // MessageBox.Show(strTip.Substring(11, 3) + " - " + s.Substring(loc0+1, loc1 - loc0-1), "Departure");
                    Clipboard.SetText(s.Substring(loc0 + 1, loc1 - loc0 - 1));
                    SystemSounds.Asterisk.Play();
                }
                //strTip.Substring(13, 3);

            }
        }
    }
}
