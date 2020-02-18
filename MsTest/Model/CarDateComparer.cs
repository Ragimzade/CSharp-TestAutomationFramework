﻿namespace TestAutomation.model
{
    public class CarDateComparer : CarDataComparer
    {
        public new bool Equals(CarData x, CarData y)
        {
            return y != null && x != null && x.Date == y.Date;
        }

    }
}