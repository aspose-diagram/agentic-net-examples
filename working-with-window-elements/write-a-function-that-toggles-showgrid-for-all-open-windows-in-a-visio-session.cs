using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard against missing input file.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Check if any windows are present.
            if (diagram.Windows.Count == 0)
            {
                Console.WriteLine("No windows found in the diagram.");
            }
            else
            {
                // Iterate over all windows and toggle the ShowGrid property.
                foreach (Window window in diagram.Windows)
                {
                    // Toggle: if currently TRUE make it FALSE, otherwise make it TRUE.
                    window.ShowGrid = (window.ShowGrid == BOOL.True) ? BOOL.False : BOOL.True;
                }

                // Save the modified diagram back to the same file (overwrites original).
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine("ShowGrid toggled for all windows and diagram saved.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}