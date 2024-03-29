using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteC41.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteC41.G02.DAL.Data.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
          //Fluent Apis For Employee Domains
          builder.Property(E=>E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E => E.EmailAddress).IsRequired() ;

            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.Gneder)
                .HasConversion(
                (Gender) => Gender.ToString(),
                (Gender) => (Gender)Enum.Parse(typeof(Gender), Gender.ToString()));

        }
    }

}
