using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class Transform : Component
    {
        public Position position {
            get
            {
                if (parent == null)
                {
                    return localP;
                }
                else
                {
                    Vector3 v = parent.position.ToVector3() + parent.rotation.ToQuaternion() * localPosition.ToVector3();
                    return new Position(v.X, v.Y, v.Z);
                }
            }
            set
            {
                localP = value;
            }
        }
        public Position localPosition
        {
            get
            {
                return localP;
            }
            set
            {
                localP = value;
            }
        }
        private Position localP = new Position(0, 0, 0);


        public Rotation rotation
        {
            get
            {
                if (parent == null)
                {
                    return localR;
                }
                else
                {
                    Vector3 v = (parent.rotation.ToQuaternion() * localR.ToQuaternion()).ToEulerAngles();
                    return new Rotation(v.X, v.Y, v.Z);
                }
            }
            private set { }
        }
        public Rotation localRotation {
            get
            {
                return localR;
            }
            set
            {
                localR = value;
            }
        }

        private Rotation localR = new Rotation(0, 0, 0);


        public SizeDelta sizeDelta = new SizeDelta(1, 1, 1);
        public Transform? parent = null;
        public List<Transform> childs { get; private set; } = new List<Transform>();

        public Vector3 forward
        {
            get
            {
                return Vector3.Normalize(rotation.ToQuaternion() * new Vector3(0, 0, 1));
            }
            private set { }
        }
        public Vector3 up
        {
            get
            {
                return Vector3.Normalize(rotation.ToQuaternion() *new Vector3(0, 1, 0));
            }
            private set { }
        }
        public Vector3 right
        {
            get
            {
                return Vector3.Normalize(rotation.ToQuaternion() * new Vector3(-1, 0, 0));
            }
            private set { }
        }
        public void SetPosition(Vector3 v)
        {
            position.x = v.X;
            position.y = v.Y;
            position.z = v.Z;
        }
        public bool HasChild()
        {
            return childs.Count > 0;
        }
        public void Child(Transform child)
        {
            childs.Add(child);
            child.parent = this;
        }
        public Transform GetChild(int id)
        {
            return childs[id];
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
        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
    public class Rotation
    {
        private float X,Y,Z;
        
        public float x
        {
            get
            {
                return X;
            }
            set
            {
                while (true)
                {
                    if (value > 180)
                    {
                        value -= 360;
                    }
                    else if (value < -180)
                    {
                        value += 360;
                    }
                    else
                    {
                        X = value;
                        return;
                    }
                };
            }
        }
        public float y
        {
            get
            {
                return Y;
            }
            set
            {
                while (true)
                {
                    if (value > 180)
                    {
                        value -= 360;
                    }
                    else if (value < -180)
                    {
                        value += 360;
                    }
                    else
                    {
                        Y = value;
                        return;
                    }
                };
            }
        }
        public float z
        {
            get
            {
                return Z;
            }
            set
            {
                while (true)
                {
                    if (value > 180)
                    {
                        value -= 360;
                    }
                    else if (value < -180)
                    {
                        value += 360;
                    }
                    else
                    {
                        Z = value;
                        return;
                    }
                };
            }
        }
        
        public Rotation(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Quaternion ToQuaternion()
        {
            return Quaternion.FromEulerAngles(x, y, z);
        }
        public Vector3 ToEuler()
        {
            return new Vector3(x / 180 * (float)Math.PI, y / 180 * (float)Math.PI, z / 180 * (float)Math.PI);
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
