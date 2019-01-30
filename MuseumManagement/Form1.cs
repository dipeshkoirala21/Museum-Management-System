using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuseumManagement
{
    public partial class Form1 : Form
    {
        List<MuseumData> lst = new List<MuseumData>();
        List<VisitorsData> vst = new List<VisitorsData>();
        private int index;

        public Form1()
        {
            InitializeComponent();
           read_data_from_csv();
            read_data_from_visitorsentry();
        }

        void ClearData()
        {
            card_txtbox.Clear();
            name_txtbox.Clear();
            email_txtbox.Clear();
            address_txtbox.Clear();
            occupation_txtbox.Clear();
            cont_txtbox.Clear();
            

        }

        void ClearData1()
        {
            cardchk_txtbox.Clear();
            day_txtbox.ResetText();
            visit_txtbox.Clear();
            intime_txtbox.ResetText();
            


        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (card_txtbox.Text.Equals("") || name_txtbox.Text.Equals("") || email_txtbox.Text.Equals("") || cont_txtbox.Text.Equals("")
              || address_txtbox.Text.Equals("") || occupation_txtbox.Text.Equals(""))
            {
                error_message.Text = "Do not leave Fields empty";
            }
            else
            {
                VisitorsData existingUser = vst.Where(x => x.card_no == card_txtbox.Text).FirstOrDefault();
                VisitorsData existingEmail = vst.Where(x => x.email == email_txtbox.Text).FirstOrDefault();
                if (existingUser != null)
                {
                    error_message.Text = "Card Number Already Exists ";
                }
                else if (existingEmail != null)
                {
                    error_message.Text = "Email already exists";
                }
                else
                {
                    error_message.Text = "You are now registered";
                    VisitorsData v = new VisitorsData();
                    v.card_no = card_txtbox.Text;
                    v.name = name_txtbox.Text;
                    v.email = email_txtbox.Text;
                    v.address = address_txtbox.Text;
                    v.occupation = occupation_txtbox.Text;
                    v.contact = cont_txtbox.Text;
                    vst.Add(v);
                    visitors_datagridview.Rows.Add(card_txtbox.Text, name_txtbox.Text, email_txtbox.Text, address_txtbox.Text, occupation_txtbox.Text, cont_txtbox.Text);
                    ClearData();
                    error_message.Text = "";
                    try
                    {
                        string file_path = @".\visitorsinfo.csv";
                        using (var writer = new StreamWriter(file_path, append: true))
                        {

                            var line = string.Format("{0}, {1}, {2}, {3}, {4}, {5} , {6}", v.card_no,
                                v.name, v.email, v.address, v.occupation, v.contact, v.gender);


                            writer.WriteLine(line);
                            writer.Flush();

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error: Cannot write to the file");
                    }
                }
            }
        }
        public void read_data_from_csv()
        {
            try
            {
                String file_path = @".\visitorsinfo.csv";
                using (var reader = new StreamReader(file_path))
                {
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        VisitorsData data = new VisitorsData(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                        visitors_datagridview.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                        Console.WriteLine(values[0]);
                        vst.Add(data);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot read data from the file");
            }

        }
        public void read_data_from_visitorsentry()
        {
            try
            {
                String file_path = @".\visitorsentry.csv";
                using (var reader = new StreamReader(file_path))
                {
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        MuseumData entry = new MuseumData(values[0], values[1], values[2], values[3], values[4], values[5]);
                        datagridview1.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);
                        Console.WriteLine(values[0]);
                        lst.Add(entry);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot read data from the file");
            }

        }

        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MuseumData m = new MuseumData();
            if (e.ColumnIndex == 6)
            {
                // MessageBox.Show((e.RowIndex+1).ToString());
                int i = e.RowIndex;
                string inTime = datagridview1.Rows[i].Cells[3].Value.ToString();
                string outTime = DateTime.Now.ToShortTimeString();
                datagridview1.Rows[i].Cells[4].Value = outTime;

                DateTime d1 = DateTime.Parse(inTime);
                DateTime d2 = DateTime.Parse(outTime);
                TimeSpan ts = d2.Subtract(d1);
                double minutes = ts.TotalMinutes;
                int min = Convert.ToInt32(minutes);
                datagridview1.Rows[i].Cells[5].Value = min + " min ";
                MuseumData mem = new MuseumData();
                try
                {
                    string file_path = @".\visitorsentry.csv";
                    using (var writer = new StreamWriter(file_path, append: true))
                    {

                        writer.WriteLine(datagridview1.Rows[i].Cells[0].Value + "," + datagridview1.Rows[i].Cells[1].Value + "," + datagridview1.Rows[i].Cells[2].Value + "," + datagridview1.Rows[i].Cells[3].Value + "," + datagridview1.Rows[i].Cells[4].Value + "," + datagridview1.Rows[i].Cells[5].Value);
                        writer.Flush();
                    }
                }
                catch
                {
                    MessageBox.Show("Error: Cannot write to the file");
                }

            }
            
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            MuseumData m = new MuseumData();
            m.card_no = cardchk_txtbox.Text;
            m.name = visit_txtbox.Text;
            m.day = day_txtbox.Text;
            m.in_time = intime_txtbox.Text;
            
            lst.Add(m);
            datagridview1.Rows.Add(cardchk_txtbox.Text, visit_txtbox.Text, day_txtbox.Text, intime_txtbox.Text);
            ClearData1();
            
        

    }

        private void chk_btn_Click(object sender, EventArgs e)
        {
            VisitorsData existingData = vst.Where(x => x.card_no == cardchk_txtbox.Text).FirstOrDefault();
            if (existingData != null && existingData.card_no == cardchk_txtbox.Text)
            {
                visit_txtbox.Text = existingData.name;
                visitorschk_lbl.Text="Visitors Found";
                add_btn.Enabled = true;

            }
            else
            {

                MessageBox.Show("Not found.Register First.");
                visit_txtbox.Text = "";
                cardchk_txtbox.Text = "";
                add_btn.Enabled = false;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        
        private void btn_loadchart_Click(object sender, EventArgs e)
        {
            //chart1.Series.Clear();
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }


            int durationM = 0; int durationT = 0; int durationW = 0; int durationTh = 0; int durationF = 0;

            foreach (var row in lst)
            {
                if (row.day == "Monday")
                {
                    durationM = Convert.ToInt32(row.total_duration) + durationM;

                }
                if (row.day == "Tuesday")
                {
                    durationT = Convert.ToInt32(row.total_duration) + durationT;

                }
                if (row.day == "Wednesday")
                {
                    durationW = Convert.ToInt32(row.total_duration) + durationW;

                }
                if (row.day == "Thursday")
                {
                    durationTh = Convert.ToInt32(row.total_duration) + durationTh;

                }
                if (row.day == "Friday")
                {
                    durationF = Convert.ToInt32(row.total_duration) + durationF;

                }
            }
            this.chart1.Series["Duration"].Points.AddXY("Monday", durationM);
            this.chart1.Series["Duration"].Points.AddXY("Tueday", durationT);
            this.chart1.Series["Duration"].Points.AddXY("Wednesday", durationW);
            this.chart1.Series["Duration"].Points.AddXY("Thursday", durationTh);
            this.chart1.Series["Duration"].Points.AddXY("Friday", durationF);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            visitors_datagridview[0, index].Value = card_txtbox.Text;
            visitors_datagridview[1, index].Value = name_txtbox.Text;
            visitors_datagridview[2, index].Value = email_txtbox.Text;
            visitors_datagridview[3, index].Value = address_txtbox.Text;
            visitors_datagridview[4, index].Value = occupation_txtbox.Text;
            visitors_datagridview[5, index].Value = cont_txtbox.Text;

            ClearData();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            visitors_datagridview.Rows.RemoveAt(index);
            ClearData();
            MessageBox.Show(@"Deleted successfully");
        }

        private void random_generate_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rnd = new Random();
            int card = rnd.Next(1000, 90000);
            VisitorsData existingCard = vst.Where(x => x.card_no == card.ToString()).FirstOrDefault();

            while (existingCard != null)
            {
                card = rnd.Next(1000, 90000);
            }

            card_txtbox.Text = card.ToString();
        }
        public List<string> checkin()
        {
            List<string> ls = new List<string>();


            using (StreamReader sr = new StreamReader(@".\visitorsentry.csv"))
            {
                string line;


                int i = 0;


                while ((line = sr.ReadLine()) != null)
                {
                    ls.Add(line);

                }
            }
            return ls;

        }
        private void weeklyReportGenerate_Click(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("en-US");
            Calendar myCal = info.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = info.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = info.DateTimeFormat.FirstDayOfWeek;

            this.weeklyReportDataGrid.Rows.Clear();
            this.weeklyReportDataGrid.Refresh();
            if (weekComboBox.Text == "1" || weekComboBox.Text == "2" || weekComboBox.Text == "3" || weekComboBox.Text == "4" || weekComboBox.Text == "5")
            {

                List<string> checkWeek = checkin();
                string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                for (int i = 0; i < days.Length; i++)
                {
                    int visitors = 0;
                    int totalMin = 0;
                    var visited = from date in checkWeek
                                  where date.Contains(days[i])
                                  select date;

                    List<string> cards1 = new List<string>();
                    foreach (string s in visited)
                    {


                        string vals = s.ToString();
                        string[] n = vals.Split(',');

                        Console.WriteLine("Index check: " + n[3]);
                        int wno = myCal.GetWeekOfYear(DateTime.Parse(n[3]), myCWR, myFirstDOW);
                        Console.WriteLine("WNO: " + wno);
                        if (DateTime.Parse(n[3]).Month.ToString() == DateTime.Parse(weeklyReportDatePicker.Value.ToString()).Month.ToString() && myCal.GetWeekOfYear(DateTime.Parse(n[3]), myCWR, myFirstDOW) == Convert.ToInt32(weekComboBox.Text))
                        {
                            cards1.Add(n[0]);
                        }
                    }

                    var uniq1 = cards1.Distinct();
                    foreach (var u in uniq1)
                    {
                        visitors = visitors + 1;

                    }

                    //minutes
                    foreach (string t in checkWeek)
                    {

                        var values = t.Split(',').ToList();
                        if (DateTime.Parse(values[3]).Month.ToString() == DateTime.Parse(weeklyReportDatePicker.Value.ToString()).Month.ToString() && myCal.GetWeekOfYear(DateTime.Parse(values[3]), myCWR, myFirstDOW) == Convert.ToInt32(weekComboBox.Text))
                        {


                            var qTotalTime = from date in values
                                             where date.Contains(days[i])
                                             select values[5];



                            foreach (var name in qTotalTime)
                            {

                                if (name != null && name != "")
                                {
                                    totalMin = Int32.Parse(name) + totalMin;
                                }


                            }
                        }

                    }

                    this.weeklyReportDataGrid.Rows.Add(days[i], visitors.ToString(), totalMin.ToString());


                }

            }
            else
            {
                MessageBox.Show("Pick a week.", "Error!");
            }
        }

        private void dailyReportDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Hola: " + dailyReportDateTimePicker.Value.ToShortDateString());
            totalVisitorsTextBox.Text = String.Empty;
            totalTimeSpentTextBox.Text = String.Empty;
            this.dailyVisitorsGridView.Rows.Clear();
            this.dailyVisitorsGridView.Refresh();

            this.dataGridVisitorVisit.Rows.Clear();
            this.dataGridVisitorVisit.Refresh();

            List<string> ls = checkin();

            int visitorsCount = 0;
            int totalMin = 0;

            var visited = from date in ls

                          where date.Contains(dailyReportDateTimePicker.Value.ToShortDateString())
                          select date;


            List<string> cards = new List<string>();

            List<MuseumData> perVisit = new List<MuseumData>();
            foreach (string v in visited)
            {
                Console.WriteLine("Uniques: " + v);

                string vals = v.ToString();
                string[] n = vals.Split(',');
                this.dailyVisitorsGridView.Rows.Add(n[0], n[1], n[2], DateTime.Parse(n[3]).ToShortDateString(), n[4], n[5], n[6]);

                MuseumData showVisitor = new MuseumData();
                showVisitor.card_no = n[0];
                showVisitor.name = n[1];
                showVisitor.total_duration = n[6];
                perVisit.Add(showVisitor);
                cards.Add(n[0]);
            }

            var uniq = cards.Distinct();
            foreach (var u in uniq)
            {
                visitorsCount = visitorsCount + 1;

            }

            foreach (string i in ls)
            {

                var values = i.Split(',').ToList();
                var totalVisitor = from date in values
                                   where date.Contains(DateTime.Now.Date.ToShortDateString())
                                   select date;

                // Query execution

                foreach (var name in totalVisitor)
                {

                }

                var totalDurations = from date in values
                                     where date.Contains(dailyReportDateTimePicker.Value.ToShortDateString())
                                     select values[6];

                foreach (var name in totalDurations)
                {
                    Console.WriteLine("Time:  " + name);
                    if (name != null && name != "")
                    {
                        totalMin = Int32.Parse(name) + totalMin;
                    }


                }

                var multipleVisitor = from v in perVisit
                                      group v by v.card_no;

                string card = "";
                string name1 = "";
                int totalM = 0;
                foreach (var visitors in multipleVisitor)
                {
                    card = visitors.Key.ToString();
                    Console.WriteLine("Single Visitor: " + visitors.Key);
                    foreach (var v in visitors)
                    {
                        name1 = v.name;
                        totalM = Convert.ToInt32(v.total_duration) + totalM;
                        Console.WriteLine(v.card_no + "  " + v.total_duration);
                    }

                    this.dataGridVisitorVisit.Rows.Add(card, name1, totalM.ToString());
                    totalM = 0;
                    card = "";
                    name1 = "";

                }
                this.dataGridVisitorVisit.Rows.Add(visitorsCount.ToString(), totalMin.ToString());

            }

            Console.WriteLine("Total Visitor: " + visitorsCount);
            Console.WriteLine("Total Min: " + totalMin);
            totalVisitorsTextBox.Text = visitorsCount.ToString();
            totalTimeSpentTextBox.Text = totalMin.ToString();
        }

        private void daily_btn_Click(object sender, EventArgs e)
        {
            panel_daily.Visible = true;
            weekly_panel.Visible = false;
        }

        private void weekly_btn_Click(object sender, EventArgs e)
        {
            
            panel_daily.Visible = false;
            weekly_panel.Visible = true;
        }
    }
}
