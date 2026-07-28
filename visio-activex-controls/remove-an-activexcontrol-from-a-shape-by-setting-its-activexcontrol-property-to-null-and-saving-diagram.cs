using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and collect shapes that contain ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                // List to hold shapes that need to be removed
                List<Shape> shapesToRemove = new List<Shape>();

                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has an ActiveX control (property is read‑only, so we cannot set it to null)
                    if (shape.ActiveXControl != null)
                    {
                        // Mark the shape for removal
                        shapesToRemove.Add(shape);
                    }
                }

                // Remove the marked shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}