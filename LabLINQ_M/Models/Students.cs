using System;
using System.Collections.Generic;

namespace LabLINQ_M.Models
{
    public partial class Students
    {
        public Students()
        {
            Groups = new HashSet<Groups>();
        }

        public int StudentId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int? GroupId { get; set; }
        public int? Absences { get; set; }
        public int? UnreasonableAbsences { get; set; }
        public int? UndoneLabs { get; set; }
        public int? UnreadyLabs { get; set; }

        public virtual Groups Group { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}
