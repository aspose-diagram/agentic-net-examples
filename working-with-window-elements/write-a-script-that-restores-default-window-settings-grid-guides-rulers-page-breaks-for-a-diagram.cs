using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input file path and output file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure at least one window exists; create one if necessary
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window();
                    // Set a basic window type; other properties can remain default
                    newWindow.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(newWindow);
                }

                // Retrieve the first window (index 0) to modify its settings
                Window window = diagram.Windows[0];

                // Restore default visibility settings
                window.ShowGrid = BOOL.True;          // Show grid lines
                window.ShowGuides = BOOL.True;        // Show alignment guides
                window.ShowRulers = BOOL.True;        // Show rulers
                window.ShowPageBreaks = BOOL.True;    // Show page breaks

                // Optionally, also restore dynamic grid and connection points to defaults
                // These properties accept BOOL values as well
                window.DynamicGridEnabled = BOOL.True;      // Enable dynamic grid
                window.ShowConnectionPoints = BOOL.True;    // Show connection points

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}