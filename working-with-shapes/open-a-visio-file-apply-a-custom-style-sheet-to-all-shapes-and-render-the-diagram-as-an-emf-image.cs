using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom StyleSheet to be applied to all shapes.
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();
                customStyle.ID = diagram.StyleSheets.Count + 1;
                customStyle.Name = "CustomStyle";

                // Define character (text) style – red text color.
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.Color.Value = "#FF0000"; // Red
                charStyle.IX = 0;
                customStyle.Chars.Add(charStyle);

                // Define fill style – green fill.
                customStyle.Fill.FillForegnd.Value = "#00FF00"; // Green

                // Define line style – blue line color.
                customStyle.Line.LineColor.Value = "#0000FF"; // Blue

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the custom stylesheet to every shape on every page.
                // ------------------------------------------------------------
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        shape.TextStyle = customStyle;
                        shape.FillStyle = customStyle;
                        shape.LineStyle = customStyle;
                    }
                }

                // ------------------------------------------------------------
                // Render each page of the diagram as an EMF image.
                // ------------------------------------------------------------
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                    imgOptions.PageIndex = i;   // Zero‑based page index.
                    imgOptions.PageCount = 1;   // Export a single page.

                    string outputPath = Path.Combine(outputDir, $"page_{i + 1}.emf");

                    // Save the page as EMF.
                    diagram.Save(outputPath, imgOptions);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}