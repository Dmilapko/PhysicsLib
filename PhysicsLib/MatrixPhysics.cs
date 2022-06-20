using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MonoHelper;

namespace Physics
{

    public class MatrixPhysics
    {
        public class MP_Object : PhysicsObject
        {
            public List<PointF> points = new List<PointF>();
            public float x, y, realx, realy;
            public float dir = 0;
            public List<Force> forces = new List<Force>();

            public MP_Object(float _MomentOfInertia, float _mass, PointF _position, List<PointF> _points)
            {
                MonentOfInertia = _MomentOfInertia;
                mass = _mass;
                x = _position.X;
                y = _position.Y;
                points = _points;
            }

            public override void ApplyForce(PointF pos, float angle, float force)
            {
                base.ApplyForce(pos, angle, force);
                forces.Add(new Force(pos, angle, force));
            }

            public virtual void Run()
            {

            }
        }

        public List<MP_Object> objects;
        public List<List<bool>> matrix;
        public float pim;
        public int fps;

        public MatrixPhysics(List<MP_Object> _objects, List<List<bool>> _matrix, float pixels_in_meter, int _fps)
        {
            objects = _objects;
            matrix = _matrix;
            pim = pixels_in_meter;
            fps = _fps;
        }

        private void RunObjPhysics(MP_Object obj)
        {
            obj.forces.Clear();
            obj.Run();
            //float nextx, nexty;
            obj.dir += obj.angularvelocity / (float)fps;
            obj.x += obj.speed.X * pim / (float)fps;
            obj.y += obj.speed.Y * pim / (float)fps;
            obj.dir = obj.dir % 360.ToRadians();
        }

        public void Run()
        {
            foreach (MP_Object obj in objects)
            {
                RunObjPhysics(obj);
            }
        }
    }
}
