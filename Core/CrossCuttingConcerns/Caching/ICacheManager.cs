using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key); //cache de var mı
        void Remove(string key); //sana verdiğim keye ait nesneyi uçur.
        void RemoveByPattern(string pattern); // sonu vs önemli değil içinde başında GEt, product geçenleri uçur gibi çok sayıda nesneyide cache den silebiliriz. 
   
    
    }
}
