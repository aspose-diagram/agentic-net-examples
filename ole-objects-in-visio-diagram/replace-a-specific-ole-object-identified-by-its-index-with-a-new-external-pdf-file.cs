using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <outputVisio> <pdfFile> <shapeId>");
            return;
        }

        string inputVisioPath = args[0];
        string outputVisioPath = args[1];
        string pdfFilePath = args[2];
        string shapeIdArg = args[3];

        // Guard: input Visio file must exist
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        // Guard: PDF file must exist
        if (!File.Exists(pdfFilePath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfFilePath}");
            return;
        }

        // Guard: shapeId must be a valid long integer
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape throws if not found, so use try/catch per page
                try
                {
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                    {
                        break;
                    }
                }
                catch
                {
                    // Shape not on this page, continue searching
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                return;
            }

            // Ensure the shape has ForeignData (OLE container)
            if (targetShape.ForeignData == null)
            {
                Console.Error.WriteLine($"Shape ID {shapeId} does not contain OLE (ForeignData) content.");
                return;
            }

            // Read the new PDF file into a byte array
            byte[] pdfBytes = File.ReadAllBytes(pdfFilePath);

            // Replace the OLE object's binary data with the new PDF bytes
            targetShape.ForeignData.ObjectData = pdfBytes;

            // Update the source file name (optional, for reference)
            targetShape.ForeignData.ObjectSourceFullName = Path.GetFileName(pdfFilePath);

            // Optionally display the OLE object as an icon
            targetShape.ForeignData.ShowAsIcon = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully replaced OLE object in shape {shapeId} and saved to {outputVisioPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}