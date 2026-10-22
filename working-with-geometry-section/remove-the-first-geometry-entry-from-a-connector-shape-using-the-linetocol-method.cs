using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connector shapes are 1‑D shapes (OneD returns a native bool).
                        if (shape.OneD)
                        {
                            // Ensure the shape has at least one geometry.
                            if (shape.Geoms.Count > 0)
                            {
                                // Get the first geometry.
                                Geom firstGeom = shape.Geoms[0];

                                // Access the LineTo collection within the geometry's coordinate section.
                                var lineToCol = firstGeom.CoordinateCol.LineToCol;

                                // Remove the first LineTo entry if it exists.
                                if (lineToCol.Count > 0)
                                {
                                    lineToCol.RemoveAt(0);
                                    Console.WriteLine($"Removed first LineTo entry from connector shape ID {shape.ID} on page \"{page.Name}\".");
                                }
                                else
                                {
                                    Console.WriteLine($"Connector shape ID {shape.ID} on page \"{page.Name}\" has no LineTo entries to remove.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Connector shape ID {shape.ID} on page \"{page.Name}\" has no geometries.");
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}