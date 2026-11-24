using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Attempt to delete a built‑in property (Title) by setting it to null.
            // Built‑in properties are read‑only in many cases; this operation is expected to throw.
            try
            {
                diagram.DocumentProps.Title = null;
                // If no exception is thrown, indicate unexpected behavior.
                Console.WriteLine("Unexpected: No exception was thrown when attempting to delete a built‑in property.");
            }
            catch (Exception ex)
            {
                // Expected path: an exception should be caught.
                Console.WriteLine($"Caught expected exception: {ex.GetType().Name} - {ex.Message}");
            }

            // Optionally, save the diagram to demonstrate normal operation (not required for the test).
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? string.Empty,
                                             Path.GetFileNameWithoutExtension(diagramPath) + "_output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception e)
        {
            // Handle any unexpected errors during loading or saving.
            Console.Error.WriteLine($"Error: {e.GetType().Name} - {e.Message}");
        }
    }
}