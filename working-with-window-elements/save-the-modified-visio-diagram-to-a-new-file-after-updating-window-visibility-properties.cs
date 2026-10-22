using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_modified.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one window; add a default one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    // Use cast to zero for normal state as Normal is not defined
                    defaultWindow.WindowState = (WindowStateValue)0;
                    diagram.Windows.Add(defaultWindow);
                }

                // Update visibility properties for each window in the diagram
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGrid = BOOL.True;
                    win.ShowGuides = BOOL.True;
                    win.ShowRulers = BOOL.True;
                    win.ShowPageBreaks = BOOL.True;
                }

                // Save the modified diagram to a new file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}