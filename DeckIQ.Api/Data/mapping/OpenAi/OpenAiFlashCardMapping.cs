using DeckIQ.Core.Models.OpenIa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeckIQ.Api.Data.mapping.OpenAi;

public class OpenAiFlashCardMapping : IEntityTypeConfiguration<OpenIaFlashCard>
{
    public void Configure(EntityTypeBuilder<OpenIaFlashCard> builder)
    {
        builder.ToTable("OpenIaFlashCard");

        // Configuração da chave primária
        builder.HasKey(c => c.Id);

        // Campo UserId
        builder.Property(c => c.UserId)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        // Campos obrigatórios e configuração de tipos
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

        // Campo opcional para tokens da OpenIA
        builder.Property(c => c.OpenIaTokens)
            .IsRequired(false)
            .HasColumnType("BIGINT");

        // Modelo de linguagem utilizado (LLM)
        builder.Property(c => c.LLM)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        // Data de requisição
        builder.Property(c => c.RequesDateTime)
            .IsRequired(true)
            .HasColumnType("DATETIME");
    }
}
