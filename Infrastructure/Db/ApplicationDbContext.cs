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
    public virtual DbSet<Media> Files => Set<Media>();
    public virtual DbSet<Education> Educations => Set<Education>();
    public virtual DbSet<Job> Jobs => Set<Job>();
    public virtual DbSet<Hobby> Hobbies => Set<Hobby>();
    public virtual DbSet<Country> Countries => Set<Country>();
    public virtual DbSet<UserJob> ProfileJobs => Set<UserJob>();
    public virtual DbSet<UserHobby> ProfileHobbies => Set<UserHobby>();
    public virtual DbSet<UserEducation> ProfileEducations => Set<UserEducation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePost(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureVote(modelBuilder);
        ConfigurePoolOption(modelBuilder);
        ConfigureCountry(modelBuilder);
        ConfigureHobby(modelBuilder);
        ConfigureEducation(modelBuilder);
        ConfigureJob(modelBuilder);
        ConfigureProfileJob(modelBuilder);
        ConfigureProfileEducation(modelBuilder);
        ConfigureProfileHobby(modelBuilder);

        FillProfileTables(modelBuilder);
        FillDbMock(modelBuilder);
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

        modelBuilder.Entity<Media>()
            .HasOne(u => u.User)
            .WithOne(u => u.Avatar)
            .HasForeignKey<User>(u => u.AvatarId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
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

    private void ConfigureCountry(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Country>()
            .HasMany(p => p.Users)
            .WithOne(u => u.Country)
            .HasForeignKey(p => p.CountryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
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
        modelBuilder.Entity<UserJob>()
        .HasKey(u => new { u.UserId, u.JobId });

        modelBuilder.Entity<UserJob>()
            .HasOne(u => u.User)
            .WithMany(up => up.Jobs)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<UserJob>()
            .HasOne(u => u.Job)
            .WithMany(j => j.ProfileJobs)
            .HasForeignKey(u => u.JobId);
    }

    private void ConfigureProfileEducation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEducation>()
        .HasKey(u => new { u.UserId, u.EducationId });

        modelBuilder.Entity<UserEducation>()
            .HasOne(u => u.User)
            .WithMany(up => up.Educations)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<UserEducation>()
            .HasOne(u => u.Education)
            .WithMany(j => j.ProfileEducations)
            .HasForeignKey(u => u.EducationId);
    }

    private void ConfigureProfileHobby(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserHobby>()
        .HasKey(u => new { u.UserId, u.HobbyId });

        modelBuilder.Entity<UserHobby>()
            .HasOne(u => u.User)
            .WithMany(up => up.Hobbies)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<UserHobby>()
            .HasOne(u => u.Hobby)
            .WithMany(j => j.ProfileHobbies)
            .HasForeignKey(u => u.HobbyId);
    }

    private void FillProfileTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasData(
                new Country { Id = 1, Name = "Ukraine", CountryCode = "UA" },
                new Country { Id = 2, Name = "United States", CountryCode = "US" },
                new Country { Id = 3, Name = "Canada", CountryCode = "CA" },
                new Country { Id = 4, Name = "United Kingdom", CountryCode = "GB" },
                new Country { Id = 5, Name = "Germany", CountryCode = "DE" },
                new Country { Id = 6, Name = "France", CountryCode = "FR" },
                new Country { Id = 7, Name = "Japan", CountryCode = "JP" },
                new Country { Id = 8, Name = "Australia", CountryCode = "AU" },
                new Country { Id = 9, Name = "Brazil", CountryCode = "BR" },
                new Country { Id = 10, Name = "India", CountryCode = "IN" },
                new Country { Id = 11, Name = "Italy", CountryCode = "IT" },
                new Country { Id = 12, Name = "South Africa", CountryCode = "ZA" }
            );

        modelBuilder.Entity<Hobby>()
            .HasData(
                new Hobby { Id = 1, Name = "Photography", Description = "Capturing moments through photography" },
                new Hobby { Id = 2, Name = "Cooking", Description = "Exploring and creating new recipes in the kitchen" },
                new Hobby { Id = 3, Name = "Reading", Description = "Enjoying a variety of books and literature" },
                new Hobby { Id = 4, Name = "Gardening", Description = "Cultivating plants and flowers in the garden" },
                new Hobby { Id = 5, Name = "Cycling", Description = "Exploring scenic routes on a bicycle" },
                new Hobby { Id = 6, Name = "Painting", Description = "Expressing creativity through painting" },
                new Hobby { Id = 7, Name = "Traveling", Description = "Exploring new places and cultures" },
                new Hobby { Id = 8, Name = "Playing Music", Description = "Creating melodies on musical instruments" },
                new Hobby { Id = 9, Name = "Hiking", Description = "Venturing into the great outdoors on hiking trails" },
                new Hobby { Id = 10, Name = "Yoga", Description = "Practicing yoga for physical and mental well-being" },
                new Hobby { Id = 11, Name = "Gaming", Description = "Enjoying video games and board games" },
                new Hobby { Id = 12, Name = "Fishing", Description = "Relaxing by the water with a fishing rod" },
                new Hobby { Id = 13, Name = "Writing", Description = "Expressing thoughts and ideas through writing" },
                new Hobby { Id = 14, Name = "Collecting", Description = "Building collections of various items" },
                new Hobby { Id = 15, Name = "Dancing", Description = "Expressing joy and creativity through dance" },
                new Hobby { Id = 16, Name = "Crafting", Description = "Creating handmade crafts and projects" },
                new Hobby { Id = 17, Name = "Bird Watching", Description = "Observing and identifying different bird species" },
                new Hobby { Id = 18, Name = "Sculpting", Description = "Creating sculptures from various materials" },
                new Hobby { Id = 19, Name = "Playing Sports", Description = "Engaging in various sports activities" },
                new Hobby { Id = 20, Name = "Stargazing", Description = "Observing the night sky and celestial objects" }
            );


        modelBuilder.Entity<Job>()
            .HasData(
                new Job { Id = 1, Name = "Software Developer", Description = "Develops software applications" },
                new Job { Id = 2, Name = "Data Scientist", Description = "Analyzes and interprets complex data sets" },
                new Job { Id = 3, Name = "Marketing Manager", Description = "Plans and executes marketing strategies" },
                new Job { Id = 4, Name = "Teacher", Description = "Educates and guides students" },
                new Job { Id = 5, Name = "Nurse", Description = "Provides medical care and support" },
                new Job { Id = 6, Name = "Architect", Description = "Designs and plans buildings and structures" },
                new Job { Id = 7, Name = "Accountant", Description = "Manages financial records and statements" },
                new Job { Id = 8, Name = "Graphic Designer", Description = "Creates visual concepts using computer software" },
                new Job { Id = 9, Name = "Human Resources Specialist", Description = "Handles HR-related tasks and policies" },
                new Job { Id = 10, Name = "Chef", Description = "Prepares and cooks food in a professional kitchen" },
                new Job { Id = 11, Name = "Account Manager", Description = "Manages and nurtures client relationships" },
                new Job { Id = 12, Name = "Civil Engineer", Description = "Plans, designs, and oversees construction projects" },
                new Job { Id = 13, Name = "Social Media Manager", Description = "Manages social media accounts and campaigns" },
                new Job { Id = 14, Name = "Mechanical Engineer", Description = "Designs and analyzes mechanical systems" },
                new Job { Id = 15, Name = "Project Manager", Description = "Oversees and coordinates project activities" },
                new Job { Id = 16, Name = "UX/UI Designer", Description = "Creates user interfaces and experiences" },
                new Job { Id = 17, Name = "Dental Hygienist", Description = "Cleans and examines patients' teeth and gums" },
                new Job { Id = 18, Name = "Research Scientist", Description = "Conducts scientific research and experiments" },
                new Job { Id = 19, Name = "Sales Representative", Description = "Sells products or services to customers" },
                new Job { Id = 20, Name = "Pharmacist", Description = "Dispenses medications and provides healthcare advice" },
                new Job { Id = 21, Name = "Financial Analyst", Description = "Analyzes financial data and trends" },
                new Job { Id = 22, Name = "Fitness Trainer", Description = "Guides individuals in physical exercise routines" },
                new Job { Id = 23, Name = "Interior Designer", Description = "Plans and designs interior spaces" },
                new Job { Id = 24, Name = "Network Administrator", Description = "Manages and maintains computer networks" },
                new Job { Id = 25, Name = "Legal Consultant", Description = "Provides legal advice and assistance" },
                new Job { Id = 26, Name = "Environmental Scientist", Description = "Studies the environment and ecosystems" },
                new Job { Id = 27, Name = "Customer Service Representative", Description = "Assists customers with inquiries and issues" },
                new Job { Id = 28, Name = "Event Planner", Description = "Plans and organizes events and meetings" },
                new Job { Id = 29, Name = "Fashion Designer", Description = "Creates clothing and accessory designs" },
                new Job { Id = 30, Name = "Medical Doctor", Description = "Diagnoses and treats medical conditions" }
            );


        modelBuilder.Entity<Education>()
            .HasData(
                new Education { Id = 1, Name = "Computer Science", Description = "Bachelor's degree in Computer Science", EducationType = EducationType.Bachelor },
                new Education { Id = 2, Name = "Electrical Engineering", Description = "Master's degree in Electrical Engineering", EducationType = EducationType.Master },
                new Education { Id = 3, Name = "Business Administration", Description = "Bachelor's degree in Business Administration", EducationType = EducationType.Bachelor },
                new Education { Id = 4, Name = "Psychology", Description = "PhD in Psychology", EducationType = EducationType.PhD },
                new Education { Id = 5, Name = "Graphic Design", Description = "Courses in Graphic Design", EducationType = EducationType.Courses },
                new Education { Id = 6, Name = "Medical Doctor", Description = "Doctor of Medicine", EducationType = EducationType.PhD },
                new Education { Id = 7, Name = "Primary Education", Description = "Primary education", EducationType = EducationType.PrimarySchool },
                new Education { Id = 8, Name = "Chemical Engineering", Description = "Master's degree in Chemical Engineering", EducationType = EducationType.Master },
                new Education { Id = 9, Name = "No Formal Education", Description = "No formal education", EducationType = EducationType.NoEducation },
                new Education { Id = 10, Name = "Environmental Science", Description = "Bachelor's degree in Environmental Science", EducationType = EducationType.Bachelor },
                new Education { Id = 11, Name = "Law", Description = "PhD in Law", EducationType = EducationType.PhD },
                new Education { Id = 12, Name = "Graphic Design", Description = "Courses in Graphic Design", EducationType = EducationType.Courses },
                new Education { Id = 13, Name = "Secondary Education", Description = "Secondary education", EducationType = EducationType.SecondarySchool },
                new Education { Id = 14, Name = "Nursing", Description = "Bachelor's degree in Nursing", EducationType = EducationType.Bachelor },
                new Education { Id = 15, Name = "Mathematics", Description = "Master's degree in Mathematics", EducationType = EducationType.Master },
                new Education { Id = 16, Name = "Human Resources Management", Description = "Bachelor's degree in Human Resources Management", EducationType = EducationType.Bachelor },
                new Education { Id = 17, Name = "Physics", Description = "PhD in Physics", EducationType = EducationType.PhD },
                new Education { Id = 18, Name = "Marketing", Description = "Bachelor's degree in Marketing", EducationType = EducationType.Bachelor },
                new Education { Id = 19, Name = "English Literature", Description = "Master's degree in English Literature", EducationType = EducationType.Master },
                new Education { Id = 20, Name = "Mechanical Engineering", Description = "Bachelor's degree in Mechanical Engineering", EducationType = EducationType.Bachelor }
            );

    }

    private void FillDbMock(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(
                new User { Id = 1, Email="kast@gmail.com", Name = "Slava", PasswordHash = new byte[0],  PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 2, Email = "kast@gmail.com", Name = "Sasha", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 3, Email = "kast@gmail.com", Name = "Andrey", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 4, Email = "kast@gmail.com", Name = "Igor", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 5, Email = "kast@gmail.com", Name = "Masha", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 6, Email = "kast@gmail.com", Name = "Ivan", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 7, Email = "kast@gmail.com", Name = "Vlad", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 8, Email = "kast@gmail.com", Name = "Anton", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 9, Email = "kast@gmail.com", Name = "Vova", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 10, Email = "kast@gmail.com", Name = "Maks", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 11, Email = "kast@gmail.com", Name = "Tolya", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 12, Email = "kast@gmail.com", Name = "Eldar", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 13, Email = "kast@gmail.com", Name = "Nastya", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 14, Email = "kast@gmail.com", Name = "Gosha", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 15, Email = "kast@gmail.com", Name = "Vera", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 16, Email = "kast@gmail.com", Name = "Nicka", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 17, Email = "kast@gmail.com", Name = "Sofia", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 18, Email = "kast@gmail.com", Name = "Dasha", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 19, Email = "kast@gmail.com", Name = "Vlada", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 20, Email = "kast@gmail.com", Name = "Egor", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now },
                new User { Id = 21, Email = "kast@gmail.com", Name = "Dubina", PasswordHash = new byte[0], PasswordSalt = new byte[0], Created = DateTimeOffset.Now, DateOfBirth = DateTime.Now }
            );

        modelBuilder.Entity<Post>()
            .HasData(
                new Post { Id = 1, Title = "Як часто ви відвідуєте кінотеатри або дивитесь фільми вдома?", AuthorId = 1, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now},
                new Post { Id = 2, Title = "Яка ваша улюблена кухня?", AuthorId = 2, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 3, Title = "Які країни ви мрієте відвідати?", AuthorId = 2, IsMultiple = true, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 4, Title = "Скільки разів на день ви їсте?", AuthorId = 3, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 5, Title = "Які з цих ігор ви вважаєте найлегендарнішими?", AuthorId = 4, IsMultiple = true, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 6, Title = "Чи вважаєте ви, що штучний інтелект захопить світ?", AuthorId = 5, IsMultiple = false, IsPrivate = false, IsRevotable = false, Created = DateTimeOffset.Now },
                new Post { Id = 7, Title = "Скільки тривали (тривають) ващі найдовші відносини?", AuthorId = 6, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 8, Title = "Яка консоль вам подобається більше з перелічених?", AuthorId = 4, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 9, Title = "Скільки зірок на прапорі США?", AuthorId = 21, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 10, Title = "На яку оцінку ви вчились в школі (середня)?", AuthorId = 20, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 11, Title = "Що краще Дота чи КС2?", AuthorId = 19, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 12, Title = "Як часто ви гуляєте на вулиці?", AuthorId = 18, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 13, Title = "Вам подобається жити в великому чи малому місті?", AuthorId = 17, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 14, Title = "Чи маєете ви вищу освіту?", AuthorId = 16, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 15, Title = "Чи полюбляли ви ходити в дитячий садок?", AuthorId = 16, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 16, Title = "Як часто ви відвідуєте Мак Доналдс?", AuthorId = 14, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 17, Title = "Скільки ви витрачаєте грошей в місяць?", AuthorId = 13, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 18, Title = "Чи живете ви з батьками, чи самі?", AuthorId = 12, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 19, Title = "В якому році почалася Друга світова?", AuthorId = 16, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 20, Title = "Яка з цих страв ваша найулюбленіша?", AuthorId = 10, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 21, Title = "Яка була НАЙБІЛЬША кількість людей на вечірці, яку ви відвідували?", AuthorId = 1, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 22, Title = "Оберіть марки машин, які вам подобаються найбільше", AuthorId = 11, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 23, Title = "Яким супер героєм ви би ніколи не хотіли бути?", AuthorId = 7, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 24, Title = "Що було перше курка чи яйце?", AuthorId = 8, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 25, Title = "Чи подобалась вам хімія у школі?", AuthorId = 12, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 26, Title = "Де вам більше подобалось у школі чи в університеті?", AuthorId = 3, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now },
                new Post { Id = 27, Title = "Як би вам дали вибір звільнитись з поточоної роботи, чи залишитись на ній назавжди, щоб ви обрали?", Description = "Важливо відмітити, що на поточній роботі, кар'єрний ріст все ще можливий", AuthorId = 1, IsMultiple = false, IsPrivate = false, IsRevotable = true, Created = DateTimeOffset.Now }
             );

        modelBuilder.Entity<PoolOption>()
            .HasData(
                new PoolOption { Id = 1, PostId = 1, Title = "Вдома", Created = DateTimeOffset.Now},
                new PoolOption { Id = 2, PostId = 1, Title = "У кіно", Created = DateTimeOffset.Now },
                new PoolOption { Id = 3, PostId = 2, Title = "Європейська", Created = DateTimeOffset.Now },
                new PoolOption { Id = 4, PostId = 2, Title = "Американська", Created = DateTimeOffset.Now },
                new PoolOption { Id = 5, PostId = 2, Title = "Латиноамериканська", Created = DateTimeOffset.Now },
                new PoolOption { Id = 6, PostId = 2, Title = "Азійська", Created = DateTimeOffset.Now },
                new PoolOption { Id = 7, PostId = 3, Title = "Франція", Created = DateTimeOffset.Now },
                new PoolOption { Id = 8, PostId = 3, Title = "Німеччина", Created = DateTimeOffset.Now },
                new PoolOption { Id = 9, PostId = 3, Title = "Країни Балтії", Created = DateTimeOffset.Now },
                new PoolOption { Id = 10, PostId = 3, Title = "Англію", Created = DateTimeOffset.Now },
                new PoolOption { Id = 11, PostId = 3, Title = "Америку", Created = DateTimeOffset.Now },
                new PoolOption { Id = 12, PostId = 3, Title = "Іспанію", Created = DateTimeOffset.Now },
                new PoolOption { Id = 13, PostId = 3, Title = "Австралію", Created = DateTimeOffset.Now },
                new PoolOption { Id = 14, PostId = 4, Title = "1 раз", Created = DateTimeOffset.Now },
                new PoolOption { Id = 15, PostId = 4, Title = "2 рази", Created = DateTimeOffset.Now },
                new PoolOption { Id = 16, PostId = 4, Title = "3 рази", Created = DateTimeOffset.Now },
                new PoolOption { Id = 17, PostId = 4, Title = "більше 3", Created = DateTimeOffset.Now },
                new PoolOption { Id = 18, PostId = 5, Title = "Minecraft", Created = DateTimeOffset.Now },
                new PoolOption { Id = 19, PostId = 5, Title = "Last of Us", Created = DateTimeOffset.Now },
                new PoolOption { Id = 20, PostId = 5, Title = "Half-Life", Created = DateTimeOffset.Now },
                new PoolOption { Id = 21, PostId = 5, Title = "Halo", Created = DateTimeOffset.Now },
                new PoolOption { Id = 22, PostId = 5, Title = "God of war", Created = DateTimeOffset.Now },
                new PoolOption { Id = 23, PostId = 5, Title = "Doom eternal", Created = DateTimeOffset.Now },
                new PoolOption { Id = 24, PostId = 5, Title = "Roblox", Created = DateTimeOffset.Now },
                new PoolOption { Id = 25, PostId = 6, Title = "Ні, не захопить", Created = DateTimeOffset.Now },
                new PoolOption { Id = 26, PostId = 6, Title = "Може захопити в майбутньому", Created = DateTimeOffset.Now },
                new PoolOption { Id = 27, PostId = 6, Title = "Захопить і це точно", Created = DateTimeOffset.Now },
                new PoolOption { Id = 28, PostId = 7, Title = "Тиждень", Created = DateTimeOffset.Now },
                new PoolOption { Id = 29, PostId = 7, Title = "Місяць", Created = DateTimeOffset.Now },
                new PoolOption { Id = 30, PostId = 7, Title = "2 місяці", Created = DateTimeOffset.Now },
                new PoolOption { Id = 31, PostId = 7, Title = "До 6 місяців", Created = DateTimeOffset.Now },
                new PoolOption { Id = 32, PostId = 7, Title = "До 1 року", Created = DateTimeOffset.Now },
                new PoolOption { Id = 33, PostId = 7, Title = "До 3 років", Created = DateTimeOffset.Now },
                new PoolOption { Id = 34, PostId = 7, Title = "До 5 років", Created = DateTimeOffset.Now },
                new PoolOption { Id = 35, PostId = 7, Title = "Більше ніж 5 років", Created = DateTimeOffset.Now },
                new PoolOption { Id = 36, PostId = 8, Title = "Xbox series s", Created = DateTimeOffset.Now },
                new PoolOption { Id = 37, PostId = 8, Title = "Xbox series x", Created = DateTimeOffset.Now },
                new PoolOption { Id = 38, PostId = 8, Title = "PlayStation 5", Created = DateTimeOffset.Now },
                new PoolOption { Id = 39, PostId = 8, Title = "PlayStation 4", Created = DateTimeOffset.Now },
                new PoolOption { Id = 40, PostId = 9, Title = "10 зірок", Created = DateTimeOffset.Now },
                new PoolOption { Id = 41, PostId = 9, Title = "40 зірок", Created = DateTimeOffset.Now },
                new PoolOption { Id = 42, PostId = 9, Title = "62 зірки", Created = DateTimeOffset.Now },
                new PoolOption { Id = 43, PostId = 9, Title = "50 зірок", Created = DateTimeOffset.Now },
                new PoolOption { Id = 44, PostId = 10, Title = "2", Created = DateTimeOffset.Now },
                new PoolOption { Id = 45, PostId = 10, Title = "3", Created = DateTimeOffset.Now },
                new PoolOption { Id = 46, PostId = 10, Title = "4", Created = DateTimeOffset.Now },
                new PoolOption { Id = 47, PostId = 10, Title = "5", Created = DateTimeOffset.Now },
                new PoolOption { Id = 48, PostId = 10, Title = "6", Created = DateTimeOffset.Now },
                new PoolOption { Id = 49, PostId = 10, Title = "7", Created = DateTimeOffset.Now },
                new PoolOption { Id = 50, PostId = 10, Title = "8", Created = DateTimeOffset.Now },
                new PoolOption { Id = 51, PostId = 10, Title = "9", Created = DateTimeOffset.Now },
                new PoolOption { Id = 52, PostId = 10, Title = "10", Created = DateTimeOffset.Now },
                new PoolOption { Id = 53, PostId = 10, Title = "11", Created = DateTimeOffset.Now },
                new PoolOption { Id = 54, PostId = 10, Title = "12", Created = DateTimeOffset.Now },
                new PoolOption { Id = 55, PostId = 11, Title = "Дота краще", Created = DateTimeOffset.Now },
                new PoolOption { Id = 56, PostId = 11, Title = "КС2 краще", Created = DateTimeOffset.Now },
                new PoolOption { Id = 57, PostId = 12, Title = "Раз на тиждень", Created = DateTimeOffset.Now },
                new PoolOption { Id = 58, PostId = 12, Title = "Декілька разів на тиждень", Created = DateTimeOffset.Now },
                new PoolOption { Id = 59, PostId = 12, Title = "Постійно гуляю", Created = DateTimeOffset.Now },
                new PoolOption { Id = 60, PostId = 12, Title = "Майже не знаходжусь вдома", Created = DateTimeOffset.Now },
                new PoolOption { Id = 61, PostId = 13, Title = "Мале місто", Created = DateTimeOffset.Now },
                new PoolOption { Id = 62, PostId = 13, Title = "Велике місто", Created = DateTimeOffset.Now },
                new PoolOption { Id = 63, PostId = 14, Title = "Ні, не має", Created = DateTimeOffset.Now },
                new PoolOption { Id = 64, PostId = 14, Title = "Так, маю", Created = DateTimeOffset.Now },
                new PoolOption { Id = 65, PostId = 15, Title = "Так", Created = DateTimeOffset.Now },
                new PoolOption { Id = 66, PostId = 15, Title = "Ні", Created = DateTimeOffset.Now },
                new PoolOption { Id = 67, PostId = 16, Title = "не відвідую", Created = DateTimeOffset.Now },
                new PoolOption { Id = 68, PostId = 16, Title = "раз на місяць", Created = DateTimeOffset.Now },
                new PoolOption { Id = 69, PostId = 16, Title = "ходжу туди раз на тиждень", Created = DateTimeOffset.Now },
                new PoolOption { Id = 70, PostId = 17, Title = "До 200 долларів", Created = DateTimeOffset.Now },
                new PoolOption { Id = 71, PostId = 17, Title = "До 500 долларів", Created = DateTimeOffset.Now },
                new PoolOption { Id = 72, PostId = 17, Title = "До 1000 долларів", Created = DateTimeOffset.Now },
                new PoolOption { Id = 73, PostId = 17, Title = "До 2000 долларів", Created = DateTimeOffset.Now },
                new PoolOption { Id = 74, PostId = 17, Title = "більше", Created = DateTimeOffset.Now },
                new PoolOption { Id = 75, PostId = 18, Title = "Сам", Created = DateTimeOffset.Now },
                new PoolOption { Id = 76, PostId = 18, Title = "З батьками", Created = DateTimeOffset.Now },
                new PoolOption { Id = 77, PostId = 19, Title = "В 1941", Created = DateTimeOffset.Now },
                new PoolOption { Id = 78, PostId = 19, Title = "В 1939", Created = DateTimeOffset.Now },
                new PoolOption { Id = 79, PostId = 19, Title = "В 1932", Created = DateTimeOffset.Now },
                new PoolOption { Id = 80, PostId = 19, Title = "Волинська різня...", Created = DateTimeOffset.Now },
                new PoolOption { Id = 81, PostId = 20, Title = "Борщ", Created = DateTimeOffset.Now },
                new PoolOption { Id = 82, PostId = 20, Title = "Пельмені", Created = DateTimeOffset.Now },
                new PoolOption { Id = 83, PostId = 20, Title = "Хачапурі", Created = DateTimeOffset.Now },
                new PoolOption { Id = 84, PostId = 20, Title = "Піцца", Created = DateTimeOffset.Now },
                new PoolOption { Id = 85, PostId = 20, Title = "Бургери", Created = DateTimeOffset.Now },
                new PoolOption { Id = 86, PostId = 21, Title = "до 5", Created = DateTimeOffset.Now },
                new PoolOption { Id = 87, PostId = 21, Title = "до 10", Created = DateTimeOffset.Now },
                new PoolOption { Id = 88, PostId = 21, Title = "до 15", Created = DateTimeOffset.Now },
                new PoolOption { Id = 89, PostId = 21, Title = "до 20", Created = DateTimeOffset.Now },
                new PoolOption { Id = 90, PostId = 21, Title = "до 25", Created = DateTimeOffset.Now },
                new PoolOption { Id = 91, PostId = 21, Title = "більше", Created = DateTimeOffset.Now },
                new PoolOption { Id = 92, PostId = 22, Title = "Toyota", Created = DateTimeOffset.Now },
                new PoolOption { Id = 93, PostId = 22, Title = "Ford", Created = DateTimeOffset.Now },
                new PoolOption { Id = 94, PostId = 22, Title = "Hunda", Created = DateTimeOffset.Now },
                new PoolOption { Id = 95, PostId = 22, Title = "Audi", Created = DateTimeOffset.Now },
                new PoolOption { Id = 96, PostId = 22, Title = "Reno", Created = DateTimeOffset.Now },
                new PoolOption { Id = 97, PostId = 22, Title = "Fiat", Created = DateTimeOffset.Now },
                new PoolOption { Id = 98, PostId = 22, Title = "Volkswagen", Created = DateTimeOffset.Now },
                new PoolOption { Id = 99, PostId = 23, Title = "Супер мен", Created = DateTimeOffset.Now },
                new PoolOption { Id = 100, PostId = 23, Title = "Залізна людина", Created = DateTimeOffset.Now },
                new PoolOption { Id = 101, PostId = 23, Title = "Халк", Created = DateTimeOffset.Now },
                new PoolOption { Id = 102, PostId = 23, Title = "Капітан Америка", Created = DateTimeOffset.Now },
                new PoolOption { Id = 103, PostId = 23, Title = "Людина Павук", Created = DateTimeOffset.Now },
                new PoolOption { Id = 104, PostId = 23, Title = "Тор", Created = DateTimeOffset.Now },
                new PoolOption { Id = 105, PostId = 24, Title = "Курка", Created = DateTimeOffset.Now },
                new PoolOption { Id = 106, PostId = 24, Title = "Яйце", Created = DateTimeOffset.Now },
                new PoolOption { Id = 107, PostId = 25, Title = "Ні", Created = DateTimeOffset.Now },
                new PoolOption { Id = 108, PostId = 25, Title = "Так", Created = DateTimeOffset.Now },
                new PoolOption { Id = 109, PostId = 26, Title = "Університет", Created = DateTimeOffset.Now },
                new PoolOption { Id = 110, PostId = 26, Title = "Школа", Created = DateTimeOffset.Now },
                new PoolOption { Id = 111, PostId = 27, Title = "Так", Created = DateTimeOffset.Now },
                new PoolOption { Id = 112, PostId = 27, Title = "Ні", Created = DateTimeOffset.Now }
            );

        Random random = new Random();
        List<Vote> votes = new();
        int id = 0;

        for ( int i = 1; i < 112; i++)
        {
            for (int j = 1; j < 22; j++)
            {
                if (random.Next(1, 10) >= 5)
                {
                    id++;
                    votes.Add(new Vote { Id = id, UserId = j, PoolOptionId = i, Created = DateTimeOffset.Now });
                }
            }
        }

        modelBuilder.Entity<Vote>()
            .HasData(votes);
    }
}