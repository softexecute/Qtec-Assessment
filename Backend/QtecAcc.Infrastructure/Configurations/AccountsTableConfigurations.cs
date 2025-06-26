using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QtecAcc.Domains;


namespace QtecAcc.Infrastructure.Configurations
{
    internal class AccountsTableConfigurations :
        IEntityTypeConfiguration<Account>,
        IEntityTypeConfiguration<Journal>,
        IEntityTypeConfiguration<JournalLine>

    {
        public void Configure(EntityTypeBuilder<Journal> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(p => p.Description).HasMaxLength(255);
            builder.HasMany(p => p.Lines).WithOne(f => f.Journal).HasForeignKey(f=>f.JournalId);
        }

        public void Configure(EntityTypeBuilder<JournalLine> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Journal).WithMany(f => f.Lines);
           
            builder.Property(p => p.Debit).HasDefaultValue(0).HasPrecision(18,2).IsRequired();
            builder.Property(p => p.Credit).HasDefaultValue(0).HasPrecision(18, 2).IsRequired();

            builder.HasOne(f => f.Account).WithMany(f => f.JournalLines).HasForeignKey(f => f.AccountId);

        }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).HasMaxLength(125).IsRequired();
            builder.Property(f => f.Type).IsRequired().HasMaxLength(12);
            builder.HasMany(f => f.JournalLines).WithOne(f => f.Account).HasForeignKey(a => a.AccountId);
        }
    }
}
