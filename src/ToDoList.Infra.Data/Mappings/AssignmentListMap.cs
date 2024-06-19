using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infra.Data.Mappings;

public class AssignmentListMap : IEntityTypeConfiguration<AssignmentList>
{
    public void Configure(EntityTypeBuilder<AssignmentList> builder)
    {
        builder.ToTable("AssignmentList");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("VARCHAR(80)");

        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.HasMany(x => x.Assignments)
            .WithOne(p => p.AssignmentList)
            .OnDelete(DeleteBehavior.Restrict);
    }
}