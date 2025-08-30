using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Smis.Models;

public partial class SmisContext : IdentityDbContext<Useri>
{
    public SmisContext()
    {
    }

    public SmisContext(DbContextOptions<SmisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admin { get; set; }

    public virtual DbSet<Departamenti> Departamenti { get; set; }

    public virtual DbSet<Grupi> Grupi { get; set; }

    public virtual DbSet<Lenda> Lenda { get; set; }

    public virtual DbSet<Ligjerata> Ligjerata { get; set; }

    public virtual DbSet<Nota> Nota { get; set; }

    public virtual DbSet<Orari> Orari { get; set; }

    public virtual DbSet<Pagesa> Pagesa { get; set; }

    public virtual DbSet<ParaqitjaEprovimit> ParaqitjaEprovimit { get; set; }

    public virtual DbSet<Provimi> Provimi { get; set; }

    public virtual DbSet<Salla> Salla { get; set; }

    public virtual DbSet<Semestri> Semestri { get; set; }

    public virtual DbSet<StafiAkademik> StafiAkademik { get; set; }

    public virtual DbSet<Studenti> Studenti { get; set; }
    public DbSet<RegjistrimiSemestrit> RegjistrimiSemestrit { get; set; }

    // public virtual DbSet<Transkripta> Transkripta { get; set; }

    public virtual DbSet<Universiteti> Universiteti { get; set; }

    public virtual DbSet<Useri> Useri { get; set; }
    // public virtual DbSet<Role> Role { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<RezervimiSalles> RezervimiSalles { get; set; }
    public virtual DbSet<SuperAdmin> SuperAdminet { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ERA;Database=smisbackup;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Universiteti>()
            .OwnsMany(u => u.Lokacionet, a =>
         {
             a.WithOwner().HasForeignKey("UniversitetiId");
             a.Property<int>("Id");
             a.HasKey("Id");
         });

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole {  Id = "1",Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
            new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole {  Id = "3",Name = "StafAkademik", NormalizedName = "STAFAKADEMIK" },
            new IdentityRole {  Id = "4",Name = "Student", NormalizedName = "STUDENT" },
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        // modelBuilder.Entity<Useri>()
        //    .HasOne(u => u.Role)
        //    .WithMany(r => r.Users)
        //    .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Studenti>()
            .HasOne(s => s.Useri)
            .WithOne(u => u.Studenti)
            .HasForeignKey<Studenti>(s => s.Id);

        modelBuilder.Entity<Studenti>()
    .HasOne(s => s.Semestri)
    .WithMany()
    .HasForeignKey(s => s.SemestriID)
    .OnDelete(DeleteBehavior.Restrict);




        modelBuilder.Entity<RegjistrimiSemestrit>()
      .HasKey(r => r.RegjistrimiSemestritId);

        modelBuilder.Entity<RegjistrimiSemestrit>()
            .Property(r => r.RegjistrimiSemestritId)
            .HasColumnName("RegjistrimiId");

        modelBuilder.Entity<RegjistrimiSemestrit>()
            .HasOne(r => r.Studenti)
            .WithMany(s => s.RegjistrimiSemestrit)
            .HasForeignKey(r => r.StudentiId);

        modelBuilder.Entity<RegjistrimiSemestrit>()
            .Property(r => r.StudentiId)
            .HasColumnName("StudentID");

        modelBuilder.Entity<RegjistrimiSemestrit>()
            .HasOne(r => r.Semestri)
            .WithMany(s => s.RegjistrimiSemestrit)
            .HasForeignKey(r => r.SemestriId);

        modelBuilder.Entity<RegjistrimiSemestrit>()
            .Property(r => r.SemestriId)
            .HasColumnName("SemestriID");




        modelBuilder.Entity<StafiAkademik>()
            .HasOne(sa => sa.Useri)
            .WithOne(u => u.StafiAkademik)
            .HasForeignKey<StafiAkademik>(sa => sa.Id)
            .IsRequired();

        modelBuilder.Entity<SuperAdmin>()
        .HasOne(s => s.Useri)
        .WithOne()
        .HasForeignKey<SuperAdmin>(s => s.Id)
        .OnDelete(DeleteBehavior.Cascade);


        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8E38461D9");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.UniId).HasColumnName("UniID");
            entity.Property(e => e.Id);

            entity.HasOne(d => d.Departamenti).WithMany(p => p.Admin)
                .HasForeignKey(d => d.DepartamentiId)
                .HasConstraintName("FK__Admin__Departame__4CA06362");

            entity.HasOne(d => d.Uni).WithMany(p => p.Admin)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Admin__UniID__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.Admin)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__Admin__UserID__4D94879B");
        });

        modelBuilder.Entity<Departamenti>(entity =>
        {
            entity.HasKey(e => e.DepartamentiId).HasName("PK__Departam__67077AE2D726109C");

            entity.ToTable("Departamenti");

            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.Emri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UniId).HasColumnName("UniID");

            entity.HasOne(d => d.Universiteti).WithMany(p => p.Departamenti)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Departame__UniID__3A81B327");
        });

        modelBuilder.Entity<Grupi>(entity =>
        {
            entity.HasKey(e => e.GrupiId).HasName("PK__Grupi__580495DA09264F41");

            entity.ToTable("Grupi");

            entity.Property(e => e.GrupiId).HasColumnName("GrupiID");
            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.Emri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrariId).HasColumnName("OrariID");
            entity.Property(e => e.SemestriId).HasColumnName("SemestriID");

            entity.HasOne(d => d.Departamenti).WithMany(p => p.Grupi)
                .HasForeignKey(d => d.DepartamentiId)
                .HasConstraintName("FK__Grupi__Departame__4316F928");

            entity.HasOne(d => d.Orari).WithMany(p => p.Grupi)
                .HasForeignKey(d => d.OrariId)
                .HasConstraintName("FK__Grupi__OrariID__440B1D61");

            entity.HasOne(d => d.Semestri).WithMany(p => p.Grupi)
                .HasForeignKey(d => d.SemestriId)
                .HasConstraintName("FK__Grupi__SemestriI__4222D4EF");
        });

        modelBuilder.Entity<Lenda>(entity =>
        {
            entity.HasKey(e => e.LendaId).HasName("PK__Lenda__F222ADBEE91CCC4B");

            entity.Property(e => e.LendaId).HasColumnName("LendaID");
            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.Emri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SemestriId).HasColumnName("SemestriID");

            entity.HasOne(d => d.Departamenti).WithMany(p => p.Lenda)
                .HasForeignKey(d => d.DepartamentiId)
                .HasConstraintName("FK__Lenda__Departame__5629CD9C");

            entity.HasOne(d => d.Semestri).WithMany(p => p.Lenda)
                .HasForeignKey(d => d.SemestriId)
                .HasConstraintName("FK__Lenda__SemestriI__571DF1D5");
        });

        modelBuilder.Entity<Ligjerata>(entity =>
        {
            entity.HasKey(e => new { e.StafiId, e.LendaId }).HasName("PK__Ligjerat__68B8092B794D08D0");

            entity.Property(e => e.StafiId).HasColumnName("StafiID");
            entity.Property(e => e.LendaId).HasColumnName("LendaID");

            entity.HasOne(d => d.Lenda).WithMany(p => p.Ligjerata)
                .HasForeignKey(d => d.LendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ligjerata__Lenda__5FB337D6");

            entity.HasOne(d => d.Stafi).WithMany(p => p.Ligjerata)
                .HasForeignKey(d => d.StafiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ligjerata__Stafi__5EBF139D");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__Nota__EF36CC7AADB7961F");

            entity.Property(e => e.NotaId).HasColumnName("NotaID");
            entity.Property(e => e.NotaNr).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.ParaqitjaId).HasColumnName("ParaqitjaID");
            entity.Property(e => e.StudentiId).HasColumnName("StudentiID");

            entity.Property(e => e.ParaqitjaId).HasColumnName("ParaqitjaID");

            entity.HasOne(d => d.ParaqitjaEprovimit)
                .WithOne(p => p.Nota)
                .HasForeignKey<Nota>(d => d.ParaqitjaId)
                .HasConstraintName("FK_Nota_ParaqitjaEprovimit");
            entity.HasOne(d => d.Studenti).WithMany(p => p.Nota)
                .HasForeignKey(d => d.StudentiId)
                .HasConstraintName("FK__Nota__StudentiID__6A30C649");


        });

        modelBuilder.Entity<Orari>(entity =>
        {
            entity.HasKey(e => e.OrariId).HasName("PK__Orari__9849076D327A3909");

            entity.ToTable("Orari");

            entity.Property(e => e.OrariId).HasColumnName("OrariID");
            entity.Property(e => e.Lloji)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ndërrimi)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Ligjerata).WithMany(p => p.Orari)
                .UsingEntity<Dictionary<string, object>>(
                    "LigjerataOrari",
                    r => r.HasOne<Ligjerata>().WithMany()
                        .HasForeignKey("StafiId", "LendaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Ligjerata_Orari__6383C8BA"),
                    l => l.HasOne<Orari>().WithMany()
                        .HasForeignKey("OrariId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Ligjerata__Orari__628FA481"),
                    j =>
                    {
                        j.HasKey("OrariId", "StafiId", "LendaId").HasName("PK__Ligjerat__3EC287FF252515D0");
                        j.ToTable("Ligjerata_Orari");
                        j.IndexerProperty<int>("OrariId").HasColumnName("OrariID");
                        j.IndexerProperty<int>("StafiId").HasColumnName("StafiID");
                        j.IndexerProperty<int>("LendaId").HasColumnName("LendaID");
                    });

            entity.HasMany(d => d.Salla).WithMany(p => p.Orari)
                .UsingEntity<Dictionary<string, object>>(
                    "OrariSalla",
                    r => r.HasOne<Salla>().WithMany()
                        .HasForeignKey("SallaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Orari_Sal__Salla__797309D9"),
                    l => l.HasOne<Orari>().WithMany()
                        .HasForeignKey("OrariId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Orari_Sal__Orari__787EE5A0"),
                    j =>
                    {
                        j.HasKey("OrariId", "SallaId").HasName("PK__Orari_Sa__13CB1F8C031D9C39");
                        j.ToTable("Orari_Salla");
                        j.IndexerProperty<int>("OrariId").HasColumnName("OrariID");
                        j.IndexerProperty<int>("SallaId").HasColumnName("SallaID");
                    });
        });

        modelBuilder.Entity<Pagesa>(entity =>
        {
            entity.HasKey(e => e.PagesaId).HasName("PK__Pagesa__D4F1A29DF907B783");

            entity.ToTable("Pagesa");

            entity.Property(e => e.PagesaId).HasColumnName("PagesaID");
            entity.Property(e => e.Shuma).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StudentiId).HasColumnName("StudentiID");

            entity.HasOne(d => d.Studenti).WithMany(p => p.Pagesa)
                .HasForeignKey(d => d.StudentiId)
                .HasConstraintName("FK__Pagesa__Studenti__71D1E811");
        });

        modelBuilder.Entity<ParaqitjaEprovimit>(entity =>
        {
            entity.HasKey(e => e.ParaqitjaId).HasName("PK__Paraqitj__F86D81136DE3097B");

            entity.ToTable("ParaqitjaEProvimit");

            entity.Property(e => e.ParaqitjaId).HasColumnName("ParaqitjaID");
            entity.Property(e => e.LendaId).HasColumnName("LendaID");
            entity.Property(e => e.StudentiId).HasColumnName("StudentiID");
            entity.Property(e => e.EshtePerfunduar).HasColumnName("EshtePerfunduar").HasDefaultValue(false);

            entity.HasOne(d => d.Lenda).WithMany(p => p.ParaqitjaEprovimit)
                .HasForeignKey(d => d.LendaId)
                .HasConstraintName("FK__Paraqitja__Lenda__6EF57B66");

            entity.HasOne(d => d.Studenti).WithMany(p => p.ParaqitjaEprovimit)
                .HasForeignKey(d => d.StudentiId)
                .HasConstraintName("FK__Paraqitja__Stude__6E01572D");
        });

        modelBuilder.Entity<Provimi>(entity =>
        {
            entity.HasKey(e => e.ProvimiId).HasName("PK__Provimi__30F18CFB0E2D146E");

            entity.ToTable("Provimi");

            entity.Property(e => e.ProvimiId).HasColumnName("ProvimiID");
            entity.Property(e => e.LendaId).HasColumnName("LendaID");
            entity.Property(e => e.OrariId).HasColumnName("OrariID");
            entity.Property(e => e.StafiAkademikId).HasColumnName("StafiAkademikID");
            entity.Property(e => e.Statusi)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Lenda).WithMany(p => p.Provimi)
                .HasForeignKey(d => d.LendaId)
                .HasConstraintName("FK__Provimi__LendaID__5AEE82B9");

            entity.HasOne(d => d.Orari).WithMany(p => p.Provimi)
                .HasForeignKey(d => d.OrariId)
                .HasConstraintName("FK__Provimi__OrariID__5BE2A6F2");

            entity.HasOne(d => d.StafiAkademik).WithMany(p => p.Provimi)
                .HasForeignKey(d => d.StafiAkademikId)
                .HasConstraintName("FK__Provimi__StafiAk__59FA5E80");
        });

        modelBuilder.Entity<Salla>(entity =>
        {
            entity.HasKey(e => e.SallaId).HasName("PK__Salla__B8218E197C43C293");

            entity.ToTable("Salla");

            entity.Property(e => e.SallaId).HasColumnName("SallaID");
            entity.Property(e => e.NrSalles)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Objekti)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StafiAkademikId).HasColumnName("StafiAkademikID");
            entity.Property(e => e.UniId).HasColumnName("UniID");

            entity.HasOne(d => d.StafiAkademik).WithMany(p => p.Salla)
                .HasForeignKey(d => d.StafiAkademikId)
                .HasConstraintName("FK__Salla__StafiAkad__74AE54BC");

            entity.HasOne(d => d.Uni).WithMany(p => p.Salla)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Salla__UniID__75A278F5");
        });

        modelBuilder.Entity<Semestri>(entity =>
        {
            entity.HasKey(e => e.SemestriId).HasName("PK__Semestri__E6EAA2D31A5FF8AA");

            entity.ToTable("Semestri");

            entity.Property(e => e.SemestriId).HasColumnName("SemestriID");
            entity.Property(e => e.Semestri1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Semestri");
            entity.Property(e => e.UniId).HasColumnName("UniID");

            entity.HasOne(d => d.Universiteti).WithMany(p => p.Semestri)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Semestri__UniID__3D5E1FD2");
        });

        modelBuilder.Entity<StafiAkademik>(entity =>
        {
            entity.HasKey(e => e.StafiId).HasName("PK__StafiAka__979A23F0A2DE9EE0");

            entity.ToTable("StafiAkademik");

            entity.Property(e => e.StafiId).HasColumnName("StafiID");
            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.RoliStafit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulli)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UniId).HasColumnName("UniID");

            entity.HasOne(d => d.Departamenti).WithMany(p => p.StafiAkademik)
                .HasForeignKey(d => d.DepartamentiId)
                .HasConstraintName("FK__StafiAkad__Depar__46E78A0C");

            entity.HasOne(d => d.Universiteti).WithMany(p => p.StafiAkademik)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__StafiAkad__UniID__47DBAE45");
        });

        modelBuilder.Entity<Studenti>(entity =>
        {
            entity.HasKey(e => e.StudentiId).HasName("PK__Studenti__EBE2AD8DFC0E89E1");

            entity.ToTable("Studenti");

            entity.Property(e => e.StudentiId).HasColumnName("StudentiID");
            entity.Property(e => e.DepartamentiId).HasColumnName("DepartamentiID");
            entity.Property(e => e.GrupiId).HasColumnName("GrupiID");
            entity.Property(e => e.Statusi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UniId).HasColumnName("UniID");

            entity.HasOne(d => d.Departamenti).WithMany(p => p.Studenti)
                .HasForeignKey(d => d.DepartamentiId)
                .HasConstraintName("FK__Studenti__Depart__52593CB8");

            entity.HasOne(d => d.Grupi).WithMany(p => p.Studenti)
                .HasForeignKey(d => d.GrupiId)
                .HasConstraintName("FK__Studenti__GrupiI__534D60F1");

            entity.HasOne(d => d.Universiteti).WithMany(p => p.Studenti)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Studenti__UniID__5165187F");
        });

        // modelBuilder.Entity<Transkripta>(entity =>
        // {
        //     entity.HasKey(e => e.TranskriptalId).HasName("PK__Transkri__F40854979D056D51");

        //     entity.Property(e => e.TranskriptalId).HasColumnName("TranskriptalID");
        //     entity.Property(e => e.StudentiId).HasColumnName("StudentiID");

        //     entity.HasOne(d => d.Studenti).WithMany(p => p.Transkripta)
        //         .HasForeignKey(d => d.StudentiId)
        //         .HasConstraintName("FK__Transkrip__Stude__66603565");
        // });

        modelBuilder.Entity<Universiteti>(entity =>
        {
            entity.HasKey(e => e.UniId).HasName("PK__Universi__9E7FA84A35466E61");

            entity.ToTable("Universiteti");

            entity.Property(e => e.UniId).HasColumnName("UniID");
            entity.Property(e => e.Adress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Emri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Useri>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AspNetUsers");

            // entity.ToTable("Useri");

            entity.Property(e => e.Id);
            entity.Property(e => e.Adresa)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Emri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gjinia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Mbiemri)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nenshtetesia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NrLeternjoftimit)
                .HasMaxLength(20)
                .IsUnicode(false);
            // entity.Property(e => e.Password)
            //     .HasMaxLength(255)
            //     .IsUnicode(false);
            // entity.Property(e => e.Roli)
            //     .HasMaxLength(50)
            //     .IsUnicode(false);
            entity.Property(e => e.Shteti)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefoni)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VendLindja)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Vendbanimi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Zipkodi)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ZIPKodi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
