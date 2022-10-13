using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class CMSFPTUContext : DbContext
    {
        public CMSFPTUContext()
        {
        }

        public CMSFPTUContext(DbContextOptions<CMSFPTUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountSubject> AccountSubjects { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassSubject> ClassSubjects { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SystemStatus> SystemStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("account_code");

                entity.Property(e => e.AccountStatus).HasColumnName("account_status");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EnrollmentYear)
                    .HasColumnType("date")
                    .HasColumnName("enrollment_year");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.HiringDate)
                    .HasColumnType("date")
                    .HasColumnName("hiring_date");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime")
                    .HasColumnName("last_login");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_AccountClass");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__account__role_id__38996AB5");

                entity.HasOne(d => d.SystemStatus)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.SystemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__account__LK_Acco__71D1E811");
            });

            modelBuilder.Entity<AccountSubject>(entity =>
            {
                entity.ToTable("account_subject");

                entity.Property(e => e.AccountSubjectId).HasColumnName("account_subject_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountSubjects)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__account_s__accou__52593CB8");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.AccountSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__account_s__subje__534D60F1");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("class_code");

                entity.HasOne(d => d.SystemStatus)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SystemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__class__systemsta__2739D489");
            });

            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("class_subject");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Class)
                    .WithMany()
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__class_sub__class__4F7CD00D");

                entity.HasOne(d => d.Subject)
                    .WithMany()
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__class_sub__subje__5070F446");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.RequestBy).HasColumnName("request_by");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("date")
                    .HasColumnName("request_date");

                entity.Property(e => e.RequestDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("request_description");

                entity.Property(e => e.RequestName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("request_name");

                entity.Property(e => e.RequestStatus).HasColumnName("request_status");

                entity.Property(e => e.RequestTime)
                    .HasColumnType("datetime")
                    .HasColumnName("request_time");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.SlotId).HasColumnName("slot_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__request__account__49C3F6B7");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__request__class_i__4D94879B");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__request__room_id__4CA06362");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__request__slot_id__4BAC3F29");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__request__subject__4AB81AF0");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("role_code");

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("role_description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.SystemStatus)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.SystemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__room__systemstat__282DF8C2");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__room__type_id__46E78A0C");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__roomType__2C00059882B0B73C");

                entity.ToTable("roomType");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("type_code");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(200)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.ScheduleDate)
                    .HasColumnType("date")
                    .HasColumnName("schedule_date");

                entity.Property(e => e.SlotId).HasColumnName("slot_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__schedule__accoun__5629CD9C");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__schedule__class___571DF1D5");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__schedule__room_i__5812160E");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__schedule__slot_i__59063A47");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("slot");

                entity.Property(e => e.SlotId).HasColumnName("slot_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("time(0)")
                    .HasColumnName("end_time");

                entity.Property(e => e.StartTime)
                    .HasColumnType("time(0)")
                    .HasColumnName("start_time");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.SubjectCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("subject_code");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_name");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.HasOne(d => d.SystemStatus)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.SystemStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__subject__systems__29221CFB");
            });

            modelBuilder.Entity<SystemStatus>(entity =>
            {
                entity.ToTable("SystemStatus");

                entity.Property(e => e.SystemStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
