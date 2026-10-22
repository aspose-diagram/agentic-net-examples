using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_protected.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // ------------------------------
            // Apply protection to the shape
            // Lock various editing aspects but allow text editing
            // ------------------------------

            // Lock moving the shape horizontally
            shape.Protection.LockMoveX.Value = BOOL.True;
            // Lock moving the shape vertically
            shape.Protection.LockMoveY.Value = BOOL.True;
            // Lock resizing the shape's width
            shape.Protection.LockWidth.Value = BOOL.True;
            // Lock resizing the shape's height
            shape.Protection.LockHeight.Value = BOOL.True;
            // Lock rotating the shape
            shape.Protection.LockRotate.Value = BOOL.True;
            // Lock deleting the shape
            shape.Protection.LockDelete.Value = BOOL.True;
            // Lock changing the shape's aspect ratio
            shape.Protection.LockAspect.Value = BOOL.True;
            // Lock editing the shape's custom properties
            shape.Protection.LockCustProp.Value = BOOL.True;
            // Ensure text editing is NOT locked (allow paragraph modifications)
            shape.Protection.LockTextEdit.Value = BOOL.False;

            // ------------------------------
            // Save the protected diagram
            // ------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // ------------------------------
            // Verify protection settings
            // ------------------------------
            Console.WriteLine("Protection settings for the shape:");
            Console.WriteLine($"LockMoveX: {shape.Protection.LockMoveX.Value}");
            Console.WriteLine($"LockMoveY: {shape.Protection.LockMoveY.Value}");
            Console.WriteLine($"LockWidth: {shape.Protection.LockWidth.Value}");
            Console.WriteLine($"LockHeight: {shape.Protection.LockHeight.Value}");
            Console.WriteLine($"LockRotate: {shape.Protection.LockRotate.Value}");
            Console.WriteLine($"LockDelete: {shape.Protection.LockDelete.Value}");
            Console.WriteLine($"LockAspect: {shape.Protection.LockAspect.Value}");
            Console.WriteLine($"LockCustProp: {shape.Protection.LockCustProp.Value}");
            Console.WriteLine($"LockTextEdit (should be False to allow text edit): {shape.Protection.LockTextEdit.Value}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}