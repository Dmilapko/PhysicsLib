using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MonoHelper;

namespace Physics
{
    public class Force
    {
        public PointD pos; public double angle; public double force;

        public Force(PointD _pos, double _angle, double _force)
        {
            pos = _pos;
            angle = _angle;
            force = _force;
        }
    }

    public class PhysicsObject
    {
        //public delegate void MOIF();
        //public MOIF MonentOfInertia;
        public double MonentOfInertia;
        /// <summary>
        /// Angular speed in degrees
        /// </summary>
        public double angularvelocity = 0;
        /// <summary>
        /// Mass of object in kilos
        /// </summary>
        public double mass;
        /// <summary>
        /// Vector of speed
        /// </summary>
        public PointD speed = new PointD(0, 0);

        public PhysicsObject()
        {

        }

        public PhysicsObject(float _MomentOfInertia, float _mass)
        {
            MonentOfInertia = _MomentOfInertia;
            mass = _mass;
        }

        /// <summary>
        /// Apply force in N(Newtons)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="angle">Angle in radians</param>
        /// <param name="force"></param>
        public virtual void ApplyForce(PointD pos, double angle, double force)
        {
            //angle - Math.Atan2(pos.X, pos.Y) angle between ANGLE and tangent
            double torque = (force * Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y) * Math.Sin(angle - Math.Atan2(pos.X, pos.Y)));
            angularvelocity += (torque / MonentOfInertia);

            speed.X += (float)(force * Math.Sin(angle) / mass);
            speed.Y += (float)(force * Math.Cos(angle) / mass);
        }
    }
}
