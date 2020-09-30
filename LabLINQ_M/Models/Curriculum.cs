using System;
using System.Collections.Generic;

namespace LabLINQ_M.Models
{
    public partial class Curriculum
    {
        public int CurriculumId { get; set; }
        public string GroupNumber { get; set; }
        public int? SubjectId { get; set; }
        public int? TutorId { get; set; }

        public virtual Groups GroupNumberNavigation { get; set; }
        public virtual Subjects Subject { get; set; }
        public virtual Tutors Tutor { get; set; }
    }
}
