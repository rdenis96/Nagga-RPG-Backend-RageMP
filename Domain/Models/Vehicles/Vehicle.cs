using Domain.Models.Common;

namespace Domain.Models.Vehicles
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string model { get; set; }
        public string owner { get; set; }
        public string plate { get; set; }
        public Vector3Wrapper position { get; set; }
        public Vector3Wrapper rotation { get; set; }
        public int colorType { get; set; }
        public string firstColor { get; set; }
        public string secondColor { get; set; }
        public int pearlescent { get; set; }
        public uint dimension { get; set; }
        public int faction { get; set; }
        public int engine { get; set; }
        public int locked { get; set; }
        public int price { get; set; }
        public int parking { get; set; }
        public int parked { get; set; }
        public float gas { get; set; }
        public float kms { get; set; }
    }
}
