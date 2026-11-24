using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (provide a valid .vsdx file)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Set the default font name for all newly created text shapes
                FontConfigs.DefaultFontName = "Arial";

                // Ensure there is at least one page; create a new one if necessary
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Add a blank page to the diagram
                    diagram.Pages.Add(new Page());
                    page = diagram.Pages[0];
                }

                // Add a text annotation to the page.
                // This overload uses the default font configured above.
                page.AddText(
                    pinX: 2.0,
                    pinY: 2.0,
                    width: 2.0,
                    height: 0.5,
                    text: "Sample annotation"
                );

                // Save the modified diagram with the default font applied to the new annotation
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}