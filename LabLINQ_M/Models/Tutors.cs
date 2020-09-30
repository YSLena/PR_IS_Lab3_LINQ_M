using System;
using System.Collections.Generic;

namespace LabLINQ_M.Models
{
    public partial class Tutors
    {
        public Tutors()
        {
            ChairsChairHead = new HashSet<Chairs>();
            ChairsDeputyDean = new HashSet<Chairs>();
            Curriculum = new HashSet<Curriculum>();
            CuratorOfGroups = new HashSet<Groups>();
        }

        public int TutorId { get; set; }
        public string NameFio { get; set; }
        public int? Faculty { get; set; }
        public int? ChairId { get; set; }
        public string ChairExternal { get; set; }
        public string Position { get; set; }

        public virtual Chairs Chair { get; set; }
        public virtual ICollection<Chairs> ChairsChairHead { get; set; }
        public virtual ICollection<Chairs> ChairsDeputyDean { get; set; }
        public virtual ICollection<Curriculum> Curriculum { get; set; }
        public virtual ICollection<Groups> CuratorOfGroups { get; set; }
    }
}
