using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that contain ActiveX controls
                List<long> activeXShapeIds = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // ActiveXControl is read‑only; we only need to know if it exists
                    if (shape.ActiveXControl != null)
                    {
                        activeXShapeIds.Add(shape.ID);
                    }
                }

                // Remove shapes that host ActiveX controls to disable them
                foreach (long shapeId in activeXShapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    // Remove the shape from the page (deleting the ActiveX control)
                    page.Shapes.Remove(shape);
                }
            }

            // Export the sanitized diagram to PDF
            string outputPath = "output.pdf";
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set a fallback font for any missing fonts during PDF export
                DefaultFont = "Arial"
            };
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}