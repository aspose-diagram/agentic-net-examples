using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path
            Console.Write("Enter the path to the Visio file to process: ");
            string inputPath = Console.ReadLine();

            // Output Visio file path
            Console.Write("Enter the path where the modified file should be saved: ");
            string outputPath = Console.ReadLine();

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and all shapes, applying DistanceFromGround = 15 points
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the ThreeDFormat object exists (it is always instantiated)
                    shape.ThreeDFormat.DistanceFromGround.Value = 15;
                }
            }

            // Save the modified diagram (preserving the original format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("All shapes have been updated with DistanceFromGround = 15 points.");
        }
    }