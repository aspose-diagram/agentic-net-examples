using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Identify the shape to modify.
                // Replace the shape ID with the actual ID of the target shape.
                long shapeId = 1; // example ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Convert 2.5 centimeters to inches (1 cm = 0.393700787 inches)
                double heightInInches = 2.5 * 0.393700787;

                // Set the new height using the SetHeight method
                shape.SetHeight(heightInInches);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape ID {shapeId} height set to 2.5 cm and saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }