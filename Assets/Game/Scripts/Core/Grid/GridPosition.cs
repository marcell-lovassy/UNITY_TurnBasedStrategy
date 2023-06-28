using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Core.Grid
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int z;

        public GridPosition(int x, int z)
        {
            this.x = x; 
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition position &&
                   x == position.x &&
                   z == position.z;
        }

        public bool Equals(GridPosition other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }

        public override string ToString()
        {
            return $"(x: {x} z: {z})";
        }

        public static bool operator ==(GridPosition a, GridPosition other) => a.x == other.x && a.z == other.z;
        public static bool operator !=(GridPosition a, GridPosition other) => !(a == other);
    }
}
