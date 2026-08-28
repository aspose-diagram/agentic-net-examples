using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Create a new master shape (a simple rectangle) and configure it
            // -----------------------------------------------------------------
            Master master = new Master
            {
                // Assign a unique numeric ID based on current masters count
                ID = diagram.Masters.Count + 1,
                // Human‑readable name used when adding shapes from this master
                Name = "CustomRectangle",
                // Unique identifiers required by the API
                UniqueID = Guid.NewGuid(),
                BaseID = Guid.NewGuid(),
                // Visibility and matching settings
                Hidden = BOOL.False,
                MatchByName = BOOL.True,
                IconUpdate = BOOL.True
            };

            // Create the rectangle shape that will belong to the master
            Shape masterShape = new Shape
            {
                // Define the shape as a regular 2‑D shape
                Type = TypeValue.Shape
            };

            // Set geometry: position (PinX, PinY) and size (Width, Height)
            masterShape.XForm.PinX.Value = 1.0;   // X centre of the shape
            masterShape.XForm.PinY.Value = 1.0;   // Y centre of the shape
            masterShape.XForm.Width.Value = 2.0; // Width in inches
            masterShape.XForm.Height.Value = 1.0; // Height in inches

            // Optional: give the rectangle a fill colour
            masterShape.Fill.FillForegnd.Value = "#FFCC00"; // orange fill

            // Add the rectangle shape to the master’s shape collection
            master.Shapes.Add(masterShape);

            // Register the new master with the diagram
            diagram.Masters.Add(master);

            // ---------------------------------------------------------------
            // Add an instance of the newly created master to the first page
            // ---------------------------------------------------------------
            Page page = diagram.Pages[0]; // Use the first page of the diagram

            // AddShape returns the shape ID (long). The fourth argument is a bool.
            long newShapeId = page.AddShape(3.0, 3.0, master.Name, false);

            // Retrieve the concrete Shape object using the returned ID
            Shape instanceShape = page.Shapes.GetShape(newShapeId);

            // Set some visible text on the new shape
            instanceShape.Text.Value.Clear();                     // Remove any default text
            instanceShape.Text.Value.Add(new Txt("Hello World")); // Add custom text

            // ---------------------------------------------------------------
            // Save the modified diagram to the specified output file
            // ---------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}