using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (OneD == true)
                    if (shape.OneD)
                    {
                        // Ensure the shape has at least one geometry section
                        if (shape.Geoms != null && shape.Geoms.Count > 0)
                        {
                            // Access the first geometry section
                            Geom geom = shape.Geoms[0];

                            // Ensure the geometry has a CoordinateCol with LineTo entries
                            if (geom != null && geom.CoordinateCol != null && geom.CoordinateCol.LineToCol != null && geom.CoordinateCol.LineToCol.Count > 0)
                            {
                                // Remove the first LineTo entry
                                geom.CoordinateCol.LineToCol.RemoveAt(0);
                                Console.WriteLine($"Removed first LineTo entry from connector shape ID {shape.ID} on page ID {page.ID}.");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}