using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using KENGINE;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace KENGINE_Editor
{
	public partial class EditorFormMain : Form
	{
        private Timer timer = null!;
        static TreeView sceneTree;

        float camRotX, camRotY = 0;
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

            GameObject viewer = new GameObject("viewer");
            viewer.transform.position.z = -5;
            viewer.transform.position.y = 0;
            viewer.transform.rotation.y = 0;

            //Add GameObject
            GameObject cam = new GameObject("Camera");
            Camera.main = cam.AddComponent<Camera>();

            viewer.transform.Child(cam.transform);

            GameObject o1 = new GameObject("Cube1");
            o1.transform.position.x = 0;
            MeshRender o1m = o1.AddComponent<MeshRender>();
            o1m.mesh = new CubeMesh();
            
            GameObject o2 = new GameObject("Cube2");
            o2.transform.localPosition.x = 2;
            MeshRender o2m = o2.AddComponent<MeshRender>();
            o2m.mesh = new CubeMesh();

            o1.transform.Child(o2.transform);

            GameObject f = new GameObject("Floor");
            f.transform.position.x = 0;
            f.transform.position.y = -2;
            f.transform.sizeDelta.x = 10;
            f.transform.sizeDelta.z = 10;
            MeshRender fm = f.AddComponent<MeshRender>();
            fm.mesh = new CubeMesh();

            GenerateSceneTree();
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
            Transform viewer = GameObject.Find("viewer").transform;
            camRotX = Camera.main.gameObject.transform.localRotation.x;
            camRotY = viewer.localRotation.y;
            if (Input.GetKey(KeyCode.W))
            {
                viewer.SetPosition(viewer.position.ToVector3() + viewer.forward * 0.5f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                viewer.SetPosition(viewer.position.ToVector3() - viewer.forward * 0.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                viewer.SetPosition(viewer.position.ToVector3() - viewer.right * 0.5f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                viewer.SetPosition(viewer.position.ToVector3() + viewer.right * 0.5f);
            }
            if (Input.GetKey(KeyCode.E))
            {
                viewer.SetPosition(viewer.position.ToVector3() + viewer.up * 0.5f);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                viewer.SetPosition(viewer.position.ToVector3() - viewer.up * 0.5f);
            }


            if (Input.GetMouseButton(KENGINE.MouseButton.Right))
            {
                camRotY -= Input.GetMouseDeltaX() / 50;
                camRotX += Input.GetMouseDeltaY() / 50;
                viewer.localRotation.y = camRotY;
                Camera.main.gameObject.transform.localRotation.x = camRotX;
            }

            //General Update
            glControl.MakeCurrent();

            KENGINE.KENGINE.OnUpdate();
            
            glControl.SwapBuffers();
        }

        public static void GenerateSceneTree()
        {
            sceneTree.Nodes[0].Nodes.Clear();
            foreach (GameObject o in KENGINE.KENGINE.gameObjects)
            {
                if (o.transform.parent == null)
                {
                    TreeNode n = sceneTree.Nodes[0].Nodes.Add(o.name);
                    if (o.transform.HasChild())
                    {
                        GenerateTree(o.transform.childs, n);
                    }
                }
            }
        }
        public static void GenerateTree(List<Transform> transforms, TreeNode node)
        {
            foreach (Transform t in transforms)
            {
                TreeNode n = node.Nodes.Add(t.gameObject.name);
                if (t.HasChild())
                {
                    GenerateTree(t.childs, n);
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
            if (tabControl1.TabIndex == 1)
            {
                Debug.WriteLine(e.KeyCode);
                KeyCode k = (KeyCode)e.KeyValue;
                if (Input.currentKeys.ContainsKey(k))
                {
                    Input.currentKeys[k] = true;
                }
                else
                {
                    Input.currentKeys.Add((KeyCode)e.KeyValue, true);
                }
            }
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (tabControl1.TabIndex == 1)
            {
                KeyCode k = (KeyCode)e.KeyValue;
                if (Input.currentKeys.ContainsKey(k))
                {
                    Input.currentKeys[k] = false;
                }
                else
                {
                    Input.currentKeys.Add((KeyCode)e.KeyValue, false);
                }
            }
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            tabControl1.Focus();
            KENGINE.MouseButton mb = (KENGINE.MouseButton)e.Button;
            if (Input.currentMouse.ContainsKey((KENGINE.MouseButton)e.Button))
            {
                Input.currentMouse[mb] = true;
            }
            else
            {
                Input.currentMouse.Add(mb, true);
            }
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            KENGINE.MouseButton mb = (KENGINE.MouseButton)e.Button;
            if (Input.currentMouse.ContainsKey((KENGINE.MouseButton)e.Button))
            {
                Input.currentMouse[mb] = false;
            }
            else
            {
                Input.currentMouse.Add(mb, false);
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

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            Input.mouseX = e.X;
            Input.mouseY = e.Y;
        }
    }
}
