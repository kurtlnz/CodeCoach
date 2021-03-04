using System.Collections.Generic;
using System.Linq;
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
            var lines = new List<SalesInvoiceLine>
            {
                new SalesInvoiceLine(new Apple(), 4), 
                new SalesInvoiceLine(new Banana(), 3)
            };

            // Set up sales invoice
            var invoice = new SalesInvoice(lines);

            // Verify sales invoice has added the sales invoice lines.
            invoice.Total.Should().Be(3.65m);
            invoice.LineCount.Should().Be(2);
        }
        
        public class SalesInvoice
        {
            public SalesInvoice(List<SalesInvoiceLine> lines)
            {
                Lines = lines;
            }

            public decimal Total => Lines.Sum(l => l.Subtotal);
            public int LineCount => Lines.Count;
            public List<SalesInvoiceLine> Lines { get; set; }
        }

        public class SalesInvoiceLine
        {
            public SalesInvoiceLine(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }
            
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal Subtotal => Quantity * Product.UnitPrice;
        }

        public class Apple : Product
        {
            public string Name => "Apple";
            public decimal UnitPrice => 0.35m;
        }
        
        public class Banana : Product
        {
            public string Name => "Banana";
            public decimal UnitPrice => 0.75m;
        }
        
        public class Product
        {
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}