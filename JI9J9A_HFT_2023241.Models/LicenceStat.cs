using JI9J9A_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JI9J9A_HFT_2023241.Models
{
    public class LicenceStat
    {
        public string Firearm { get; set; }
        public IEnumerable<LicenceCount> licenceCounts { get; set; }
        public class LicenceCount
        {
            public int Count { get; set; }
            public LicenceType Type { get; set; }
            public override bool Equals(object obj)
            {
                LicenceCount lc = obj as LicenceCount;
                if (lc == null)
                {
                    return false;
                }
                return this.Count == lc.Count && this.Type == lc.Type;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Count, Type);
            }
        }

        public override bool Equals(object obj)
        {
            LicenceStat ls = obj as LicenceStat;
            if (ls == null)
            {
                return false;
            }
            return ls.Firearm == this.Firearm && ls.licenceCounts.SequenceEqual(this.licenceCounts);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Firearm, licenceCounts);
        }


    }
}
