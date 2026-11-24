using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input diagram file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Prepare a temporary output path for the serialized diagram
        string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".vsdx");

        try
        {
            // Load the original diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Guard: ensure the diagram has at least one window
                if (diagram.Windows.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no windows.");
                    return;
                }

                // Access the first window and set ShowPageBreaks to TRUE
                Window window = diagram.Windows[0];
                window.ShowPageBreaks = BOOL.True;

                // Save the modified diagram to the temporary file
                diagram.Save(tempPath, SaveFileFormat.Vsdx);
            }

            // Reload the diagram from the temporary file to verify persistence
            using (Diagram reloadedDiagram = new Diagram(tempPath))
            {
                // Guard: ensure the reloaded diagram has at least one window
                if (reloadedDiagram.Windows.Count == 0)
                {
                    Console.Error.WriteLine("Error: Reloaded diagram contains no windows.");
                    return;
                }

                // Access the first window and verify ShowPageBreaks is TRUE
                Window reloadedWindow = reloadedDiagram.Windows[0];
                if (reloadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks property did not persist after save and reload.");
                }

                // If verification passes, output success message
                Console.WriteLine("Success: ShowPageBreaks property persisted correctly after serialization.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it exists
            if (File.Exists(tempPath))
            {
                try
                {
                    File.Delete(tempPath);
                }
                catch
                {
                    // Suppress any cleanup errors
                }
            }
        }
    }
}