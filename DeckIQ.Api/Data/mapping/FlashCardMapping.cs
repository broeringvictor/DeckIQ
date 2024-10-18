using DeckIQ.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeckIQ.Api.Data.mapping;

public class FlashCardMapping : IEntityTypeConfiguration<FlashCard>
{
    public void Configure(EntityTypeBuilder<FlashCard> builder)
    {
        builder.ToTable("FlashCard");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Question)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);

        builder.Property(c => c.Answer)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.IncorrectAnswerA)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.IncorrectAnswerB)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.IncorrectAnswerC)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.IncorrectAnswerD)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.CardImage)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);
        builder.Property(c => c.CreateDate)
            .IsRequired();
        builder.Property(c => c.LastUpdateDate)
            .IsRequired();
        builder.Property(c => c.CategoryId)
            .IsRequired(true);
        // Remova HasColumnType e HasMaxLength para usar o mapeamento padrão (bigint)
        builder.Property(c => c.CategoryId)
            .IsRequired(true);
        // Removido HasColumnType("VARCHAR") e HasMaxLength(160)

        builder.Property(c => c.UserId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    
    }
}
