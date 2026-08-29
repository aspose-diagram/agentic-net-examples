using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (for demonstration purposes)
                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Store the current line color (hex string) of the shape
                string originalLineColor = shape.Line.LineColor.Value;

                // Disable line inheritance by explicitly setting the line color
                // This ensures the shape retains its original line color even after inheritance is broken
                shape.Line.LineColor.Value = originalLineColor;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved successfully with line inheritance disabled.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }