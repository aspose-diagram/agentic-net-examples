using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: expect diagram path and target page name.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageName>");
            return;
        }

        string diagramPath = args[0];
        string targetPageName = args[1];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the page by its name.
            // The GetPage(string) method returns null if the page does not exist.
            Page page = diagram.Pages.GetPage(targetPageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Error: No page named '{targetPageName}' was found in the diagram.");
                return;
            }

            Console.WriteLine($"Page '{targetPageName}' retrieved successfully. Shape count: {page.Shapes.Count}");

            // Example shape operation: list all shapes on the page.
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                // Retrieve shape by index.
                Shape shape = page.Shapes[i];
                // Output basic shape information.
                string shapeName = shape.NameU ?? "<no NameU>";
                Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shapeName}, Master: {(shape.Master != null ? shape.Master.Name : "<none>")}");
            }

            // Example modification: if there is at least one shape, change its width to 2.0 inches.
            if (page.Shapes.Count > 0)
            {
                Shape firstShape = page.Shapes[0];
                // Width is stored in shape.XForm.Width.Value (in inches).
                firstShape.XForm.Width.Value = 2.0;
                Console.WriteLine($"Modified first shape (ID {firstShape.ID}) width to 2.0 inches.");
            }

            // Save the modified diagram to a new file (optional).
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", 
                                            Path.GetFileNameWithoutExtension(diagramPath) + "_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}