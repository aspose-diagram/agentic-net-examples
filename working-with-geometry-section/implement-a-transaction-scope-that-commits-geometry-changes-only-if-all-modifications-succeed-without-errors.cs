using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command line or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Wrap the whole operation in a try/catch to handle any unexpected errors
        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Begin a logical transaction: all geometry changes will be applied here
            // If any step throws, the catch block will prevent saving the diagram
            // (i.e., changes are not committed)
            // --------------------------------------------------------------
            // Example modification: add a simple line segment to the first shape
            // --------------------------------------------------------------

            // Ensure there is at least one page and one shape to work with
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
                throw new Exception("The first page contains no shapes.");

            // Retrieve the first shape
            Shape shape = firstPage.Shapes[0];

            // Ensure the shape has at least one geometry section
            if (shape.Geoms.Count == 0)
                throw new Exception("The shape has no geometry sections.");

            // Access the first geometry (explicit cast as required)
            Geom geom = (Geom)shape.Geoms[0];

            // --------------------------------------------------------------
            // 1. Optionally delete an existing segment (demonstrates BOOL usage)
            // --------------------------------------------------------------
            if (geom.CoordinateCol.Count > 0)
            {
                // Mark the first segment for deletion
                // The segment type can be MoveTo, LineTo, etc.; we treat it generically
                // by accessing the base class (Cell) and setting its Del flag.
                // Since the collection stores specific segment objects, we cast to
                // the appropriate type based on its actual runtime type.
                var firstSegment = geom.CoordinateCol[0];
                // All segment types inherit from Cell which has the Del property
                // (using dynamic cast to access the property safely)
                if (firstSegment is MoveTo moveTo)
                {
                    moveTo.Del = BOOL.True;
                }
                else if (firstSegment is LineTo lineTo)
                {
                    lineTo.Del = BOOL.True;
                }
                // Add handling for other segment types if needed
            }

            // --------------------------------------------------------------
            // 2. Append a new MoveTo (starting point) and a LineTo (ending point)
            // --------------------------------------------------------------

            // Create a MoveTo segment at (0,0)
            MoveTo start = new MoveTo();
            start.X.Value = 0.0;
            start.Y.Value = 0.0;
            geom.CoordinateCol.Add(start);

            // Create a LineTo segment to (2,2)
            LineTo line = new LineTo();
            line.X.Value = 2.0;
            line.Y.Value = 2.0;
            geom.CoordinateCol.Add(line);

            // --------------------------------------------------------------
            // All modifications succeeded; commit by saving the diagram
            // --------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors; the diagram is not saved, so changes are not committed
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}