using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // ID of the shape whose X coordinate will be changed
                // Replace with the actual shape ID you want to modify
                long shapeId = 1;

                // New X coordinate value (in inches)
                double newPinX = 5.0;

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Update only the X coordinate, preserving the existing Y coordinate
                shape.XForm.PinX.Value = newPinX;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }