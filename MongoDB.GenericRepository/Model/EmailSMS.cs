using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class EmailSMS
    {
        public string SelectedDomainAccount { get; set; }
        public String PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public String SecondayDomain { get; set; }

        public int SecondaryPort { get; set; }

        public String UsernameEmail { get; set; }

        public String UsernamePassword { get; set; }

        public String FromEmail { get; set; }

        public String ToEmail { get; set; }

        public String CcEmail { get; set; }
        public String BccEmail { get; set; }
        public string Body { get; set; }
    }

    public class SMSTemplate
    {
        public string SelectedDomainAccount { get; set; }

        public String CLIENT_NUMBER { get; set; }
        public String SENDER_ID { get; set; }
        public string API_KEY { get; set; }

        public String templatename { get; set; }
        public string VAR1_VALUE { get; set; }
        public string VAR2_VALUE { get; set; }
        public string VAR3_VALUE { get; set; }
        public string VAR4_VALUE { get; set; }
    }
}
