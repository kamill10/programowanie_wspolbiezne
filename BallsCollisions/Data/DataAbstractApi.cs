using System;
using System.Collections.Generic;
using System.Text;
namespace Data
{
   
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateDataApi()
        {
            return new DataLayer();
        }
    
    }
    public class DataLayer:DataAbstractApi
    {
        public DataLayer() { }

       
    }
}