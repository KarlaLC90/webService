using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class pa_check_in_outs
    {
        public int id { get; set; } //
        public int org_company_id { get; set; }  //1
        public int enroll_id {get; set;} //personid
        public DateTime check_dt { get; set; }  //time fecha
       public int status { get; set; }  //1
        

        public pa_check_in_outs() { }

    }
}
