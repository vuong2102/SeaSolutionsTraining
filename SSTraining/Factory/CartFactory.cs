using SSTraining.Config;
using SSTraining.Model.BaseModel;
using SSTraining.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Factory
{
    public class CartFactory : IEntityFactory
    {
        private readonly string _customerId;
        private readonly decimal _totalAmount;
        private readonly List<ShoppingCart> _shoppingCarts;

        public CartFactory(string customerId, decimal totalAmount, List<ShoppingCart> shoppingCarts)
        {
            _customerId = customerId;
            _totalAmount = totalAmount;
            _shoppingCarts = shoppingCarts;
        }

        public override BaseEntity CreateBaseEntity()
        {
            var cartId = Guid.NewGuid().ToString();
            _shoppingCarts.ForEach(c => c.Cart_Id = cartId);

            return new Cart
            {
                Id = cartId,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                CustomerId = _customerId,
                TotalAmount = _totalAmount,
                ShoppingCarts = _shoppingCarts
            };
        }

        public override void SaveBaseEntity(DatabaseContext context, BaseEntity entity)
        {
            entity.Save(context);
        }
    }

}
