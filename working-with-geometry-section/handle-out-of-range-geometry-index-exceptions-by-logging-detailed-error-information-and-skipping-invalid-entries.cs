using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram '{inputPath}': {ex.Message}");
            return;
        }

        // Iterate through all pages
        for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
        {
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page
            for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
            {
                Shape shape = page.Shapes[shapeIndex];
                long shapeId = shape.ID;

                // Iterate through all geometries of the shape
                for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                {
                    // Cast the geometry object to Geom
                    Geom geom = (Geom)shape.Geoms[geomIndex];

                    // Example operation: attempt to delete the segment at index 2
                    int targetSegmentIndex = 2;

                    try
                    {
                        // Access the segment; this may throw if the index is out of range
                        object segmentObj = geom.CoordinateCol[targetSegmentIndex];

                        // The segment types (MoveTo, LineTo, etc.) share the Del property
                        // Use dynamic to set the property without knowing the exact type
                        // (Aspose.Diagram types expose Del as a BOOL field)
                        var delProperty = segmentObj.GetType().GetProperty("Del");
                        if (delProperty != null && delProperty.CanWrite)
                        {
                            delProperty.SetValue(segmentObj, BOOL.True);
                        }
                        else
                        {
                            Console.Error.WriteLine($"Segment at page {pageIndex}, shape ID {shapeId}, geometry {geomIndex}, index {targetSegmentIndex} does not have a deletable 'Del' property.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log detailed error information and continue processing
                        Console.Error.WriteLine($"Error processing segment at page {pageIndex}, shape ID {shapeId}, geometry {geomIndex}, segment index {targetSegmentIndex}: {ex.Message}");
                        // Skip this invalid entry and continue with the next geometry/segment
                    }
                }
            }
        }

        try
        {
            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram to '{outputPath}': {ex.Message}");
        }
    }
}