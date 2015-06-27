namespace GoFit.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class masterEntities : DbContext
    {
        public masterEntities()
            : base("name=masterEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<exercise> exercises { get; set; }
        public virtual DbSet<type> types { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<user_favorite_exercise> user_favorite_exercise { get; set; }
        public virtual DbSet<user_favorite_workout> user_favorite_workout { get; set; }
        public virtual DbSet<user_workout> user_workout { get; set; }
        public virtual DbSet<workout> workouts { get; set; }
        public virtual DbSet<workout_exercise> workout_exercise { get; set; }
    }
}