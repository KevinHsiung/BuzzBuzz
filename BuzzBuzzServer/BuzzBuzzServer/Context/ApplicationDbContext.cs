using BuzzBuzzServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.ToTable("Customer");
                builder.HasKey(x => x.Id);
                builder.HasData(GenerateSeedData());    
                //builder.HasMany(i => i.Products, item =>
                //{
                //    item.ToTable("Product");
                //    item.HasKey("Id");
                //    item.Property(x => x.Id).UseIdentityColumn();
                //    item.WithOwner().HasForeignKey("CustomerId");                    
                //    item.HasData(GenerateProducts());
                //    //item.HasData(
                //    //    new Product { Id = 1, CustomerId = 1, Name="test", Price = 100 },
                //    //    new Product { Id = 2, CustomerId = 1, Name="test", Price = 100 }
                //    //);

                //});
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Product");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).UseIdentityColumn();
                builder.HasQueryFilter(x => x.Status != ProductStatus.Deleted);
                builder.HasData(GenerateProducts());
            });


        }

        private Customer[] GenerateSeedData()
        {
            var seed = new Customer[10];
            var random = new Random();
            var names = new string[10] { "Kevin", "Sam", "Jack", "Kara", "Mark", "Sang", "Jean", "Elena", "Juana", "Susanne" };
            for (int i = 0; i < names.Length; i++)
            {
                seed[i] = new Customer { Id = i + 1, Name = names[i], Age = random.Next(20,80).ToString(), City = "Toronto", Email=$"{names[i]}@email.com", Gender="Any"};
            }
            return seed;
        }

        private Product[] GenerateProducts() {
            var products = new List<Product>();
            var price = new Random();
            var productNames = new List<string> { "Opel Ampera-e ", "Renault Kangoo Maxi ZE 33", "Nissan Leaf ", "Audi e-tron Sportback 55 quattro", "Porsche Taycan Turbo S", "Nissan e-NV200 Evalia ", "Volkswagen ID.3 Pure Performance", "BMW iX3 ", "Nissan Leaf e+", "BMW i3 120 Ah", "Mercedes EQA 250", "DS 3 Crossback E-Tense", "BMW i3s 120 Ah", "Sono Sion ", "Audi e-tron GT RS", "Kia e-Soul 64 kWh", "Renault Zoe ZE50 R110", "Hyundai IONIQ Electric", "Lightyear One ", "Tesla Roadster ", "Peugeot e-208 ", "Honda e ", "CUPRA Born 110 kW - 55 kWh", "Audi Q4 e-tron 35", "Tesla Model Y Long Range Dual Motor", "Tesla Model Y Performance", "Porsche Taycan 4 Cross Turismo", "Skoda Enyaq iV 50", "Volkswagen e-Up! ", "SEAT Mii Electric ", "Opel Corsa-e ", "MG ZS EV", "Volkswagen ID.3 Pro", "Volkswagen ID.3 Pro S", "Hyundai Kona Electric 64 kWh", "Renault Zoe ZE50 R135", "Peugeot e-2008 SUV ", "Audi e-tron 50 quattro", "Porsche Taycan Turbo", "Smart EQ fortwo coupe", "Smart EQ fortwo cabrio", "Smart EQ forfour ", "Honda e Advance", "Byton M-Byte 72 kWh 2WD", "Byton M-Byte 95 kWh 4WD", "Renault Zoe ZE40 R110", "Porsche Taycan 4S", "Porsche Taycan 4S Plus", "Hyundai Kona Electric 39 kWh", "Mercedes EQV 300 Long", "Mazda MX-30 ", "Ford Mustang Mach-E SR RWD", "Ford Mustang Mach-E ER RWD", "Ford Mustang Mach-E SR AWD", "Ford Mustang Mach-E ER AWD", "Ford Mustang Mach-E GT", "Audi e-tron Sportback 50 quattro", "Tesla Cybertruck Single Motor", "Tesla Cybertruck Dual Motor", "Tesla Cybertruck Tri Motor", "Lexus UX 300e", "BMW i4 eDrive40", "Audi e-tron 55 quattro", "Aiways U5 ", "Renault Twingo Electric", "Audi e-tron S 55 quattro", "Audi e-tron S Sportback 55 quattro", "Volkswagen ID.4 1st", "Byton M-Byte 95 kWh 2WD", "Fiat 500e Cabrio", "Opel Mokka-e ", "Skoda Enyaq iV 60", "Skoda Enyaq iV 80", "Skoda Enyaq iV 80X", "Skoda Enyaq iV RS", "Fiat 500e Hatchback 42 kWh", "Citroen e-C4 ", "Jaguar I-Pace EV400", "Kia e-Soul 64 kWh", "Kia e-Soul 39 kWh", "Audi Q4 Sportback e-tron 35", "Nissan Ariya 63kWh", "Nissan Ariya 87kWh", "Nissan Ariya e-4ORCE 63kWh", "Nissan Ariya e-4ORCE 87kWh", "Nissan Ariya e-4ORCE 87kWh Performance", "Volkswagen ID.3 Pro Performance", "MG MG5 EV", "Volkswagen ID.4 Pro Performance", "Mercedes EQV 300 Extra-Long", "Lucid Air Grand Touring", "Lucid Air Touring", "Lucid Air Pure", "Dacia Spring Electric", "Tesla Model 3 Standard Range Plus LFP", "Tesla Model 3 Long Range Dual Motor", "Tesla Model 3 Performance", "Fiat 500e Hatchback 24 kWh", "Fiat 500e 3+1", "Mercedes EQC 400 4MATIC", "Kia e-Niro 64 kWh", "Kia e-Niro 39 kWh", "Citroen e-SpaceTourer XS 50 kWh", "Citroen e-SpaceTourer M 50 kWh", "Citroen e-SpaceTourer XL 50 kWh", "Citroen e-SpaceTourer M 75 kWh", "Citroen e-SpaceTourer XL 75 kWh", "Opel Zafira-e Life S 50 kWh", "Opel Zafira-e Life M 50 kWh", "Opel Zafira-e Life L 50 kWh", "Opel Zafira-e Life M 75 kWh", "Opel Zafira-e Life L 75 kWh", "Peugeot e-Traveller Compact 50 kWh", "Peugeot e-Traveller Standard 50 kWh", "Peugeot e-Traveller Long 50 kWh", "Peugeot e-Traveller Standard 75 kWh", "Peugeot e-Traveller Long 75 kWh", "Audi e-tron 55 quattro", "Audi e-tron Sportback 55 quattro", "Seres 3 ", "Hyundai IONIQ 5 Project 45", "Porsche Taycan ", "Porsche Taycan Plus", "Tesla Model S Long Range", "Tesla Model S Plaid", "Tesla Model X Long Range", "Tesla Model X Plaid", "Mini Cooper SE ", "JAC iEV7s ", "Volkswagen ID.4 Pure Performance", "Audi e-tron GT quattro", "Volvo C40 Recharge", "Hyundai Kona Electric 39 kWh", "Hyundai Kona Electric 64 kWh", "Porsche Taycan 4S Cross Turismo", "Porsche Taycan Turbo Cross Turismo", "Porsche Taycan Turbo S Cross Turismo", "Kia EV6 GT", "BMW iX xDrive40", "BMW iX xDrive50", "MG MG5 Electric", "MG Marvel R Performance", "Hyundai IONIQ 5 Standard Range 2WD", "Hyundai IONIQ 5 Standard Range AWD", "Hyundai IONIQ 5 Long Range 2WD", "Hyundai IONIQ 5 Long Range AWD", "Kia EV6 Standard Range 2WD", "Kia EV6 Long Range 2WD", "Kia EV6 Long Range AWD", "Mercedes EQS 450+", "Mercedes EQS 580 4MATIC", "Tesla Model 3 Standard Range Plus", "Polestar 2 Standard Range Single Motor", "Polestar 2 Long Range Single Motor", "Polestar 2 Long Range Dual Motor", "Volkswagen ID.4 Pure", "Audi Q4 e-tron 40", "Audi Q4 e-tron 50 quattro", "Audi Q4 Sportback e-tron 50 quattro", "Mercedes EQB 350 4MATIC", "Volkswagen ID.4 GTX", "Mercedes EQA 300 4MATIC", "Mercedes EQA 350 4MATIC", "Toyota PROACE Verso M 50 kWh", "Toyota PROACE Verso L 50 kWh", "Toyota PROACE Verso M 75 kWh", "Toyota PROACE Verso L 75 kWh", "CUPRA Born 150 kW - 62 kWh", "CUPRA Born 170 kW - 62 kWh", "CUPRA Born 170 kW - 82 kWh", "BMW i4 M50", "Volvo XC40 Recharge Twin Pure Electric", "Renault Megane E-Tech Electric", "Peugeot e-Rifter Standard 50 kWh", "Peugeot e-Rifter Long 50 kWh", "MG Marvel R ", "Tesla Model 3 Long Range Dual Motor", "MG MG5 EV Long Range", "Audi Q4 e-tron 45 quattro", "Audi Q4 Sportback e-tron 40" };
            var productId = 1;
            for (int i = 0; i < 10; i++)
            {                
                for (int j = 0; j < productNames.Count; j++)
                {
                    products.Add(new Product { CustomerId = i + 1, Id = ++productId , Name = productNames[j], Price = price.Next(40000, 100000), Status= ProductStatus.Active }) ;
                }                
            }
            return products.ToArray();
        }


        public async Task<int> SaveChanges()
        {
            try
            {
                return await base.SaveChangesAsync();

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
