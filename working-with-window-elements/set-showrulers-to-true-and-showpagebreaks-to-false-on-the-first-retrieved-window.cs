using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one Window object.
                if (diagram.Windows == null || diagram.Windows.Count == 0)
                {
                    Console.Error.WriteLine("No Window objects found in the diagram.");
                    return;
                }

                // Retrieve the first Window.
                Window firstWindow = diagram.Windows[0];

                // Set ShowRulers to true and ShowPageBreaks to false using BOOL values.
                firstWindow.ShowRulers = BOOL.True;
                firstWindow.ShowPageBreaks = BOOL.False;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Window properties updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}