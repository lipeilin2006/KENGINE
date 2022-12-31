using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class CubeMesh : Mesh
    {
        /*
        private readonly float[] vertices =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };*/
        public override void Draw(Vector3 position, Quaternion rotation, SizeDelta sizeDelta)
        {
            /*
            Transform t = gameObject.GetComponent<Transform>();
            Quaternion rotation = t.GetRotation();
            Vector3 position = t.GetPosition();

            int n = 0;
            {
                Vector3 vn = rotation * new Vector3(0.0f, 0.0f, -1.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            {
                Vector3 vn = rotation * new Vector3(0.0f, 0.0f, 1.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            {
                Vector3 vn = rotation * new Vector3(-1.0f, 0.0f, 0.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            {
                Vector3 vn = rotation * new Vector3(1.0f, 0.0f, 0.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            {
                Vector3 vn = rotation * new Vector3(0.0f, -1.0f, 0.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            {
                Vector3 vn = rotation * new Vector3(0.0f, 1.0f, 0.0f);
                Vector3 v1 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v2 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                Vector3 v3 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v4 = rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v5 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position;
                Vector3 v6 = rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position;
                vertices[n + 0] = v1.X; vertices[n + 1] = v1.Y; vertices[n + 2] = v1.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v2.X; vertices[n + 1] = v2.Y; vertices[n + 2] = v2.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v3.X; vertices[n + 1] = v3.Y; vertices[n + 2] = v3.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v4.X; vertices[n + 1] = v4.Y; vertices[n + 2] = v4.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v5.X; vertices[n + 1] = v5.Y; vertices[n + 2] = v5.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
                vertices[n + 0] = v6.X; vertices[n + 1] = v6.Y; vertices[n + 2] = v6.Z; vertices[n + 3] = vn.X; vertices[n + 4] = vn.Y; vertices[n + 5] = vn.Z;
                n += 8;
            }
            */
            /*
            GL.BindVertexArray(KENGINE.VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, KENGINE.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StreamDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            KENGINE.shaders["Default"].Use();
            GL.BindVertexArray(KENGINE.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            */

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1.0, 1.0, 1.0);

            //back
            GL.Normal3(rotation * new Vector3(0, 0, -1));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);

            //bottom
            GL.Normal3(rotation * new Vector3(0, -1, 0));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);

            //left
            GL.Normal3(rotation * new Vector3(-1, 0, 0));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);

            //front
            GL.Normal3(rotation * new Vector3(0, 0, 1));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);

            //top
            GL.Normal3(rotation * new Vector3(0, 1, 0));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * -0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);

            //right
            GL.Normal3(rotation * new Vector3(1, 0, 0));
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * -0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * 0.5f, sizeDelta.z * 0.5f) + position);
            GL.Vertex3(rotation * new Vector3(sizeDelta.x * 0.5f, sizeDelta.y * -0.5f, sizeDelta.z * 0.5f) + position);

            GL.End();
        }
    }
}
