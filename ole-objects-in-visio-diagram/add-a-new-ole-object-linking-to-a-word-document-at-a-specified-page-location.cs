using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <wordFilePath> <outputPath> <pinX> <pinY>");
            return;
        }

        string diagramPath = args[0];
        string wordFilePath = args[1];
        string outputPath = args[2];
        string pinXStr = args[3];
        string pinYStr = args[4];

        // Guard: check diagram file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: check Word file existence
        if (!File.Exists(wordFilePath))
        {
            Console.Error.WriteLine($"Word file not found: {wordFilePath}");
            return;
        }

        // Parse coordinates
        if (!double.TryParse(pinXStr, out double pinX) || !double.TryParse(pinYStr, out double pinY))
        {
            Console.Error.WriteLine("Invalid pinX or pinY values. They must be numeric.");
            return;
        }

        try
        {
            // Load the Visio diagram using a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Use the first page (index 0) for inserting the OLE object
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Master name for OLE objects in Visio is typically "OLE Object"
                // Add a new shape at the specified location
                long shapeId = page.AddShape(pinX, pinY, "OLE Object");

                // Retrieve the newly added shape
                Shape oleShape = page.Shapes.GetShape(shapeId);
                if (oleShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added OLE shape.");
                    return;
                }

                // Load the Word document bytes
                byte[] wordBytes = File.ReadAllBytes(wordFilePath);

                // Assign OLE data to the shape's ForeignData
                oleShape.ForeignData.ObjectData = wordBytes;
                // Set the source full name (optional, helps identify the linked file)
                oleShape.ForeignData.ObjectSourceFullName = Path.GetFileName(wordFilePath);

                // Set a caption for the OLE icon shape
                oleShape.Text.Value.Clear();
                oleShape.Text.Value.Add(new Txt("Word Document"));

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"OLE object linked successfully. Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}