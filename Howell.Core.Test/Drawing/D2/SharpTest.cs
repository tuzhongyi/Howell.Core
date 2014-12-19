using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howell.Drawing.D2
{
    class SharpTest 
    {
        public static void Test()
        {
            CircleDTest();
            CircleITest();
            EllipseDTest();
            EllipseITest();
            PointDTest();
            PointITest();
            LineDTest();
            LineITest();
            PolygonDTest();
            PolygonITest();
            PolylineDTest();
            PolylineITest();
            RectangleDTest();
            RectangleITest();
            SizeDTest();
            SizeITest();
            PointSetDTest();
            PointSetITest();
        }
        public static void CircleDTest()
        {
            Console.WriteLine("++++++++++++++++++++CircleD++++++++++++++++++++++");
            CircleD c1 = new CircleD(102.01, 2990.19945, 102031.1);
            Console.WriteLine("Circle 01:{0}", c1.ToString());
            try
            {
                CircleD c2 = CircleD.Parse(c1.ToString());
                Console.WriteLine("Circle 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch(Exception ex)
            {
                Console.WriteLine("CircleD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void CircleITest()
        {
            Console.WriteLine("++++++++++++++++++++CircleI++++++++++++++++++++++");
            CircleI c1 = new CircleI(102, 2990, 1020);
            Console.WriteLine("Circle 01:{0}", c1.ToString());
            try
            {
                CircleI c2 = CircleI.Parse(c1.ToString());
                Console.WriteLine("Circle 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CircleI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void EllipseDTest()
        {
            Console.WriteLine("++++++++++++++++++++EllipseD++++++++++++++++++++++");
            EllipseD c1 = new EllipseD(102.01, 2990.19945, 102031.1,1002.3);
            Console.WriteLine("Ellipse 01:{0}", c1.ToString());
            try
            {
                EllipseD c2 = EllipseD.Parse(c1.ToString());
                Console.WriteLine("Ellipse 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EllipseD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void EllipseITest()
        {
            Console.WriteLine("++++++++++++++++++++EllipseI++++++++++++++++++++++");
            EllipseI c1 = new EllipseI(102, 2990, 1020,991);
            Console.WriteLine("Ellipse 01:{0}", c1.ToString());
            try
            {
                EllipseI c2 = EllipseI.Parse(c1.ToString());
                Console.WriteLine("Ellipse 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("EllipseI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PointDTest()
        {
            Console.WriteLine("++++++++++++++++++++PointD++++++++++++++++++++++");
            PointD c1 = new PointD(102.01, 2990.19945);
            Console.WriteLine("Point 01:{0}", c1.ToString());
            try
            {
                PointD c2 = PointD.Parse(c1.ToString());
                Console.WriteLine("Point 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PointD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PointITest()
        {
            Console.WriteLine("++++++++++++++++++++PointI++++++++++++++++++++++");
            PointI c1 = new PointI(102, 2990);
            Console.WriteLine("Point 01:{0}", c1.ToString());
            try
            {
                PointI c2 = PointI.Parse(c1.ToString());
                Console.WriteLine("Point 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PointI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void LineDTest()
        {
            Console.WriteLine("++++++++++++++++++++LineD++++++++++++++++++++++");
            LineD c1 = new LineD(new PointD(12.1, 9.9), new PointD(0.123, 0.38856));
            Console.WriteLine("Line 01:{0}", c1.ToString());
            try
            {
                LineD c2 = LineD.Parse(c1.ToString());
                Console.WriteLine("Line 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("LineD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void LineITest()
        {
            Console.WriteLine("++++++++++++++++++++LineI++++++++++++++++++++++");
            LineI c1 = new LineI(new PointI(0, 1), new PointI(23, 0));
            Console.WriteLine("Line 01:{0}", c1.ToString());
            try
            {
                LineI c2 = LineI.Parse(c1.ToString());
                Console.WriteLine("Line 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("LineI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PolygonDTest()
        {

            Console.WriteLine("++++++++++++++++++++PolygonD++++++++++++++++++++++");
            PolygonD c1 = new PolygonD(new PointD [] { new PointD(0.23, 1.68), new PointD(23, 0.879), new PointD(11.87, 0.1879) });
            Console.WriteLine("Polygon 01:{0}", c1.ToString());
            try
            {
                PolygonD c2 = PolygonD.Parse(c1.ToString());
                Console.WriteLine("Polygon 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PolygonD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PolygonITest()
        {
            Console.WriteLine("++++++++++++++++++++PolygonI++++++++++++++++++++++");
            PolygonI c1 = new PolygonI(new PointI[] { new PointI(23, 1), new PointI(23, 879), new PointI(11, 1879) });
            Console.WriteLine("Polygon 01:{0}", c1.ToString());
            try
            {
                PolygonI c2 = PolygonI.Parse(c1.ToString());
                Console.WriteLine("Polygon 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PolygonI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PolylineDTest()
        {
            Console.WriteLine("++++++++++++++++++++PolylineD++++++++++++++++++++++");
            PolylineD c1 = new PolylineD(new PointD[] { new PointD(23.01, 1.45), new PointD(23, 879), new PointD(11.48, 1879) });
            Console.WriteLine("Polyline 01:{0}", c1.ToString());
            try
            {
                PolylineD c2 = PolylineD.Parse(c1.ToString());
                Console.WriteLine("Polyline 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PolylineD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PolylineITest()
        {
            Console.WriteLine("++++++++++++++++++++PolylineI++++++++++++++++++++++");
            PolylineI c1 = new PolylineI(new PointI[] { new PointI(23, 5), new PointI(23, 879), new PointI(11, 1879) });
            Console.WriteLine("Polyline 01:{0}", c1.ToString());
            try
            {
                PolylineI c2 = PolylineI.Parse(c1.ToString());
                Console.WriteLine("Polyline 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PolylineI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void RectangleDTest()
        {
            Console.WriteLine("++++++++++++++++++++RectangleD++++++++++++++++++++++");
            RectangleD c1 = new RectangleD(23.01, 1.451, 1.48, 1879);
            Console.WriteLine("Rectangle 01:{0}", c1.ToString());
            try
            {
                RectangleD c2 = RectangleD.Parse(c1.ToString());
                Console.WriteLine("Rectangle 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("RectangleD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void RectangleITest()
        {
            Console.WriteLine("++++++++++++++++++++RectangleI++++++++++++++++++++++");
            RectangleI c1 = new RectangleI(23, 51, 8, 879);
            Console.WriteLine("Rectangle 01:{0}", c1.ToString());
            try
            {
                RectangleI c2 = RectangleI.Parse(c1.ToString());
                Console.WriteLine("Rectangle 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("RectangleI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void SizeDTest()
        {
            Console.WriteLine("++++++++++++++++++++SizeD++++++++++++++++++++++");
            SizeD c1 = new SizeD(102.01, 2990.19945);
            Console.WriteLine("Size 01:{0}", c1.ToString());
            try
            {
                SizeD c2 = SizeD.Parse(c1.ToString());
                Console.WriteLine("Size 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SizeD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void SizeITest()
        {
            Console.WriteLine("++++++++++++++++++++SizeI++++++++++++++++++++++");
            SizeI c1 = new SizeI(102, 2990);
            Console.WriteLine("Size 01:{0}", c1.ToString());
            try
            {
                SizeI c2 = SizeI.Parse(c1.ToString());
                Console.WriteLine("Size 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SizeI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PointSetDTest()
        {
            Console.WriteLine("++++++++++++++++++++PointSetD++++++++++++++++++++++");
            PointSetD c1 = new PointSetD(new PointD[] { new PointD(23.01, 1.45), new PointD(23, 879), new PointD(11.48, 1879) });
            Console.WriteLine("PointSet 01:{0}", c1.ToString());
            try
            {
                PointSetD c2 = PointSetD.Parse(c1.ToString());
                Console.WriteLine("PointSet 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PointSetD.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
        public static void PointSetITest()
        {
            Console.WriteLine("++++++++++++++++++++PointSetI++++++++++++++++++++++");
            PointSetI c1 = new PointSetI(new PointI[] { new PointI(231, 15), new PointI(23, 879), new PointI(18, 1879) });
            Console.WriteLine("PointSet 01:{0}", c1.ToString());
            try
            {
                PointSetI c2 = PointSetI.Parse(c1.ToString());
                Console.WriteLine("PointSet 02:{0}", c2.ToString());
                if (c2 == c1)
                    Console.WriteLine("01 == 02");
                else
                    Console.WriteLine("01 != 02");
            }
            catch (Exception ex)
            {
                Console.WriteLine("PointSetI.Parse failed, {0}.", ex.Message);
            }
            Console.WriteLine("===============================================");
        }
    }
}
