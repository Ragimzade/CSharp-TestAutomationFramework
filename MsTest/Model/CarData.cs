using System;
using System.Collections.Generic;
using static System.String;

namespace MsTest.Model
{
    public class CarData : IEquatable<CarData>, IComparable<CarData>
    {
        public string Name { get; }
        public int Price { get; }
        public string Year { get; }
        public string Date { get; }

        public CarData(string name, int price, string year, string date)
        {
            Name = name;
            Price = price;
            Year = year;
            Date = date;
        }

        public override string ToString()
        {
            return "Name=" + Name + ", Price=" + Price + ", Year=" + Year + ", Date=" + Date;
        }

        public bool Equals(CarData other)
        {
            if (other == null) return false;
            return Name == other.Name && Price == other.Price && Year == other.Year && Date == other.Date;
        }

        public override bool Equals(object obj) => Equals(obj as CarData);


        public int CompareTo(CarData other)
        {
            return other == null ? 1 : Compare(Name, other.Name, StringComparison.Ordinal);
        }


        public override int GetHashCode() => (Name, Price, Year, Date).GetHashCode();
    }

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

    public class CarDateComparer : CarDataComparer
    {
        public new bool Equals(CarData x, CarData y)
        {
            return y != null && x != null && x.Date == y.Date;
        }
    }
}