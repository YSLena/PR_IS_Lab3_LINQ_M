using System;
using System.Collections.Generic;

namespace LabLINQ_M.Models
{
    public partial class Chairs
    {
        public Chairs()
        {
            Groups = new HashSet<Groups>();
            Tutors = new HashSet<Tutors>();
        }

        public int ChairId { get; set; }
        public string ChairNumber { get; set; }
        public int? ChairHeadId { get; set; }
        public int? DeputyDeanId { get; set; }

        public virtual Tutors ChairHead { get; set; }
        public virtual Tutors DeputyDean { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<Tutors> Tutors { get; set; }
    }
}
