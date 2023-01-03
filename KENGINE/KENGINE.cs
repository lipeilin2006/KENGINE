using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class KENGINE
    {
        public static int VAO;
        public static int VBO;
        public static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        public static List<GameObject> gameObjects { get; private set; } = new List<GameObject>();
        
        public static void LoadShader(string shaderName,string vert,string frag)
        {
            shaders.Add(shaderName, new Shader(vert, frag));
        }
        public static void OnAwake()
        {
            //SetBackColor
            GL.ClearColor(Color4.Black);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Lighting);
            //GL.Enable(EnableCap.RasterizerDiscard);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.FragmentColorMaterialSgix);

            //Lighting
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 3, 8, 8 });
            GL.Light(LightName.Light0, LightParameter.SpotDirection, new float[] { -2, 0, 2 });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1, 1, 1, 1 });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.5f, 0.5f, 0.5f, 1f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1f, 1f, 1f, 1f });
            GL.Enable(EnableCap.Light0);

            GL.Light(LightName.FragmentLight0Sgix, LightParameter.Position, new float[] { 3, 8, 8 });
            GL.Light(LightName.FragmentLight0Sgix, LightParameter.SpotDirection, new float[] { -2, 0, 2 });
            GL.Light(LightName.FragmentLight0Sgix, LightParameter.Diffuse, new float[] { 1, 1, 1, 1 });
            GL.Light(LightName.FragmentLight0Sgix, LightParameter.Ambient, new float[] { 0.5f, 0.5f, 0.5f, 1f });
            GL.Light(LightName.FragmentLight0Sgix, LightParameter.Specular, new float[] { 1f, 1f, 1f, 1f });
            GL.Enable(EnableCap.FragmentLight0Sgix);
        }
        public static void OnUpdate()
        {
            Input.Update();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GameObject.Find("Cube1").transform.rotation += new Vector3(0, 1, 0);
            GameObject.Find("Cube2").transform.localRotation += new Vector3(0, -1, 0);

            foreach (GameObject o in gameObjects)
            {
                foreach (Component c in o.components)
                {
                    c.Update();
                }
            }
        }
    }
}
