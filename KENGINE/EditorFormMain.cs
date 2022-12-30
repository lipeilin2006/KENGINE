using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using KENGINE;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenTK.WinForms.TestForm
{
	public partial class EditorFormMain : Form
	{
        private Timer timer = null!;
        static TreeView sceneTree;
        public EditorFormMain()
		{
			InitializeComponent();
            sceneTree = treeView1;
        }

        private void glControl_Load(object? sender, EventArgs e)
        {
            //KENGINE.KENGINE.VAO = GL.GenVertexArray();
            //KENGINE.KENGINE.VBO = GL.GenBuffer();
            //KENGINE.KENGINE.LoadShader("Default", File.ReadAllText(".\\Shaders\\Default.vert"), File.ReadAllText(".\\Shaders\\Default.frag"));
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            //Call Awake
            KENGINE.KENGINE.OnAwake();

            //Add GameObject
            GameObject cam = new GameObject("Camera");
            Camera.main = cam.AddComponent<Camera>();
            cam.GetComponent<Transform>().position.z = -5;
            cam.GetComponent<Transform>().position.y = 0;

            GameObject o1 = new GameObject("Cube1");
            o1.GetComponent<Transform>().position.x = -1;
            MeshReneder o1m = o1.AddComponent<MeshReneder>();
            o1m.mesh = new CubeMesh();

            GameObject o2 = new GameObject("Cube2");
            o2.GetComponent<Transform>().position.x = 1;
            MeshReneder o2m = o2.AddComponent<MeshReneder>();
            o2m.mesh = new CubeMesh();

            GameObject f = new GameObject("Floor");
            f.GetComponent<Transform>().position.x = 0;
            f.GetComponent<Transform>().position.y = -2;
            f.GetComponent<Transform>().sizeDelta.x = 10;
            f.GetComponent<Transform>().sizeDelta.z = 10;
            MeshReneder fm = f.AddComponent<MeshReneder>();A game engine based on .net 7,and will continue 
            fm.mesh = new CubeMesh();

            {
                //Bind Event
                glControl.Resize += glControl_Resize;
                glControl.Paint += glControl_Paint;

                //Setup Update
                timer = new Timer();
                timer.Tick += (sender, e) =>
                {
                    OnUpdate();
                };
                timer.Interval = 1000 / 60;
                timer.Start();
                glControl_Resize(glControl, EventArgs.Empty);
            }
        }

        private void glControl_Resize(object? sender, EventArgs e)
        {
            glControl.MakeCurrent();
            if (glControl.ClientSize.Height == 0)
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
            Camera.main.aspect = Math.Max(glControl.ClientSize.Width, 1) / (float)Math.Max(glControl.ClientSize.Height, 1);
        }

        private void glControl_Paint(object? sender, PaintEventArgs e)
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            if (KENGINE.Input.GetKey(KeyCode.W))
            {
                Camera.main.gameObject.transform.SetPosition(Camera.main.gameObject.transform.GetPosition() + Camera.main.gameObject.transform.GetRotation() * new Vector3(0, 0, 0.5f));
            }
            else if (KENGINE.Input.GetKey(KeyCode.S))
            {
                Camera.main.gameObject.transform.SetPosition(Camera.main.gameObject.transform.GetPosition() + Camera.main.gameObject.transform.GetRotation() * new Vector3(0, 0, -0.5f));
            }
            if (KENGINE.Input.GetKey(KeyCode.A))
            {
                Camera.main.gameObject.transform.SetPosition(Camera.main.gameObject.transform.GetPosition() + Camera.main.gameObject.transform.GetRotation() * new Vector3(0.5f, 0, 0));
            }
            else if (KENGINE.Input.GetKey(KeyCode.D))
            {
                Camera.main.gameObject.transform.SetPosition(Camera.main.gameObject.transform.GetPosition() + Camera.main.gameObject.transform.GetRotation() * new Vector3(-0.5f, 0, 0));
            }
            glControl.MakeCurrent();

            KENGINE.KENGINE.OnUpdate();
            
            glControl.SwapBuffers();
        }

        public static void GenerateSceneTree()
        {
            sceneTree.Nodes[0].Nodes.Clear();
            foreach (GameObject o in KENGINE.KENGINE.gameObjects)
            {
                TreeNode n = sceneTree.Nodes[0].Nodes.Add(o.name);
                if (o.HasChild())
                {
                    GenerateTree(o.childs, n);
                }
            }
        }
        public static void GenerateTree(List<GameObject> gameObjects, TreeNode node)
        {
            foreach (GameObject o in gameObjects)
            {
                TreeNode n = node.Nodes.Add(o.name);
                if (o.HasChild())
                {
                    GenerateTree(o.childs, n);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView2.Nodes.Clear();
            if (treeView1.SelectedNode.Name != "Scene")
            {
                GameObject o = GameObject.Find(treeView1.SelectedNode.Text);
                foreach (Component c in o.components)
                {
                    treeView2.Nodes.Add(c.GetType().Name);
                }
            }
            
        }

        private void glControl_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.KeyCode);
            KeyCode k = (KeyCode)e.KeyValue;
            if (KENGINE.Input.currentKeys.ContainsKey(k))
            {
                KENGINE.Input.currentKeys[k] = true;
            }
            else
            {
                KENGINE.Input.currentKeys.Add((KeyCode)e.KeyValue, true);
            }
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyCode k = (KeyCode)e.KeyValue;
            if (KENGINE.Input.currentKeys.ContainsKey(k))
            {
                KENGINE.Input.currentKeys[k] = false;
            }
            else
            {
                KENGINE.Input.currentKeys.Add((KeyCode)e.KeyValue, false);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (treeView1.SelectedNode.Name == "Scene")
                {
                    contextMenuStrip1.Items.Clear();
                    contextMenuStrip1.Items.Add("Create GameObject");
                }
                else
                {
                    contextMenuStrip1.Items.Clear();
                    contextMenuStrip1.Items.Add("Delete");
                }
            }
        }
    }
}
