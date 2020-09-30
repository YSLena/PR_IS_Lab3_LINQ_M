using System;
using System.Collections.Generic;

namespace LabLINQ_M.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Curriculum = new HashSet<Curriculum>();
            Students = new HashSet<Students>();
        }

        public int GroupId { get; set; }
        public string GroupNumber { get; set; }
        public int? ChairId { get; set; }
        public int? CuratorId { get; set; }
        public int? SeniorStudentId { get; set; }
        public int? StudyHours { get; set; }
        public int? LabStudies { get; set; }
        public int? PractStudies { get; set; }

        public virtual Chairs Chair { get; set; }
        public virtual Tutors Curator { get; set; }
        public virtual Students SeniorStudent { get; set; }
        public virtual ICollection<Curriculum> Curriculum { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }
}
