using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTaskTriangle
{
    class Program
    {
        // Accuracy for the floating point comparisons.
        static readonly double Delta = 1E-6;

        static void Main()
        {
            try
            {
                // Constructs an instace Triangle class from input.
                // If not all coordinates are numbers throws an exception.
                Triangle triangle = InputTriangle();

                // If at least on side equals 0, it is not a proper triangle.
                if (triangle.IsProper())
                {
                    // Outputting side lengths of the triangle.
                    Console.WriteLine("Length of AB is: {0:N6}", triangle.GetAB());
                    Console.WriteLine("Length of BC is: {0:N6}", triangle.GetBC());
                    Console.WriteLine("Length of AC is: {0:N6}", triangle.GetAC());
                    Console.WriteLine();

                    // Outputting possible types of the triangle
                    Console.WriteLine("Triangle {0} 'Equilateral'", triangle.IsEquilateral() ? "IS" : "IS NOT");
                    Console.WriteLine("Triangle {0} 'Isosceles'", triangle.IsIsosceles() ? "IS" : "IS NOT");
                    Console.WriteLine("Triangle {0} 'Right'", triangle.IsRight() ? "IS" : "IS NOT");
                    Console.WriteLine();

                    // Outputting the perimeter of the triangle
                    double perimeter = triangle.GetPerimeter();
                    Console.WriteLine("Perimeter: {0:N6}", perimeter);
                    Console.WriteLine();

                    // Truncated perimeter is needed to avoid loss of accuracy
                    // E.g. when perimeter equals 11,999999 the number 12 must be written on console.
                    long truncatedPerimeter = (long)(perimeter + Delta);

                    // Outputting even numbers in a range
                    Console.WriteLine("Even numbers in a range from 0 to triangle perimeter:");
                    for (long i = 0; i <= truncatedPerimeter; i += 2)
                    {
                        Console.WriteLine(i);
                    }
                }
                else
                {
                    Console.Write("Is not a proper triangle! ");
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static Triangle InputTriangle()
        {
            Vertex vertexA, vertexB, vertexC;
            string[] input = new string[6];

            // Reading string input.
            Console.WriteLine("Input x-coordinate of point A:");
            input[0] = Console.ReadLine();
            Console.WriteLine("Input y-coordinate of point A:");
            input[1] = Console.ReadLine();
            Console.WriteLine("Input x-coordinate of point B:");
            input[2] = Console.ReadLine();
            Console.WriteLine("Input y-coordinate of point B:");
            input[3] = Console.ReadLine();
            Console.WriteLine("Input x-coordinate of point C:");
            input[4] = Console.ReadLine();
            Console.WriteLine("Input y-coordinate of point C:");
            input[5] = Console.ReadLine();

            // Converting string input to double type.
            try
            {
                vertexA.x = Convert.ToDouble(input[0]);
                vertexA.y = Convert.ToDouble(input[1]);
                vertexB.x = Convert.ToDouble(input[2]);
                vertexB.y = Convert.ToDouble(input[3]);
                vertexC.x = Convert.ToDouble(input[4]);
                vertexC.y = Convert.ToDouble(input[5]);
                return new Triangle(vertexA, vertexB, vertexC);
            }
            catch
            {
                throw new FormatException("Coordinates must be numbers! ");
            }

        }

        class Triangle
        {
            // Verices that from the triangle.
            private readonly Vertex VertexA;
            private readonly Vertex VertexB;
            private readonly Vertex VertexC;

            // Lengths of the triangle sides
            private readonly double AC;
            private readonly double AB;
            private readonly double BC;

            // Constructs an instance odf the Triangle for tree given verteces.
            public Triangle(Vertex vertexA, Vertex vertexB, Vertex vertexC)
            {
                this.VertexA = vertexA;
                this.VertexB = vertexB;
                this.VertexC = vertexC;
                this.AB = Math.Sqrt(Math.Pow(vertexA.x - vertexB.x, 2) + Math.Pow(vertexA.y - vertexB.y, 2));
                this.BC = Math.Sqrt(Math.Pow(vertexC.x - vertexB.x, 2) + Math.Pow(vertexC.y - vertexB.y, 2));
                this.AC = Math.Sqrt(Math.Pow(vertexA.x - vertexC.x, 2) + Math.Pow(vertexA.y - vertexC.y, 2));

            }

            // Returns the length of the side AB.
            public double GetAB()
            {
                return this.AB;
            }

            // Returns the length of the side BC.
            public double GetBC()
            {
                return this.BC;
            }

            // Returns the length of the side AC.
            public double GetAC()
            {
                return this.AC;
            }

            // Checks whether the instance is a proper triangle.
            public bool IsProper()
            {
                return
                    (AB > Delta) &&
                    (AC > Delta) &&
                    (BC > Delta);
            }

            // Checks whether the instance is an equilateral triangle.
            public bool IsEquilateral()
            {
                return
                    (Math.Abs(AB - BC) < Delta) &&
                    (Math.Abs(AB - AC) < Delta);
            }

            // Checks whether the instance is an isosceles triangle.
            public bool IsIsosceles()
            {
                return
                    (Math.Abs(AB - BC) < Delta) ||
                    (Math.Abs(AB - AC) < Delta) ||
                    (Math.Abs(AC - BC) < Delta);
            }

            // Checks whether the instance is a right triangle.
            public bool IsRight()
            {
                return
                    (Math.Abs(AB * AB + BC * BC - AC * AC) < Delta) ||
                    (Math.Abs(AB * AB + AC * AC - BC * BC) < Delta) ||
                    (Math.Abs(AC * AC + BC * BC - AB * AB) < Delta);
            }

            // Returns the perimeter of the triangle
            public double GetPerimeter()
            {
                return AB + BC + AC;
            }
        }

        // Represents a point in 2D space for triangle verteces.
        struct Vertex
        {
            public double x;
            public double y;
        }
    }

}
