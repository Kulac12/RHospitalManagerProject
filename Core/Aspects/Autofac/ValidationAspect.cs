using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        //sana bir tane Validator gelmeli. 
        public ValidationAspect(Type validatorType) 
        {
            //Eğer senin kullanıcının yazdığı şey bir validator değilse o zaman kız ona
            //Eğer diyor ki gönderilen validator type bir IValidator değilse ona kız. 
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil.");
            }
            //eğer hata fırlatılmazsa o zaman benim validator type ım gönderilen validator typetr onunla işlem yapabilrisin demektir.

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); 
            //validatorType şu an "Product" demektir.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //ProductValidator ın çalışma tipini bul, ProductValidator ın Baseine git onun çalıştığı tipi bul.
            // ProductValidator : AbstractValidator<Product> bu kısım aslında 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities) //her birini tek tek gez ve validation kullanarak Validate et.
            {
                ValidationTool.Validate(validator, entity); 
            }

        }
    }




}
