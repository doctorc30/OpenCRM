using System;
using System.Collections.Generic;
using bbom.Data.IdentityModel;

namespace Solomon.Test.Mock.Repository
{
    delegate object ModelObjectCreate(int index);

    public class MockObjectCreator<T> where T : class
    {
        private static Dictionary<Type, ModelObjectCreate> _modelObjectCreates = new Dictionary<Type, ModelObjectCreate>
            {
                {
                    typeof(AspNetUser), index => new AspNetUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = typeof(AspNetUser).Name,
                        AspNetRoles = new List<AspNetRole> { new MockObjectCreator<AspNetRole>().Create(index)}
                    }
                },
                {
                    typeof(Discount), index => new Discount
                    {
                        Id = index,
                        Name = typeof(Discount).Name,
                        DiscountAmount = 100,
                        DiscountType = new MockObjectCreator<DiscountType>().Create(),
                        DiscountTypeId = new MockObjectCreator<DiscountType>().Create().Id
                    }
                },
                {
                    typeof(DiscountType), index => new DiscountType
                    {
                        Id = index,
                        Name = typeof(DiscountType).Name
                    }
                },
                {
                    typeof(PaymentPlan), index => new PaymentPlan
                    {
                        Id = index,
                        Name = typeof(PaymentPlan).Name,
                        Amount = 100
                    }
                },
                {
                    typeof(Payment), index => new Payment
                    {
                        Id = index,
                        Amount = 100,
                        AspNetUser = new MockObjectCreator<AspNetUser>().Create(),
                        Discount = new MockObjectCreator<Discount>().Create(),
                        PaymentPlan = new MockObjectCreator<PaymentPlan>().Create(),
                        UserId = new MockObjectCreator<AspNetUser>().Create().Id,
                        Date = DateTime.Now,
                        Status = 1
                    }
                },
                {
                    typeof(AspNetRole), index =>
                    {
                        var role = new AspNetRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = index.ToString()
                        };
                        return role;
                    }
                }
            };

        public T Create()
        {
            return (T) _modelObjectCreates[typeof(T)].Invoke(0);
        }

        public T Create(int index)
        {
            return (T)_modelObjectCreates[typeof(T)].Invoke(index);
        }
    }
}