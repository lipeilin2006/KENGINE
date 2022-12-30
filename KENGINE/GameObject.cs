using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.WinForms;
using System.Diagnostics;
using OpenTK.WinForms.TestForm;

namespace KENGINE
{
    public class GameObject
    {
        public string name { get; set; }
        public List<Component> components { get; private set; } = new List<Component>();
        public List<GameObject> childs { get; private set; } = new List<GameObject>();
        public Transform transform { get; set; }
        public GameObject(string name)
        {
            this.name = name;
            transform = AddComponent<Transform>();
            KENGINE.gameObjects.Add(this);
            EditorFormMain.GenerateSceneTree();
        }
        public static GameObject Find(string name)
        {
            foreach(GameObject o in KENGINE.gameObjects)
            {
                if(o.name == name) 
                    return o;
            }
            return null;
        }
        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            component.SetGameObject(this);
            components.Add(component);
            return component;
        }
        public T GetComponent<T>()where T : Component
        {
            for(int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                {
                    return (T)components[i];
                }
            }
            return null;
        }
        public T[] GetComponents<T>() where T : Component
        {
            List<T> comps = new List<T>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                {
                    comps.Add((T)components[i]);
                }
            }
            return comps.ToArray();
        }
        public bool HasChild()
        {
            return childs.Count > 0;
        }
        public void RemoveComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                {
                    components.RemoveAt(i);
                }
            }
        }
    }
    public abstract class Component
    {
        public GameObject gameObject { get; private set; }
        public void SetGameObject(GameObject o)
        {
            gameObject = o;
        }
        public virtual void Start() { }
        public virtual void Update() { }
    }
    public class MeshReneder : Component
    {
        public Mesh? mesh { get; set; }
        public override void Update()
        {
            if (mesh != null)
            {
                Transform transform = gameObject.GetComponent<Transform>();
                mesh.Draw(transform.GetPosition(), transform.GetRotation(), transform.sizeDelta);
            }
        }
    }
    public class Mesh
    {
        public List<Vector3> v = new List<Vector3>();
        public List<Vector3> vn = new List<Vector3>();
        public List<Vector3> vt = new List<Vector3>();
        public List<List<Vector3>> f = new List<List<Vector3>>();
        public virtual void Draw(Vector3 position,Quaternion rotation,SizeDelta size)
        {

        }
    }
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

    public class Camera : Component
    {
        public static Camera main { get; set; }
        public static short cameraCount{ get; private set; }
        public Vector3 up { get; set; } = new Vector3(0, 1, 0);
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
                Quaternion rotation = t.GetRotation();
                lookat = Matrix4.LookAt(position, position + rotation * new Vector3(0, 0, 1), up);
                GL.LoadMatrix(ref lookat);
            }
        }
        public void Activate()
        {
            main = this;
        }

        // Get the projection matrix using the same method we have used up until this point
    }
}
