using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagement
{
    class MuseumData
    {
        public MuseumData()
        {
        }

        public MuseumData(string card_no, string day, string name, string in_time, string out_time, string total_duration)
        {
            this.card_no = card_no;
            this.day = day;
            this.name = name;
            this.in_time = in_time;
            this.out_time = out_time;
            this.total_duration = total_duration;
        }

        public string card_no { get; set; }

        public string day { get; set; }

        public string name { get; set; }

        public string in_time { get; set; }

        public string out_time { get; set; }

        public string total_duration { get; set; }
    }
    class VisitorsData
    {
        public VisitorsData()
        {
        }

        public VisitorsData(string card_no, string name, string email, string contact, string gender, string address, string occupation)
        {
            this.card_no = card_no;
            this.name = name;
            this.email = email;
            this.contact = contact;
            this.gender = gender;
            this.address = address;
            this.occupation = occupation;
        }

        public string card_no { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string contact { get; set; }

        public string gender { get; set; }

        public string address { get; set; }

        public string occupation { get; set; }
    }
}
