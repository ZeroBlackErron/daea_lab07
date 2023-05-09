using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            //IntroToLINQ();
            //DataSourceLINQ();
            //FilteringLINQ();
            //OrderingLINQ();
            //GroupingLINQ();
            //Grouping2LINQ();
            //JoiningLINQ();

            //IntroToLAMBDA();
            //DataSourceLAMBDA();
            //FilteringLAMBDA();
            //OrderingLAMBDA();
            //GroupingLAMBDA();
            //Grouping2LAMBDA();
            //JoiningLAMBDA();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = 
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (var num in numQuery)
            {
                Console.WriteLine(num);
            }
        }

        static void IntroToLAMBDA()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var evens = numbers.Where(n => n % 2 == 0);

            foreach (var even in evens)
            {
                Console.WriteLine(even);
            }
        }
        
        static void DataSourceLINQ()
        {
            var queryAllCustomers = from cust in context.clientes select cust;

            foreach (var customer in queryAllCustomers)
            {
                Console.WriteLine(customer.NombreCompañia);
            }
        }
        
        static void DataSourceLAMBDA()
        {
            var allCustomers = context.clientes.ToList();

            foreach (var customer in allCustomers)
            {
                Console.WriteLine(customer.NombreCompañia);
            }
        }

        static void FilteringLINQ()
        {
            var queryLondomCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var customer in queryLondomCustomers)
            {
                Console.WriteLine(customer.Ciudad);
            }
        }

        static void FilteringLAMBDA()
        {
            var londomCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres")
                .ToList();

            foreach (var customer in londomCustomers)
            {
                Console.WriteLine(customer.NombreCompañia);
            }
        }

        static void OrderingLINQ()
        {
            var queryLondomCustomersOrderAsc = from cust in context.clientes
                                               where cust.Ciudad == "Londres"
                                               orderby cust.NombreCompañia ascending
                                               select cust;

            foreach (var customer in queryLondomCustomersOrderAsc)
            {
                Console.WriteLine(customer.NombreCompañia);
            }
        }

        static void OrderingLAMBDA()
        {
            var londomCustomersOrderAsc = context.clientes.Where(cust => cust.Ciudad == "Londres")
                .OrderBy(cust => cust.NombreCompañia)
                .ToList();

            foreach (var customer in londomCustomersOrderAsc)
            {
                Console.WriteLine(customer.NombreCompañia);
            }
        }

        static void GroupingLINQ()
        {
            var queryCustomersByCity = from cust in context.clientes 
                                       group cust by cust.Ciudad;

            foreach (var customers in queryCustomersByCity)
            {
                Console.WriteLine(customers.Key);
                foreach (clientes customer in customers)
                {
                    Console.WriteLine(customer.NombreCompañia);
                }
            }
        }

        static void GroupingLAMBDA()
        {
            var customersByCity = context.clientes.GroupBy(cust => cust.NombreCompañia)
                .ToList();

            foreach (var customers in customersByCity)
            {
                Console.WriteLine(customers.Key);
                foreach (clientes customer in customers)
                {
                    Console.WriteLine(customer.NombreCompañia);
                }
            }
        }

        static void Grouping2LINQ()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Grouping2LAMBDA()
        {
            var moreThen2customersByCity = context.clientes.GroupBy(cust => cust.Ciudad)
                .Where(custGroup => custGroup.Count() > 2)
                .ToList();

            foreach (var customer in moreThen2customersByCity)
            {
                Console.WriteLine(customer.Key);
            }
        }

        static void JoiningLINQ()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DisributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void JoiningLAMBDA()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DisributorName = dist.PaisDestinatario };

            var customerSales = context.clientes.Join(
                    context.Pedidos,
                    customer => customer.idCliente,
                    sale => sale.IdCliente,
                    (customer, sale) => new
                    {
                        CustomerName = customer.NombreCompañia,
                        DisributorName = sale.PaisDestinatario
                    }
                )
                .ToList();

            foreach (var cs in customerSales)
            {
                Console.WriteLine(cs.CustomerName);
            }
        }
    }
}
