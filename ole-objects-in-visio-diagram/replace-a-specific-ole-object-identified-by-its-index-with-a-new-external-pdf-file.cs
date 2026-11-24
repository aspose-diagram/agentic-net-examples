using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - output Visio file path
        // 2 - PDF file to embed
        // 3 - page index (0‑based)
        // 4 - shape index within the page (0‑based)
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <outputVisio> <pdfFile> <pageIndex> <shapeIndex>");
            return;
        }

        string inputVisioPath = args[0];
        string outputVisioPath = args[1];
        string pdfFilePath = args[2];
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }
        if (!File.Exists(pdfFilePath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfFilePath}");
            return;
        }

        if (!int.TryParse(args[3], out int pageIndex))
        {
            Console.Error.WriteLine("Invalid page index.");
            return;
        }
        if (!int.TryParse(args[4], out int shapeIndex))
        {
            Console.Error.WriteLine("Invalid shape index.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = diagram.Pages[pageIndex];

            // Validate shape index
            if (shapeIndex < 0 || shapeIndex >= page.Shapes.Count)
            {
                Console.Error.WriteLine($"Shape index out of range. Page has {page.Shapes.Count} shapes.");
                return;
            }

            // Retrieve the shape that should contain the OLE object
            Shape shape = page.Shapes[shapeIndex];

            // Ensure the shape actually has foreign (OLE) data
            if (shape.ForeignData == null)
            {
                Console.Error.WriteLine("The selected shape does not contain any OLE (foreign) data.");
                return;
            }

            // Read the PDF file bytes
            byte[] pdfBytes = File.ReadAllBytes(pdfFilePath);

            // Replace the embedded OLE object data with the new PDF bytes
            shape.ForeignData.ObjectData = pdfBytes;

            // Optionally update the source file name (helps Visio identify the object type)
            shape.ForeignData.ObjectSourceFullName = Path.GetFileName(pdfFilePath);

            // Optionally set the object to display as an icon
            shape.ForeignData.ShowAsIcon = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully replaced OLE object and saved to: {outputVisioPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}