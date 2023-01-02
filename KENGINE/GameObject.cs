namespace KENGINE
{
    public class GameObject
    {
        public string name { get; set; }
        public List<Component> components { get; private set; } = new List<Component>();
        public Transform transform { get; set; }
        public GameObject(string name)
        {
            this.name = name;
            transform = AddComponent<Transform>();
            KENGINE.gameObjects.Add(this);
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
