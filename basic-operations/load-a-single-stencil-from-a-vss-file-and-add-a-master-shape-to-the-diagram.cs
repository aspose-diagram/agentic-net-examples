using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: stencil file path, master name, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <stencil.vss> <masterName> <output.vsdx>");
            return;
        }

        string stencilPath = args[0];
        string masterName = args[1];
        string outputPath = args[2];

        // Guard: ensure the stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Create an empty diagram
            Diagram diagram = new Diagram();

            // Add a window so the diagram has an active page context
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.WindowState = WindowStateValue.Maximized;
            diagram.Windows.Add(window);

            // Add a default page to host shapes
            Page page = new Page();
            diagram.Pages.Add(page);

            // Import the specified master from the stencil file
            diagram.AddMaster(stencilPath, masterName);

            // Add a shape based on the imported master to the page
            // Parameters: pinX, pinY, master name, isCalculate (set to false)
            long shapeId = page.AddShape(2.0, 2.0, masterName, false);

            // Retrieve the newly added shape (optional, for further manipulation)
            Shape shape = page.Shapes.GetShape(shapeId);
            // Example: set the shape's text to indicate it was added programmatically
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Added via Aspose.Diagram"));

            // Save the resulting diagram to the specified output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}