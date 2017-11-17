namespace FileAnalysis
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbFileAnalysis : DbContext
    {
        public DbFileAnalysis()
            : base("name=DbFileAnalysis")
        {
        }

        public virtual DbSet<DetailsOfFile> DetailsOfFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.EmployeeId)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.ExtProject)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.Esnumber)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.ExternalProject)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.Project)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.Wbs)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.Attribute)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.AAtype)
                .IsUnicode(false);

            modelBuilder.Entity<DetailsOfFile>()
                .Property(e => e.ProjectType)
                .IsUnicode(false);
        }
    }
}
