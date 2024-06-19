using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Data.Mappings;

public class AssignmentMap : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.AssignmentListId)
            .IsRequired(false);

        builder.Property(x => x.Concluded)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnType("VARCHAR(5)");

        builder.Property(x => x.ConcluedAt)
            .IsRequired(false)
            .HasColumnType("DATETIME");

        builder.Property(x => x.Deadline)
            .IsRequired(false)
            .HasColumnType("DATETIME");
    }
}