using System;
using System.Collections.Generic;
using System.Text;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class ValueService : IValuesInterface
    {
        public int GetValue()
        { 
                return 2;
        }
    }
}
