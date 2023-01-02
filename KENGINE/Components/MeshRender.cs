using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class MeshRender : Component
    {
        public Mesh? mesh { get; set; }
        public override void Update()
        {
            if (mesh != null)
            {
                Transform transform = gameObject.GetComponent<Transform>();
                mesh.Draw(transform.position.ToVector3(), transform.rotation.ToQuaternion(), transform.sizeDelta);
            }
        }
    }
}
