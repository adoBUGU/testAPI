using trackingAPI.Model;

namespace trackingAPI.Data
{
    public class SeedData
    {
        public static void SeedOrder(OrderDBContext context)
        {
            if (!context.Orders.Any())
            {
                var ord1 = new Order
                {
                    OrderID = 1,
                    CustomerName = "Customer One",
                    Address = "1 Address.St Ohio USA",
                    OrderDate = DateTime.Now,
                    Items = new Item[] {
                    new Item { ProductID= 1, ProductQuantity =1 },
                    new Item { ProductID= 2, ProductQuantity =2 },
                    new Item { ProductID= 3, ProductQuantity =3 },
                    new Item {ProductID= 4, ProductQuantity =4 }
                    }
                };

            var ord2 = new Order
            {
                OrderID = 2,
                CustomerName = "Customer Two",
                Address = "22 Chicago.St New Jersey USA",
                OrderDate = DateTime.Now,
                Items = new Item[] {
                    new Item { ProductID= 1, ProductQuantity =1 },
                    new Item { ProductID= 2, ProductQuantity =2 },
                    new Item {ProductID= 3, ProductQuantity =3 },
                    new Item { ProductID= 4, ProductQuantity =4 }
                }
            };

            var ord3 = new Order
            {
                OrderID = 3,
                CustomerName = "Customer Three",
                Address = "32 Maria Theresa.St Santiago New York USA",
                OrderDate = DateTime.Now,
                Items = new Item[] {
                    new Item { ProductID= 1, ProductQuantity =2 },
                    new Item {ProductID= 2, ProductQuantity =2 },
                }
            };
            context.Orders.Add(ord1);
            context.Orders.Add(ord2);
            context.Orders.Add(ord3);
            context.SaveChanges();
            }
        }

    }
}
