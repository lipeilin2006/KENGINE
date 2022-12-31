﻿using OpenTK.Mathematics;
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
        //World Space
        public Position position = new Position(0, 0, 0);
        public Rotation rotation = new Rotation(0, 0, 0);
        public SizeDelta sizeDelta = new SizeDelta(1, 1, 1);

        //Local Space
        public Vector3 forward
        {
            get
            {
                Vector3 f;
                f.X = MathF.Cos(rotation.x / 180 * (float)Math.PI) * MathF.Cos(rotation.y / 180 * (float)Math.PI);
                f.Y = MathF.Sin(rotation.x / 180 * (float)Math.PI);
                f.Z = MathF.Cos(rotation.x / 180 * (float)Math.PI) * MathF.Sin(rotation.y / 180 * (float)Math.PI);
                return Vector3.Normalize(f);
            }
        }
        public Vector3 up
        {
            get
            {
                return Vector3.Normalize(Vector3.Cross(right, forward));
            }
        }
        public Vector3 right
        {
            get
            {
                return Vector3.Normalize(Vector3.Cross(forward, Vector3.UnitY));
            }
        }

        public Vector3 GetPosition()
        {
            return new Vector3(position.x, position.y, position.z);
        }
        public Quaternion GetRotation()
        {
            return Quaternion.FromEulerAngles(rotation.x / 180 * (float)Math.PI, rotation.y / 180 * (float)Math.PI, rotation.z / 180 * (float)Math.PI);
        }
        public Vector3 GetEuler()
        {
            return new Vector3(rotation.x / 180 * (float)Math.PI, rotation.y / 180 * (float)Math.PI, rotation.z / 180 * (float)Math.PI);
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
