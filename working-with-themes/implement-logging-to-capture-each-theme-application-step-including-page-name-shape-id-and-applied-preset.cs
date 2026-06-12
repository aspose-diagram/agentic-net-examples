using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the preset theme to apply
                PresetThemeValue presetTheme = PresetThemeValue.Bubble;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Apply the preset theme to the page
                    page.PresetTheme = presetTheme;

                    // Log the page theme application
                    Console.WriteLine($"Applied theme '{presetTheme}' to page '{page.Name}'.");

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply the same preset theme to the shape
                        shape.PresetTheme = presetTheme;

                        // Log the shape theme application
                        Console.WriteLine($"Applied theme '{presetTheme}' to shape ID {shape.ID} on page '{page.Name}'.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Theme application completed and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }