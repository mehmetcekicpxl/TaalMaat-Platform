using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;

namespace TaalMaat.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Gemeente> Gemeenten { get; set; }
    public DbSet<Beschikbaarheid> Beschikbaarheden { get; set; }
    public DbSet<BuddyVerzoek> BuddyVerzoeken { get; set; }
    public DbSet<BuddyKoppeling> BuddyKoppelingen { get; set; }
    public DbSet<Sessie> Sessies { get; set; }
    public DbSet<Oefening> Oefeningen { get; set; }
    public DbSet<OefeningVraag> OefeningVragen { get; set; }
    public DbSet<Bericht> Berichten { get; set; }
    public DbSet<ChatRapport> ChatRapporten { get; set; }
    public DbSet<ExterneBron> ExterneBronnen { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.ContactPhoneNumber).HasMaxLength(20);
            entity.Property(u => u.Hobbies).HasMaxLength(500);
            entity.Property(u => u.ShortBio).HasMaxLength(1000);
        });

        // === ChatRapport configuratie ===
        builder.Entity<ChatRapport>(entity =>
        {
            entity.HasOne(r => r.Rapporteerder)
                  .WithMany()
                  .HasForeignKey(r => r.RapporteerderId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Gerapporteerde)
                  .WithMany()
                  .HasForeignKey(r => r.GerapporteerdeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BuddyVerzoek>(entity =>
        {
            entity.HasOne(v => v.Verzender)
                  .WithMany(u => u.VerzondBuddyVerzoeken)
                  .HasForeignKey(v => v.VerzenderId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Ontvanger)
                  .WithMany(u => u.OntvangenBuddyVerzoeken)
                  .HasForeignKey(v => v.OntvangerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(v => v.AfwijzingBericht).HasMaxLength(500);
        });

        // === Bericht configuratie ===
        builder.Entity<Bericht>(entity =>
        {
            entity.HasOne(b => b.Afzender)
                  .WithMany()
                  .HasForeignKey(b => b.AfzenderId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Ontvanger)
                  .WithMany()
                  .HasForeignKey(b => b.OntvangerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(b => b.Inhoud).HasMaxLength(1000).IsRequired();
        });

        builder.Entity<BuddyKoppeling>(entity =>
        {
            entity.HasOne(k => k.Vrijwilliger)
                  .WithMany()
                  .HasForeignKey(k => k.VrijwilligerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(k => k.Anderstalig)
                  .WithMany()
                  .HasForeignKey(k => k.AnderstaligId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Beschikbaarheid>(entity =>
        {
            entity.HasOne(b => b.Gebruiker)
                  .WithMany(u => u.Beschikbaarheden)
                  .HasForeignKey(b => b.GebruikerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Sessie>(entity =>
        {
            entity.HasOne(s => s.Vrijwilliger).WithMany().HasForeignKey(s => s.VrijwilligerId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(s => s.Anderstalig).WithMany().HasForeignKey(s => s.AnderstaligId).OnDelete(DeleteBehavior.Restrict);
            entity.Property(s => s.JitsiUrl).HasMaxLength(200);
        });

        builder.Entity<Oefening>(entity =>
        {
            entity.Property(o => o.Titel).HasMaxLength(200).IsRequired();
            entity.Property(o => o.YouTubeUrl).HasMaxLength(500);
        });

        // === OefeningVraag configuratie ===
        builder.Entity<OefeningVraag>(entity =>
        {
            entity.HasOne(v => v.Oefening)
                  .WithMany(o => o.Vragen)
                  .HasForeignKey(v => v.OefeningId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(v => v.VraagTekst).HasMaxLength(1000).IsRequired();
            entity.Property(v => v.JuistAntwoord).HasMaxLength(500).IsRequired();
        });

        builder.Entity<Gemeente>(entity =>
        {
            entity.Property(g => g.Naam).HasMaxLength(100).IsRequired();
        });

        builder.Entity<ExterneBron>(entity =>
        {
            entity.Property(b => b.Titel).HasMaxLength(200).IsRequired();
            entity.Property(b => b.Url).HasMaxLength(500).IsRequired();
            entity.Property(b => b.Beschrijving).HasMaxLength(1000);
        });
    }
}
