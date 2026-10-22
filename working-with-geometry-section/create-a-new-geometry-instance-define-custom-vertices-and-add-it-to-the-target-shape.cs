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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page does not contain any shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Create a new geometry (Geom) object.
                Geom newGeom = new Geom();

                // Define custom vertices for the geometry.
                // MoveTo – starting point (0,0)
                MoveTo moveTo = new MoveTo();
                moveTo.X.Value = 0.0;
                moveTo.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(moveTo);

                // LineTo – first edge (2,0)
                LineTo line1 = new LineTo();
                line1.X.Value = 2.0;
                line1.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(line1);

                // LineTo – second edge (2,2)
                LineTo line2 = new LineTo();
                line2.X.Value = 2.0;
                line2.Y.Value = 2.0;
                newGeom.CoordinateCol.Add(line2);

                // LineTo – third edge (0,2)
                LineTo line3 = new LineTo();
                line3.X.Value = 0.0;
                line3.Y.Value = 2.0;
                newGeom.CoordinateCol.Add(line3);

                // LineTo – close the shape back to the start (0,0)
                LineTo line4 = new LineTo();
                line4.X.Value = 0.0;
                line4.Y.Value = 0.0;
                newGeom.CoordinateCol.Add(line4);

                // Add the newly created geometry to the shape's Geoms collection.
                shape.Geoms.Add(newGeom);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}