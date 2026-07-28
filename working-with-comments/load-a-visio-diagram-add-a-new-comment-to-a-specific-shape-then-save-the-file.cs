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

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Identify the shape to which the comment will be added.
                // Here we assume the shape ID is known (e.g., 1). Adjust as needed.
                int shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Add a comment associated with the shape
                string commentText = "Review this shape - added via Aspose.Diagram.";
                page.AddComment(shape, commentText);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Comment added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }