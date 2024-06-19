using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(80)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("password")
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(180)
            .HasColumnName("email")
            .HasColumnType("VARCHAR(180)");

        builder.HasMany(x => x.Assignments)
            .WithOne(p => p.User)
            .HasForeignKey(c => c.UserId);

        builder.HasMany(x => x.AssignmentLists)
            .WithOne(p => p.User)
            .HasForeignKey(c => c.UserId);
    }
}