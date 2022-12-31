using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class Camera : Component
    {
        public static Camera main { get; set; }
        public static short cameraCount { get; private set; }
        public float fov { get; set; } = MathHelper.PiOver4;
        public Matrix4 lookat;
        public float aspect { get; set; }
        public short cameraID { get; private set; }
        public override void Update()
        {
            if (cameraID == main.cameraID)
            {
                GL.MatrixMode(MatrixMode.Projection);
                Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 1, 64);
                GL.LoadMatrix(ref perpective);

                GL.MatrixMode(MatrixMode.Modelview);
                Transform t = gameObject.GetComponent<Transform>();

                Vector3 position = t.GetPosition();

                lookat = Matrix4.LookAt(position, position + t.forward, t.up);
                GL.LoadMatrix(ref lookat);
            }
        }
        public void Activate()
        {
            main = this;
        }
    }
}
