using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class pa_check_in_outs
    {
        int org_company_id { get; set; }
        int enroll_id {get; set;}
        DateTime check_dt { get; set; }
        int status { get; set; }

        public pa_check_in_outs() { }

        public pa_check_in_outs(int org_company_id, int enroll_id, DateTime check_dt, int status)

        {

            this.org_company_id = org_company_id;
            this.enroll_id = enroll_id;
            this.check_dt = check_dt;
            this.status = status;
            
        }

    }
}
