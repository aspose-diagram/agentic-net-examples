using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input file paths (modify as needed)
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string stencilPath = "stencil.vssx";
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        string masterName = "Rectangle"; // name of the master in the stencil

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the first page (or modify to select a specific page)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }
                Page page = diagram.Pages[0];

                // Import the master from the stencil file into the diagram
                // The AddMaster method adds the master and makes it available for shape creation
                diagram.AddMaster(stencilPath, masterName);

                // Define position where the new shape will be placed
                double pinX = 2.0;
                double pinY = 2.0;

                // Add a shape based on the imported master to the page
                // page.AddShape returns the shape ID (long)
                long shapeId = page.AddShape(pinX, pinY, masterName, false);

                // Retrieve the newly added shape (optional, e.g., to modify properties)
                Shape newShape = page.Shapes.GetShape(shapeId);
                // Example: set the shape's text (optional)
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt("Added via master"));

                // Prepare output path
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? "",
                    Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx");

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}