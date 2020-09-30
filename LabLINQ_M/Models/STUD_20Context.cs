using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LabLINQ_M.Models
{
    public partial class STUD_20Context : DbContext
    {
        public STUD_20Context()
        {
        }

        public STUD_20Context(DbContextOptions<STUD_20Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Chairs> Chairs { get; set; }
        public virtual DbSet<Curriculum> Curriculum { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Tutors> Tutors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Users\\Lena\\NET\\DB\\STUD_20.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chairs>(entity =>
            {
                entity.HasKey(e => e.ChairId);

                entity.ToTable("CHAIRS");

                entity.HasComment("Кафедры");

                entity.Property(e => e.ChairId).HasColumnName("CHAIR_ID");

                entity.Property(e => e.ChairHeadId)
                    .HasColumnName("CHAIR_HEAD_ID")
                    .HasComment("Ссылка на заведующего");

                entity.Property(e => e.ChairNumber)
                    .HasColumnName("CHAIR_NUMBER")
                    .HasMaxLength(50)
                    .HasComment("Номер кафедры");

                entity.Property(e => e.DeputyDeanId)
                    .HasColumnName("DEPUTY_DEAN_ID")
                    .HasComment("Ссылка на замдекана");

                entity.HasOne(d => d.ChairHead)
                    .WithMany(p => p.ChairsChairHead)
                    .HasForeignKey(d => d.ChairHeadId)
                    .HasConstraintName("FK_CHAIR_HEAD");

                entity.HasOne(d => d.DeputyDean)
                    .WithMany(p => p.ChairsDeputyDean)
                    .HasForeignKey(d => d.DeputyDeanId)
                    .HasConstraintName("FK_CHAIR_VICE_DEAN");
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.ToTable("CURRICULUM");

                entity.HasComment("Учебный план");

                entity.Property(e => e.CurriculumId).HasColumnName("CURRICULUM_ID");

                entity.Property(e => e.GroupNumber)
                    .HasColumnName("GROUP_NUMBER")
                    .HasMaxLength(6)
                    .HasComment("Номер группы");

                entity.Property(e => e.SubjectId)
                    .HasColumnName("SUBJECT_ID")
                    .HasComment("Ссылка на предмет");

                entity.Property(e => e.TutorId)
                    .HasColumnName("TUTOR_ID")
                    .HasComment("Ссылка на препода");

                entity.HasOne(d => d.GroupNumberNavigation)
                    .WithMany(p => p.Curriculum)
                    .HasPrincipalKey(p => p.GroupNumber)
                    .HasForeignKey(d => d.GroupNumber)
                    .HasConstraintName("FK_CURRICULUM_GROUP");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_CURRICULUM_SUBJECT");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK_CURRICULUM_TUTOR");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("GROUPS");

                entity.HasComment("Группы");

                entity.HasIndex(e => e.GroupNumber)
                    .HasName("U_GROUP_NUMBER")
                    .IsUnique();

                entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

                entity.Property(e => e.ChairId)
                    .HasColumnName("CHAIR_ID")
                    .HasComment("Ссылка на кафедру");

                entity.Property(e => e.CuratorId)
                    .HasColumnName("CURATOR_ID")
                    .HasComment("Ссылка на куратора");

                entity.Property(e => e.GroupNumber)
                    .IsRequired()
                    .HasColumnName("GROUP_NUMBER")
                    .HasMaxLength(6)
                    .HasComment("Номер группы");

                entity.Property(e => e.LabStudies)
                    .HasColumnName("LAB_STUDIES")
                    .HasComment("Количество лаб");

                entity.Property(e => e.PractStudies)
                    .HasColumnName("PRACT_STUDIES")
                    .HasComment("Количество практик");

                entity.Property(e => e.SeniorStudentId)
                    .HasColumnName("SENIOR_STUDENT_ID")
                    .HasComment("Ссылка на старосту");

                entity.Property(e => e.StudyHours)
                    .HasColumnName("STUDY_HOURS")
                    .HasComment("Объём занятий в часах");

                entity.HasOne(d => d.Chair)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.ChairId)
                    .HasConstraintName("FK_GROUPS_CHAIRS");

                entity.HasOne(d => d.Curator)
                    .WithMany(p => p.CuratorOfGroups)
                    .HasForeignKey(d => d.CuratorId)
                    .HasConstraintName("FK_GROUPS_CURATOR");

                entity.HasOne(d => d.SeniorStudent)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.SeniorStudentId)
                    .HasConstraintName("FK_GROUPS_SENIOR_STUDENT");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.ToTable("STUDENTS");

                entity.HasComment("Студенты");

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");

                entity.Property(e => e.Absences)
                    .HasColumnName("ABSENCES")
                    .HasComment("Пропуски занятий в часах");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GROUP_ID")
                    .HasComment("Ссылка на группу");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50)
                    .HasComment("Имя");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("PATRONYMIC")
                    .HasMaxLength(50)
                    .HasComment("Отчество");

                entity.Property(e => e.Surname)
                    .HasColumnName("SURNAME")
                    .HasMaxLength(50)
                    .HasComment("Фамилия");

                entity.Property(e => e.UndoneLabs)
                    .HasColumnName("UNDONE_LABS")
                    .HasComment("Невыполненные лабы");

                entity.Property(e => e.UnreadyLabs)
                    .HasColumnName("UNREADY_LABS")
                    .HasComment("Насданные лабы");

                entity.Property(e => e.UnreasonableAbsences)
                    .HasColumnName("UNREASONABLE_ABSENCES")
                    .HasComment("Пропуски без ув. причины в часах");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_STUDENT_GROUP");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectId);

                entity.ToTable("SUBJECTS");

                entity.HasComment("Предметы");

                entity.Property(e => e.SubjectId).HasColumnName("SUBJECT_ID");

                entity.Property(e => e.ChairExternal)
                    .HasColumnName("CHAIR_EXTERNAL")
                    .HasMaxLength(3)
                    .HasComment("Номер внешней кафедры");

                entity.Property(e => e.ChairId)
                    .HasColumnName("CHAIR_ID")
                    .HasComment("Ссылка на кафедру");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .HasComment("Название");
            });

            modelBuilder.Entity<Tutors>(entity =>
            {
                entity.HasKey(e => e.TutorId);

                entity.ToTable("TUTORS");

                entity.HasComment("Преподаватели");

                entity.Property(e => e.TutorId).HasColumnName("TUTOR_ID");

                entity.Property(e => e.ChairExternal)
                    .HasColumnName("CHAIR_EXTERNAL")
                    .HasMaxLength(3)
                    .HasComment("Номер кафедры (для внешних)");

                entity.Property(e => e.ChairId)
                    .HasColumnName("CHAIR_ID")
                    .HasComment("Ссылка на кафедру (для ф-та № 3)");

                entity.Property(e => e.Faculty)
                    .HasColumnName("FACULTY")
                    .HasComment("Факультет");

                entity.Property(e => e.NameFio)
                    .HasColumnName("NAME_FIO")
                    .HasMaxLength(50)
                    .HasComment("Фамили И.О.");

                entity.Property(e => e.Position)
                    .HasColumnName("POSITION")
                    .HasMaxLength(50)
                    .HasComment("Должность");

                entity.HasOne(d => d.Chair)
                    .WithMany(p => p.Tutors)
                    .HasForeignKey(d => d.ChairId)
                    .HasConstraintName("FK_TUTORS_CHAIR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
