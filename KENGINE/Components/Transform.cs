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
        private Vector3 localP;
        public Vector3 position {
            get
            {
                if (parent == null)
                {
                    return localP;
                }
                else
                {
                    return parent.position + parent.GetQuaternion() * localPosition;
                }
            }
            set
            {
                if (parent == null)
                {
                    localP = value;
                }
                else
                {
                    localP = Quaternion.FromEulerAngles(
                        (rotation.X - 360) / 180 * (float)Math.PI,
                        (rotation.Y - 360) / 180 * (float)Math.PI,
                        (rotation.Z - 360) / 180 * (float)Math.PI) * (value - parent.position);
                }
            }
        }
        public Vector3 localPosition
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


        public Vector3 rotation
        {
            get
            {
                if (parent == null)
                {
                    return localRotation;
                }
                else
                {
                    return (parent.GetQuaternion() * GetLocalQuaternion()).ToEulerAngles() * 180 / (float)Math.PI;
                }
            }
            set
            {
                if (parent == null)
                {
                    localRotation = value;
                }
                else
                {
                    localRotation = Quaternion.FromEulerAngles(value.X, value.Y, value.Z) * Quaternion.FromEulerAngles(
                        (parent.rotation.X - 360) / 180 * (float)Math.PI,
                        (parent.rotation.Y - 360) / 180 * (float)Math.PI,
                        (parent.rotation.Z - 360) / 180 * (float)Math.PI).ToEulerAngles() * 180 / (float)Math.PI;
                }
            }
        }
        public Vector3 localRotation { get; set; }


        public SizeDelta sizeDelta = new SizeDelta(1, 1, 1);


        public Transform? parent = null;
        public List<Transform> childs { get; private set; } = new List<Transform>();

        public Vector3 forward
        {
            get
            {
                return Vector3.Normalize(GetQuaternion() * new Vector3(0, 0, 1));
            }
            private set { }
        }
        public Vector3 up
        {
            get
            {
                return Vector3.Normalize(GetQuaternion() *new Vector3(0, 1, 0));
            }
            private set { }
        }
        public Vector3 right
        {
            get
            {
                return Vector3.Normalize(GetQuaternion() * new Vector3(-1, 0, 0));
            }
            private set { }
        }
        public Quaternion GetQuaternion()
        {
            return Quaternion.FromEulerAngles(rotation.X / 180 * (float)Math.PI, rotation.Y / 180 * (float)Math.PI, rotation.Z / 180 * (float)Math.PI);
        }
        public Quaternion GetLocalQuaternion()
        {
            return Quaternion.FromEulerAngles(localRotation.X / 180 * (float)Math.PI, localRotation.Y / 180 * (float)Math.PI, localRotation.Z / 180 * (float)Math.PI);
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
