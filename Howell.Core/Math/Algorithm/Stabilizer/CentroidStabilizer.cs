using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm.Stabilizer
{
    /// <summary>
    /// 质心稳定器
    /// </summary>
    public class CentroidStabilizer
    {
        private Double m_Threshold;
        private Double m_Step;
        private Double m_X;		//质心坐标
        private Double m_Y;
        private Double m_VelocityX;	//质心速度速度
        private Double m_VelocityY;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CentroidStabilizer()
            : this(0.05,0.01)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threshold">加速阀值</param>
        /// <param name="step">速度步进</param>
        public CentroidStabilizer(Double threshold, Double step)
        {
            m_Threshold = threshold;
            m_Step = step;
        }
        /// <summary>
        /// 加速阀值
        /// </summary>
        public Double Threshold
        {
            get
            {
                return m_Threshold;
            }
        }
        /// <summary>
        /// 加速阀值
        /// </summary>
        public Double Step
        {
            get
            {
                return m_Step;
            }
        }
        /// <summary>
        /// 稳定质心
        /// </summary>
        /// <param name="newPoint">新目标质心</param>
        /// <returns>返回稳定后的质心</returns>
        public PointD Stabilize(PointD newPoint)
        {
            if (m_X == 0 && m_Y == 0)
            {
                m_X = newPoint.X;
                m_Y = newPoint.Y;
                return newPoint;
            }
            else
            {
                correct0(ref m_X, newPoint.X, ref m_VelocityX);
                correct0(ref m_Y, newPoint.Y, ref m_VelocityY);
                return new PointD(m_X, m_Y);
            }
        }
        /// <summary>
        /// 复位数据
        /// </summary>
        public void Reset()
        {
            m_VelocityX = 0;
            m_VelocityY = 0;
            m_X = 0;
            m_Y = 0;
        }
        /// <summary>
        /// 矫正速度 - 低加速度
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        /// <param name="velocity"></param>
        void correct0(ref Double previous, Double current, ref Double velocity)
	    {
            Double deta = current - previous;
            if (deta > Threshold) velocity += Step;			//加速度连续变化
            if (deta < -Threshold) velocity -= Step;

            Double halfThreshold = Threshold / 2;
            if (deta > -halfThreshold && deta < halfThreshold)
            {
                velocity -= velocity / 8;	//减速
            }
            previous += velocity;
	    }
    }
}
