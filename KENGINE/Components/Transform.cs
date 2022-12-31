using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class Transform : Component
    {
        public Position position = new Position(0, 0, 0);
        public Rotation rotation = new Rotation(0, 0, 0);
        public SizeDelta sizeDelta = new SizeDelta(1, 1, 1);
        public Vector3 GetPosition()
        {
            return new Vector3(position.x, position.y, position.z);
        }
        public Quaternion GetRotation()
        {
            return Quaternion.FromEulerAngles(rotation.x / 180 * (float)Math.PI, rotation.y / 180 * (float)Math.PI, rotation.z / 180 * (float)Math.PI);
        }
        public void SetPosition(Vector3 v)
        {
            position.x = v.X;
            position.y = v.Y;
            position.z = v.Z;
        }
    }
    public class Position
    {
        public float x, y, z;
        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public class Rotation
    {
        public float x, y, z;
        public Rotation(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    public class SizeDelta
    {
        public float x, y, z;
        public SizeDelta(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
