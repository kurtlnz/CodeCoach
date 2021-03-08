using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using FluentAssertions;
using Xunit;

namespace NotOO
{
    public class SalesInvoiceTests
    {
        [Fact]
        public void Add_Lines_To_SalesInvoice_And_Verify_They_Have_Been_Added()
        {
            // Setup sales invoice lines
            var apple = new Product("Apple", 0.35m);
            var banana = new Product("Banana", 0.75m);

            // Set up sales invoice
            var invoice = new SalesInvoice();
            
            invoice.Add(apple, 4);
            invoice.Add(banana, 3);

            // Verify sales invoice has added the sales invoice lines.
            invoice.Total.Should().Be(3.65m);
            invoice.LineCount.Should().Be(2);
        }
        
        public class SalesInvoice
        {
            public decimal Total => Lines.Sum(l => l.Subtotal);
            public int LineCount => Lines.Count;
            public List<SalesInvoiceLine> Lines { get; } = new List<SalesInvoiceLine>();

            public void Add(Product product, int quantity)
            {
                Lines.Add(new SalesInvoiceLine(product, quantity));
            }
        }

        public class SalesInvoiceLine
        {
            public Product Product { get; }
            public int Quantity { get; }
            public decimal Subtotal => Quantity * Product.UnitPrice;
            
            public SalesInvoiceLine(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }
        }
        
        public class Product
        {
            public string Name { get; }
            public decimal UnitPrice { get; }
            
            public Product(string name, decimal unitPrice)
            {
                Name = name;
                UnitPrice = unitPrice;
            }
        }
    }
}