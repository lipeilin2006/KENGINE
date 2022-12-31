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
}
