using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, output diagram path, master name, help document path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <output.vsdx> <masterName> <helpDocumentPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string masterName = args[2];
        string helpDocPath = args[3];

        // Guard input diagram file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }

        // Guard help document file existence (optional, but ensures the link points to an existing file)
        if (!File.Exists(helpDocPath))
        {
            Console.Error.WriteLine($"Help document file not found: {helpDocPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the active page where the shape will be placed
            Page page = diagram.ActivePage;

            // Add a new shape based on the specified master.
            // Parameters: pinX, pinY, master name, isCalculate (set to false)
            long shapeId = page.AddShape(1.0, 1.0, masterName, false);

            // Retrieve the newly added shape instance
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            // Create a new hyperlink that points to the help document
            Hyperlink link = new Hyperlink();
            link.Name = "HelpLink";
            // Set the address to the help document path
            link.Address.Value = helpDocPath;
            // Optional description shown on hover
            link.Description.Value = "Open Help Document";
            // Open in the same window (set to FALSE)
            link.NewWindow.Value = BOOL.False;

            // Ensure the Hyperlinks collection exists and add the new hyperlink
            if (shape.Hyperlinks != null)
            {
                shape.Hyperlinks.Add(link);
            }
            else
            {
                Console.Error.WriteLine("The shape does not support hyperlinks.");
                return;
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}