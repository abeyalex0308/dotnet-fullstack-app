using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preqin.Core.Entities
{
    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InvestorType { get; set; }
        public string Country { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual ICollection<Commitment>? Commitments { get; set; }
    }
}
