using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument)
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Desired line weight (in inches). Use second argument if provided, otherwise default.
        double newLineWeight = 0.05; // default value
        if (args.Length > 1 && double.TryParse(args[1], out double parsedWeight))
        {
            newLineWeight = parsedWeight;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Get the first shape on the page (index 0)
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }
            Shape shape = page.Shapes[0];

            // Set the line weight to the new value (in inches)
            shape.Line.LineWeight.Value = newLineWeight;

            // Verify the update by reading back the LineWeight property
            double actualWeight = shape.Line.LineWeight.Value;
            if (Math.Abs(actualWeight - newLineWeight) < 1e-6)
            {
                Console.WriteLine($"Line weight successfully updated to {actualWeight} inches.");
            }
            else
            {
                throw new Exception($"Line weight verification failed. Expected: {newLineWeight}, Actual: {actualWeight}");
            }

            // Optional: save the modified diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", 
                                             Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}