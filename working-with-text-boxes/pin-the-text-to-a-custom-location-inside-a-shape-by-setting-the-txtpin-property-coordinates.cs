using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, TxtPinX, TxtPinY
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <TxtPinX> <TxtPinY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string txtPinXStr = args[2];
        string txtPinYStr = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse coordinate values
        if (!double.TryParse(txtPinXStr, out double txtPinX))
        {
            Console.Error.WriteLine($"Invalid TxtPinX value: {txtPinXStr}");
            return;
        }

        if (!double.TryParse(txtPinYStr, out double txtPinY))
        {
            Console.Error.WriteLine($"Invalid TxtPinY value: {txtPinYStr}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (index 0)
            Shape shape = page.Shapes[0];

            // Ensure the shape is not deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The target shape is marked as deleted.");
                return;
            }

            // Set custom text pin coordinates
            // TxtPinX/Y control the origin point of the text block within the shape
            shape.TextXForm.TxtPinX.Value = txtPinX;
            shape.TextXForm.TxtPinY.Value = txtPinY;

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}