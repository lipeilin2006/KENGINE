using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
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
}
