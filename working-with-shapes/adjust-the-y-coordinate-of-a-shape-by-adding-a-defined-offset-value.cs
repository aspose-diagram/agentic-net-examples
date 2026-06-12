using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram using the built‑in constructor (lifecycle rule)
                Diagram diagram = new Diagram(inputPath);

                // Define the Y‑offset to add (in inches)
                double yOffset = 2.0;

                // Example: adjust the Y coordinate of shape with ID = 1 on the first page
                // Retrieve the shape from the page's shape collection
                Shape shape = diagram.Pages[0].Shapes.GetShape(1);

                // Add the offset to the PinY (center Y) coordinate
                shape.XForm.PinY.Value = shape.XForm.PinY.Value + yOffset;

                // Save the modified diagram (lifecycle rule)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }