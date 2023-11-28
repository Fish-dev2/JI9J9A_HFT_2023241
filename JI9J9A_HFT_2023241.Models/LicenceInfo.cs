using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Models
{
    public class LicenceInfo
    {
        public LicenceType LicenceType { get; set; }
        public int Count { get; set; }
        public override bool Equals(object obj)
        {
            LicenceInfo b = obj as LicenceInfo;
            if (b == null)
            {
                return false;
            }
            return b.LicenceType == this.LicenceType && b.Count == this.Count;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.LicenceType, this.Count);
        }
    }
}
