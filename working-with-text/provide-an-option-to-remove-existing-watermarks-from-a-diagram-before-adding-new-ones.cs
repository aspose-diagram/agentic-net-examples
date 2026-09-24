using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Remove existing watermarks
            // ------------------------------------------------------------
            // This example assumes watermarks are text shapes that contain the word "Watermark".
            // Adjust the condition as needed for your specific watermark identification logic.
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs to delete after the iteration to avoid modifying the collection while iterating
                List<long> idsToDelete = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    string shapeText = shape.Text.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(shapeText) &&
                        shapeText.Contains("Watermark", StringComparison.OrdinalIgnoreCase))
                    {
                        idsToDelete.Add(shape.ID);
                    }
                }

                // Mark the identified shapes as deleted
                foreach (long shapeId in idsToDelete)
                {
                    Shape shapeToDelete = page.Shapes.GetShape(shapeId);
                    shapeToDelete.Del = BOOL.True; // hide/delete the shape
                }
            }

            // ------------------------------------------------------------
            // Add a new watermark to each page
            // ------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2;
                double pinY = pageHeight / 2;

                // Use the full page size for the text shape so it spans the page
                double width = pageWidth;
                double height = pageHeight;

                // Add the watermark text shape
                page.AddText(
                    pinX,               // PinX (center X)
                    pinY,               // PinY (center Y)
                    width,              // Width of the shape
                    height,             // Height of the shape
                    "CONFIDENTIAL",     // Watermark text
                    "Calibri",          // Font name
                    "#a5a5a5",          // Font color (hex)
                    0.25);              // Font size (in inches)
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
