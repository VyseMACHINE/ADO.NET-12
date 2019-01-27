using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompanyApp
{
    class Request
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime Date  { get; set; }
        public string ServiceName { get; set; }
        public bool Status{ get; set; }
    }
}
