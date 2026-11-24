using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram '{inputPath}'. Exception: {ex.Message}");
            return;
        }

        // Iterate over all pages in the diagram.
        for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
        {
            Page page = diagram.Pages[pageIndex];

            // Iterate over all shapes on the current page.
            for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
            {
                Shape shape = page.Shapes[shapeIdx];

                // Attempt to access a geometry at a specific index that may be out of range.
                // This demonstrates handling of out‑of‑range geometry index exceptions.
                int targetGeomIndex = 10; // Example index that could be invalid.

                try
                {
                    // The Geoms collection returns objects; cast to Geom.
                    Geom geom = (Geom)shape.Geoms[targetGeomIndex];

                    // Example operation: mark the geometry as deleted.
                    // This uses the BOOL enum as required by Aspose.Diagram.
                    geom.Del = BOOL.True;
                }
                catch (Exception ex)
                {
                    // Log detailed error information and continue with the next shape.
                    Console.Error.WriteLine(
                        $"[Error] Page {pageIndex}, Shape ID {shape.ID} (Name: {shape.NameU}) - " +
                        $"Failed to access geometry at index {targetGeomIndex}. " +
                        $"Exception: {ex.GetType().Name} - {ex.Message}");
                    // Optionally, you could log ex.StackTrace for deeper diagnostics.
                }
            }
        }

        try
        {
            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram to '{outputPath}'. Exception: {ex.Message}");
        }
    }
}