using Amazon.DynamoDBv2.DocumentModel;
using Customize.Domain.Entities;

namespace Customize.Infra.Extensions
{
    public static class TableExtensions
    {
        public const string CustomizeTable = "customize";

        public const string PKName = "PK";
        public const string SKName = "SK";

        public const string CustomerPK = "CUST#";

        public const string SkDateIndex = "GSI_SK_CreatedAt";

        #region Customer

        public static string MapCustomerPK(this string id)
        {
            return CustomerPK + id;
        }

        public static string MapCustomerSK()
        {
            return CustomerPK;
        }

        public static string GetCustomerID(this Document doc)
        {
            return doc[PKName].AsString().Replace(CustomerPK, string.Empty);
        }

        public static Document MapToDocument(this Customer customer) 
        {
            var doc = new Document
            {
                { PKName, customer.Id.MapCustomerPK() },
                { SKName, MapCustomerSK()},
                { nameof(customer.Name), customer.Name },
                { nameof(customer.Email), customer.Email },
                { nameof(customer.Cellphone), customer.Cellphone },
                { nameof(customer.CreatedAt), customer.CreatedAt },
            };

            return doc;
        }

        public static Customer MapToCustomer(this Document doc)
        {
            var customer = new Customer(
                doc.GetCustomerID(),
                doc[nameof(Customer.Name)],
                doc[nameof(Customer.Cellphone)],
                doc[nameof(Customer.Email)],
                doc[nameof(Customer.CreatedAt)].AsDateTime()
                );

            return customer;
        }


        #endregion Customer
    }
}
