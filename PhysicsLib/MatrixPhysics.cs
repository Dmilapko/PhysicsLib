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
            /// <summary>
            /// Physics points in PIXELS
            /// </summary>
            public List<PointD> points = new List<PointD>();
            /// <summary>
            /// Death points in PIXELS
            /// </summary>
            public List<PointD> deathpoints = new List<PointD>();
            public PointD position;
            public double rotation = 0;
            public List<Force> forces = new List<Force>();
            public bool alive = true;

            public MP_Object(double _MomentOfInertia, double _mass, PointD _position)
            {
                MonentOfInertia = _MomentOfInertia;
                mass = _mass;
                position = _position;
                InitializePoints();
                InitializeDeathPoints();
            }

            public virtual void InitializePoints()
            {
                List<PointD> points = new List<PointD>();
            }

            public virtual void InitializeDeathPoints()
            {
                List<PointD> deathpoints = new List<PointD>();
            }

            public override void ApplyForce(PointD pos, double angle, double force)
            {
                base.ApplyForce(pos, angle, force);
                forces.Add(new Force(pos, angle, force));
            }

            public virtual void Run()
            {

            }
        }

        public List<MP_Object> objects;
        public List<int> matrix = new List<int>();
        public double pim;
        public int fps;
        PointD BottomLeft, TopRight;

        public void SetMatrix(List<List<bool>> _matrix)
        {
            matrix.Clear();
            for (int x = 0; x < _matrix.Count; x++)
            {
                int height;
                for (height = 0; height < 1080; height++)
                {
                    if (!_matrix[x][height]) break;
                }
                matrix.Add(height);
            }
        }

        public MatrixPhysics(List<MP_Object> _objects, PointD _BottomLeft, PointD _TopRight, double pixels_in_meter, int _fps)
        {
            objects = _objects;
            pim = pixels_in_meter;
            fps = _fps;
            BottomLeft = _BottomLeft;
            TopRight = _TopRight;
        }

        public PointD GetMatrixPosition(PointD point, PointD position, double rotation)
        {
            double l = Math.Sqrt(point.X * point.X + point.Y * point.Y);
            double d = rotation + (double)Math.Atan2(point.X, point.Y);
            return new PointD(position.X + Math.Sin(d) * l, position.Y + Math.Cos(d) * l);
        }

        public bool GetMatrixState(PointD position)
        {
            if (position.ToVector().InRect(BottomLeft.ToVector(), TopRight.ToVector()))
            {
                position.X *= pim;
                position.Y *= pim;
                return (position.Y < matrix[(int)Math.Round(position.X)]);
            }
            else return false;
        }

        private void RunObjPhysics(MP_Object obj)
        {
            if (obj.alive)
            {
                obj.forces.Clear();
                obj.Run();
                //float nextx, nexty;
                obj.rotation += obj.angularvelocity / (float)fps;
                obj.position.X += obj.speed.X / (float)fps;
                obj.position.Y += obj.speed.Y / (float)fps;
                obj.rotation = obj.rotation % 360.ToRadians();
                foreach (PointD point in obj.deathpoints)
                {
                    if (GetMatrixState(GetMatrixPosition(point, obj.position, obj.rotation)))
                    { 
                        obj.alive = false;
                        break;
                    }
                }
            }
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
