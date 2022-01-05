using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using russu_vlad_proiect_final.Models;

namespace russu_vlad_proiect_final.Data
{
    public class DbInitializer
    {
        public static void Initialize(RecordStoreContext context)
        {
            context.Database.EnsureCreated();

            if(context.Albums.Any())
            {
                return; 
            }

            var albums = new Album[]
            {
                new Album {Title="Notes On A Conditional Form", Artist="The 1975",      Price=Decimal.Parse("20"), ReleaseDate=DateTime.Parse("2020-05-12")},
                new Album {Title="Wasting Light",               Artist="Foo Fighters",  Price=Decimal.Parse("20"), ReleaseDate=DateTime.Parse("2020-05-12")},
                new Album {Title="Happier Than Ever",           Artist="Billie Eilish", Price=Decimal.Parse("20"), ReleaseDate=DateTime.Parse("2020-05-12")}
            };
            foreach (Album a in albums)
            {
                context.Albums.Add(a);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{CustomerID=1050, Name="Russu Vlad", Address="str. Dorobantilor, nr. 38, Cluj-Napoca", Email="vlad.russu@gmail.com", Phone="0748147963", BirthDate=DateTime.Parse("1991-08-14")},
                new Customer{CustomerID=1045, Name="Bodea Alex", Address="str. Dorobantilor, nr. 38, Cluj-Napoca", Email="vlad.russu@gmail.com", Phone="0748159753", BirthDate=DateTime.Parse("1994-10-22")}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{AlbumID=1, CustomerID=1050, OrderDate=DateTime.Parse("2022-01-04")},
                new Order{AlbumID=3, CustomerID=1045, OrderDate=DateTime.Parse("2022-01-04")},
                new Order{AlbumID=1, CustomerID=1045, OrderDate=DateTime.Parse("2022-01-04")},
                new Order{AlbumID=2, CustomerID=1050, OrderDate=DateTime.Parse("2022-01-04")},
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
        }
    }
}
