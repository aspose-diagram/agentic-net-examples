using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to the VSS stencil file and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <stencil.vss> <output.vsdx>");
            return;
        }

        string stencilPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the stencil file exists.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Create an empty diagram.
            Diagram diagram = new Diagram();

            // Add a window so the diagram has a visible drawing area.
            Window window = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 1100,
                WindowHeight = 700
            };
            diagram.Windows.Add(window);

            // Add a new page to host the shape.
            Page page = new Page();
            diagram.Pages.Add(page);
            // Note: Diagram.ActivePage is read‑only; we work with the page we just added.

            // Define the master name to import from the stencil.
            // Adjust this name to match a master present in your VSS file.
            string masterName = "Rectangle";

            // Import the master from the stencil file into the diagram.
            diagram.AddMaster(stencilPath, masterName);

            // Add a shape based on the imported master.
            // The fourth parameter is the zero‑based page index (0 for the first page).
            long shapeId = diagram.AddShape(2.0, 2.0, masterName, 0);

            // Retrieve the shape object to optionally modify its properties.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape != null && shape.Master != null)
            {
                // Example: set a simple text label on the shape.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Hello from master!"));
            }

            // Save the resulting diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}