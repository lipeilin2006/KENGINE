using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class Mesh
    {
        public List<Vector3> v = new List<Vector3>();
        public List<Vector3> vn = new List<Vector3>();
        public List<Vector3> vt = new List<Vector3>();
        public List<List<Vector3>> f = new List<List<Vector3>>();
        public virtual void Draw(Vector3 position, Quaternion rotation, SizeDelta size)
        {

        }
    }
}
