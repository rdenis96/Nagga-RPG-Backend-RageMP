using System;

namespace Domain.Models.Common
{
    public class Vector3Wrapper : IEquatable<Vector3Wrapper>
    {
        public float Y { get; set; }
        public float X { get; set; }
        public float Z { get; set; }

        public Vector3Wrapper(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Vector3Wrapper);
        }

        public bool Equals(Vector3Wrapper other)
        {
            return other != null &&
                   Y == other.Y &&
                   X == other.X &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, X, Z);
        }
    }
}
