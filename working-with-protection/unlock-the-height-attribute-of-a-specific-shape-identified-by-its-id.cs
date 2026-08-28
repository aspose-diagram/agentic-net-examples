using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Specify the ID of the shape whose height attribute should be unlocked
                long shapeId = 123; // TODO: replace with the actual shape ID

                // Retrieve the shape from the first page (adjust page index if needed)
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Unlock the height attribute by disabling the height lock protection
                shape.Protection.LockHeight.Value = BOOL.False;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }