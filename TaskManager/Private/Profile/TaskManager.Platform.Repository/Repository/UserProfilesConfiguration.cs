using TaskManager.Profile.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Profile.Repository.Repository;


public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(e => e.UserId);
        builder.ToTable("UserProfiles"); 

        builder.Property(e => e.UserId)
            .HasColumnName("UserId")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(e => e.Username)
            .HasColumnName("Username")
            .HasMaxLength(255);

        builder.Property(e => e.AvatarUrl)
            .HasColumnName("AvatarUrl")
            .HasMaxLength(255);

        builder.Property(e => e.Theme)
            .HasColumnName("Theme")
            .HasMaxLength(50);

        builder.Property(e => e.EmailAddress)
            .HasColumnName("EmailAddress")
            .HasMaxLength(255);

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasMaxLength(20);

        builder.Property(e => e.NotificationPreferences)
            .HasColumnName("NotificationPreferences")
            .HasMaxLength(255);

        builder.Property(e => e.Education)
            .HasColumnName("Education")
            .HasMaxLength(255);
    }
}

