
namespace DataAccess.Data.Configurations
{
    internal class EmployeeConfigurations : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");

            builder.Property(E => E.Gender)
                .HasConversion((G) => G.ToString(), (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E => E.EmployeeType)
                .HasConversion((ET) => ET.ToString(), (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType));
            
            base.Configure(builder);
        }
    }
}
