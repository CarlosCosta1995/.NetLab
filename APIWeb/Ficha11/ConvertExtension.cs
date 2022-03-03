using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ficha11
{
    public static class ConvertExtension
    {
        //Ref.: https://www.youtube.com/watch?v=XUqfg9albdA
        public static T SwapVehicle<T>(this object o)
        {
            //Ver o type dos objectos
            Type originType = o.GetType();
            Type changeType = typeof(T);

            //criar uma instancia do objecto a ser criado
            object? changeObject = Activator.CreateInstance(changeType);

            //Verificar todas as propriedades do originObject para o changeObject comuns
            var properties = from src in originType.GetProperties()
                             from change in changeType.GetProperties()
                                 //where src.GetCustomAttributes(true) == change.GetCustomAttributes(true)
                             where src.CustomAttributes == change.CustomAttributes
                             select change;

            //copiar todas as propriedades do originObject para changeObject
            foreach (var property in properties.ToList())
            {
                changeType.GetProperties().SetValue(changeType, Convert.ToInt32(originType.CustomAttributes.FirstOrDefault()));
            }
            return (T)changeObject;
        }
    }
}
