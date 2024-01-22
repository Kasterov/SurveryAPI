using Application.Abstractions.Common;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection.Emit;

namespace Infrastructure.Db;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<User> Users => Set<User>();
    public virtual DbSet<Post> Posts => Set<Post>();
    public virtual DbSet<PoolOption> PoolOptions => Set<PoolOption>();
    public virtual DbSet<Vote> Votes => Set<Vote>();
    public virtual DbSet<Education> Educations => Set<Education>();
    public virtual DbSet<Job> Jobs => Set<Job>();
    public virtual DbSet<Hobby> Hobbies => Set<Hobby>();
    public virtual DbSet<Country> Countries => Set<Country>();
    public virtual DbSet<Profile> Profiles => Set<Profile>();
    public virtual DbSet<ProfileJob> ProfileJobs => Set<ProfileJob>();
    public virtual DbSet<ProfileHobby> ProfileHobbies => Set<ProfileHobby>();
    public virtual DbSet<ProfileEducation> ProfileEducations => Set<ProfileEducation>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePost(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureVote(modelBuilder);
        ConfigurePoolOption(modelBuilder);
        ConfigureProfile(modelBuilder);
        ConfigureCountry(modelBuilder);
        ConfigureHobby(modelBuilder);
        ConfigureEducation(modelBuilder);
        ConfigureJob(modelBuilder);
        ConfigureProfileJob(modelBuilder);
        ConfigureProfileEducation(modelBuilder);
        ConfigureProfileHobby(modelBuilder);

        //FillTables(modelBuilder);
    }

    private void ConfigurePost(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .Property(p => p.AuthorId)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Options)
            .WithOne(o => o.Post)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithMany()
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureVote(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vote>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Vote>()
            .Property(x => x.UserId)
            .IsRequired();

        modelBuilder.Entity<Vote>()
            .Property(x => x.PoolOptionId)
            .IsRequired();

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.User)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Vote>()
            .HasOne(v => v.PoolOption)
            .WithMany(o => o.Votes)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigurePoolOption(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PoolOption>()
                .HasKey(x => x.Id);

        modelBuilder.Entity<PoolOption>()
           .Property(x => x.PostId)
           .IsRequired();

        modelBuilder.Entity<PoolOption>()
            .HasOne(o => o.Post)
            .WithMany(p => p.Options)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureProfile(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Profile>()
            .HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureCountry(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasKey(x => x.Id);
    }

    private void ConfigureHobby(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hobby>()
            .HasKey(x => x.Id);
    }

    private void ConfigureEducation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Education>()
            .HasKey(x => x.Id);
    }

    private void ConfigureJob(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
            .HasKey(x => x.Id);
    }

    private void ConfigureProfileJob(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileJob>()
        .HasKey(u => new { u.ProfileId, u.JobId });

        modelBuilder.Entity<ProfileJob>()
            .HasOne(u => u.Profile)
            .WithMany(up => up.Jobs)
            .HasForeignKey(u => u.ProfileId);

        modelBuilder.Entity<ProfileJob>()
            .HasOne(u => u.Job)
            .WithMany(j => j.ProfileJobs)
            .HasForeignKey(u => u.JobId);
    }

    private void ConfigureProfileEducation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileEducation>()
        .HasKey(u => new { u.ProfileId, u.EducationId });

        modelBuilder.Entity<ProfileEducation>()
            .HasOne(u => u.Profile)
            .WithMany(up => up.Educations)
            .HasForeignKey(u => u.ProfileId);

        modelBuilder.Entity<ProfileEducation>()
            .HasOne(u => u.Education)
            .WithMany(j => j.ProfileEducations)
            .HasForeignKey(u => u.EducationId);
    }

    private void ConfigureProfileHobby(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileHobby>()
        .HasKey(u => new { u.ProfileId, u.HobbyId });

        modelBuilder.Entity<ProfileHobby>()
            .HasOne(u => u.Profile)
            .WithMany(up => up.Hobbies)
            .HasForeignKey(u => u.ProfileId);

        modelBuilder.Entity<ProfileHobby>()
            .HasOne(u => u.Hobby)
            .WithMany(j => j.ProfileHobbies)
            .HasForeignKey(u => u.HobbyId);
    }

    //private void FillTables(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<UserCountry>()
    //        .HasData(
    //            new UserCountry { Id = 1, Name = "Ukraine", CountryCode = "UA" },
    //            new UserCountry { Id = 2, Name = "United States", CountryCode = "US" },
    //            new UserCountry { Id = 3, Name = "Canada", CountryCode = "CA" },
    //            new UserCountry { Id = 4, Name = "United Kingdom", CountryCode = "GB" },
    //            new UserCountry { Id = 5, Name = "Germany", CountryCode = "DE" },
    //            new UserCountry { Id = 6, Name = "France", CountryCode = "FR" },
    //            new UserCountry { Id = 7, Name = "Japan", CountryCode = "JP" },
    //            new UserCountry { Id = 8, Name = "Australia", CountryCode = "AU" },
    //            new UserCountry { Id = 9, Name = "Brazil", CountryCode = "BR" },
    //            new UserCountry { Id = 10, Name = "India", CountryCode = "IN" },
    //            new UserCountry { Id = 11, Name = "Italy", CountryCode = "IT" },
    //            new UserCountry { Id = 12, Name = "South Africa", CountryCode = "ZA" }
    //        );

    //    modelBuilder.Entity<UserHobby>()
    //        .HasData(
    //            new UserHobby { Id = 1, Name = "Photography", Description = "Capturing moments through photography" },
    //            new UserHobby { Id = 2, Name = "Cooking", Description = "Exploring and creating new recipes in the kitchen" },
    //            new UserHobby { Id = 3, Name = "Reading", Description = "Enjoying a variety of books and literature" },
    //            new UserHobby { Id = 4, Name = "Gardening", Description = "Cultivating plants and flowers in the garden" },
    //            new UserHobby { Id = 5, Name = "Cycling", Description = "Exploring scenic routes on a bicycle" },
    //            new UserHobby { Id = 6, Name = "Painting", Description = "Expressing creativity through painting" },
    //            new UserHobby { Id = 7, Name = "Traveling", Description = "Exploring new places and cultures" },
    //            new UserHobby { Id = 8, Name = "Playing Music", Description = "Creating melodies on musical instruments" },
    //            new UserHobby { Id = 9, Name = "Hiking", Description = "Venturing into the great outdoors on hiking trails" },
    //            new UserHobby { Id = 10, Name = "Yoga", Description = "Practicing yoga for physical and mental well-being" },
    //            new UserHobby { Id = 11, Name = "Gaming", Description = "Enjoying video games and board games" },
    //            new UserHobby { Id = 12, Name = "Fishing", Description = "Relaxing by the water with a fishing rod" },
    //            new UserHobby { Id = 13, Name = "Writing", Description = "Expressing thoughts and ideas through writing" },
    //            new UserHobby { Id = 14, Name = "Collecting", Description = "Building collections of various items" },
    //            new UserHobby { Id = 15, Name = "Dancing", Description = "Expressing joy and creativity through dance" },
    //            new UserHobby { Id = 16, Name = "Crafting", Description = "Creating handmade crafts and projects" },
    //            new UserHobby { Id = 17, Name = "Bird Watching", Description = "Observing and identifying different bird species" },
    //            new UserHobby { Id = 18, Name = "Sculpting", Description = "Creating sculptures from various materials" },
    //            new UserHobby { Id = 19, Name = "Playing Sports", Description = "Engaging in various sports activities" },
    //            new UserHobby { Id = 20, Name = "Stargazing", Description = "Observing the night sky and celestial objects" }
    //        );


    //    modelBuilder.Entity<UserJob>()
    //        .HasData(
    //            new UserJob { Id = 1, Name = "Software Developer", Description = "Develops software applications" },
    //            new UserJob { Id = 2, Name = "Data Scientist", Description = "Analyzes and interprets complex data sets" },
    //            new UserJob { Id = 3, Name = "Marketing Manager", Description = "Plans and executes marketing strategies" },
    //            new UserJob { Id = 4, Name = "Teacher", Description = "Educates and guides students" },
    //            new UserJob { Id = 5, Name = "Nurse", Description = "Provides medical care and support" },
    //            new UserJob { Id = 6, Name = "Architect", Description = "Designs and plans buildings and structures" },
    //            new UserJob { Id = 7, Name = "Accountant", Description = "Manages financial records and statements" },
    //            new UserJob { Id = 8, Name = "Graphic Designer", Description = "Creates visual concepts using computer software" },
    //            new UserJob { Id = 9, Name = "Human Resources Specialist", Description = "Handles HR-related tasks and policies" },
    //            new UserJob { Id = 10, Name = "Chef", Description = "Prepares and cooks food in a professional kitchen" },
    //            new UserJob { Id = 11, Name = "Account Manager", Description = "Manages and nurtures client relationships" },
    //            new UserJob { Id = 12, Name = "Civil Engineer", Description = "Plans, designs, and oversees construction projects" },
    //            new UserJob { Id = 13, Name = "Social Media Manager", Description = "Manages social media accounts and campaigns" },
    //            new UserJob { Id = 14, Name = "Mechanical Engineer", Description = "Designs and analyzes mechanical systems" },
    //            new UserJob { Id = 15, Name = "Project Manager", Description = "Oversees and coordinates project activities" },
    //            new UserJob { Id = 16, Name = "UX/UI Designer", Description = "Creates user interfaces and experiences" },
    //            new UserJob { Id = 17, Name = "Dental Hygienist", Description = "Cleans and examines patients' teeth and gums" },
    //            new UserJob { Id = 18, Name = "Research Scientist", Description = "Conducts scientific research and experiments" },
    //            new UserJob { Id = 19, Name = "Sales Representative", Description = "Sells products or services to customers" },
    //            new UserJob { Id = 20, Name = "Pharmacist", Description = "Dispenses medications and provides healthcare advice" },
    //            new UserJob { Id = 21, Name = "Financial Analyst", Description = "Analyzes financial data and trends" },
    //            new UserJob { Id = 22, Name = "Fitness Trainer", Description = "Guides individuals in physical exercise routines" },
    //            new UserJob { Id = 23, Name = "Interior Designer", Description = "Plans and designs interior spaces" },
    //            new UserJob { Id = 24, Name = "Network Administrator", Description = "Manages and maintains computer networks" },
    //            new UserJob { Id = 25, Name = "Legal Consultant", Description = "Provides legal advice and assistance" },
    //            new UserJob { Id = 26, Name = "Environmental Scientist", Description = "Studies the environment and ecosystems" },
    //            new UserJob { Id = 27, Name = "Customer Service Representative", Description = "Assists customers with inquiries and issues" },
    //            new UserJob { Id = 28, Name = "Event Planner", Description = "Plans and organizes events and meetings" },
    //            new UserJob { Id = 29, Name = "Fashion Designer", Description = "Creates clothing and accessory designs" },
    //            new UserJob { Id = 30, Name = "Medical Doctor", Description = "Diagnoses and treats medical conditions" }
    //        );


    //    modelBuilder.Entity<UserEducation>()
    //        .HasData(
    //            new UserEducation { Id = 1, Name = "Computer Science", Description = "Bachelor's degree in Computer Science", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 2, Name = "Electrical Engineering", Description = "Master's degree in Electrical Engineering", EducationType = EducationType.Master },
    //            new UserEducation { Id = 3, Name = "Business Administration", Description = "Bachelor's degree in Business Administration", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 4, Name = "Psychology", Description = "PhD in Psychology", EducationType = EducationType.PhD },
    //            new UserEducation { Id = 5, Name = "Graphic Design", Description = "Courses in Graphic Design", EducationType = EducationType.Courses },
    //            new UserEducation { Id = 6, Name = "Medical Doctor", Description = "Doctor of Medicine", EducationType = EducationType.PhD },
    //            new UserEducation { Id = 7, Name = "Primary Education", Description = "Primary education", EducationType = EducationType.PrimarySchool },
    //            new UserEducation { Id = 8, Name = "Chemical Engineering", Description = "Master's degree in Chemical Engineering", EducationType = EducationType.Master },
    //            new UserEducation { Id = 9, Name = "No Formal Education", Description = "No formal education", EducationType = EducationType.NoEducation },
    //            new UserEducation { Id = 10, Name = "Environmental Science", Description = "Bachelor's degree in Environmental Science", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 11, Name = "Law", Description = "PhD in Law", EducationType = EducationType.PhD },
    //            new UserEducation { Id = 12, Name = "Graphic Design", Description = "Courses in Graphic Design", EducationType = EducationType.Courses },
    //            new UserEducation { Id = 13, Name = "Secondary Education", Description = "Secondary education", EducationType = EducationType.SecondarySchool },
    //            new UserEducation { Id = 14, Name = "Nursing", Description = "Bachelor's degree in Nursing", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 15, Name = "Mathematics", Description = "Master's degree in Mathematics", EducationType = EducationType.Master },
    //            new UserEducation { Id = 16, Name = "Human Resources Management", Description = "Bachelor's degree in Human Resources Management", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 17, Name = "Physics", Description = "PhD in Physics", EducationType = EducationType.PhD },
    //            new UserEducation { Id = 18, Name = "Marketing", Description = "Bachelor's degree in Marketing", EducationType = EducationType.Bachelor },
    //            new UserEducation { Id = 19, Name = "English Literature", Description = "Master's degree in English Literature", EducationType = EducationType.Master },
    //            new UserEducation { Id = 20, Name = "Mechanical Engineering", Description = "Bachelor's degree in Mechanical Engineering", EducationType = EducationType.Bachelor }
    //        );

    //}

}