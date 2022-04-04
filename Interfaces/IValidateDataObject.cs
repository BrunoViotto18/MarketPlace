using System;

namespace Interfaces
{
    public interface IValidateDataObject<T>{
       
        Boolean validateObject(T obj);
    }
}