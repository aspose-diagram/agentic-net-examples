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

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Select the page that contains the target shape (first page in this example)
                Page page = diagram.Pages[0];

                // Identify the shape to which the comment will be added.
                // Replace the shapeId with the actual ID of your target shape.
                int shapeId = 5;
                Shape targetShape = page.Shapes.GetShape(shapeId);

                // Add a comment associated with the shape
                page.AddComment(targetShape, "Review this shape for accuracy.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }