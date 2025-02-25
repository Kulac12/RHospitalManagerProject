using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {

        //Burada yazdıklarım <Product> için geçerli olan kurallardır. 
        //Onu temsilen p, c, k kullanılabilir bize kalmış. Ancak temsili kullanılan bu ifadeler
        //onu kastetmektedir. 

        public ProductValidator()
        {
            // p nin ProductName i en az 2 karakter uzunluğunda olmalıdır.
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();  //Boş olarak geçilemez demek
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0); //0 dan büyük olmalı, 0 da olmaz

            //içecek kategorisinin ürünleri minimum 10 lira olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10)
                .When(p => p.CategoryId == 1);

            //Benim ürünlerim A ile başlamalı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalıdır.");
            //Buradaki StartWithA benim kendi yazdığım bir metot. Bu metotta ben istediğim kuralları verebilir
            //istediğim kuralları belirleyebilirim


            
        }

        private bool StartWithA(string arg) //arg bu önceki metottaki productName
        {
            return arg.StartsWith("A");
        }



    }
}
