﻿using System.Collections.Generic;

namespace TestAutomation.model
{
    public class CarDataComparer : IEqualityComparer<CarData>
    {
        public bool Equals(CarData x, CarData y)
        {
            return y != null && x != null && x.Year == y.Year;
        }

        public int GetHashCode(CarData obj)
        {
            return obj.GetHashCode();
        }
        
    }
}