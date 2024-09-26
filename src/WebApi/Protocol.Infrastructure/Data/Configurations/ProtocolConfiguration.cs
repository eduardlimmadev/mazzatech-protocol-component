using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Protocol.Infrastructure.Data.Configurations
{
    public class ProtocolConfiguration : IEntityTypeConfiguration<Domain.Entities.Protocol>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Protocol> builder)
        {
            builder.ToTable("protocols");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(p => p.ProtocolNumber)
                   .HasColumnName("protocol_number")
                   .IsRequired();

            builder.Property(p => p.ViaNumber)
                   .HasColumnName("via_number")
                   .IsRequired();

            builder.OwnsOne(p => p.Cpf, cpf =>
            {
                cpf.Property(x => x.Number)
                   .HasColumnName("cpf")
                   .IsRequired();
            });

            builder.OwnsOne(p => p.Rg, rg =>
            {
                rg.Property(x => x.Number)
                   .HasColumnName("rg")
                   .IsRequired();
            });

            builder.Property(p => p.Name)
                   .HasColumnName("full_name")
                   .IsRequired();

            builder.Property(p => p.MotherName)
                   .HasColumnName("mother_name");

            builder.Property(p => p.FatherName)
                   .HasColumnName("father_name");

            builder.Property(p => p.PhotoId)
                   .HasColumnName("photo_id")
                   .IsRequired();
        }
    }
}
