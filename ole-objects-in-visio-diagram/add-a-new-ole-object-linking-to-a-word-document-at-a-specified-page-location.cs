using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        // Word document to embed as OLE object
        string wordPath = "document.docx";
        // Output Visio diagram path
        string outputPath = "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: ensure the Word document exists
        if (!File.Exists(wordPath))
        {
            Console.Error.WriteLine($"Error: Word document not found at '{wordPath}'.");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Choose the target page (first page in this example)
            int targetPageIndex = 0;
            if (targetPageIndex < 0 || targetPageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine("Error: Specified page index is out of range.");
                return;
            }

            // Retrieve the target page
            Page page = diagram.Pages[targetPageIndex];

            // Define the location where the OLE object will be placed (in inches)
            double pinX = 2.0; // X coordinate
            double pinY = 2.0; // Y coordinate

            // Add a shape based on the "OLE Object" master from the default stencil
            // The AddShape method returns the shape ID (long)
            long shapeId = page.AddShape(pinX, pinY, "OLE Object");

            // Retrieve the newly added shape using its ID
            Shape oleShape = page.Shapes.GetShape(shapeId);

            // Load the Word document bytes
            byte[] oleData = File.ReadAllBytes(wordPath);

            // Assign the OLE data to the shape's ForeignData
            oleShape.ForeignData.ObjectData = oleData;
            // Set the source full name (optional, helps identify the embedded file)
            oleShape.ForeignData.ObjectSourceFullName = wordPath;
            // Mark the object type as an embedded object
            oleShape.ForeignData.ObjectType = ObjectType.EmbeddedObject;

            // Optionally set a caption for the OLE icon shape
            oleShape.Text.Value.Clear();
            oleShape.Text.Value.Add(new Txt("Embedded Word Document"));

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully added OLE object and saved diagram to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}