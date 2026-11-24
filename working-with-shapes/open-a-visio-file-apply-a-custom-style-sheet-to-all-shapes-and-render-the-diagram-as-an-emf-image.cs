using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputEmfPath>");
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
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom StyleSheet.
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();
                customStyle.Name = "CustomStyle";

                // Assign a unique ID for the stylesheet.
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // Define a character style (e.g., blue text).
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.Color.Value = "#0000FF"; // Blue text color.
                charStyle.IX = 0; // First character style.
                customStyle.Chars.Add(charStyle);

                // Define line style (e.g., red line, thin weight).
                customStyle.Line.LineColor.Value = "#FF0000"; // Red line color.
                customStyle.Line.LineWeight.Value = 0.02;    // Thin line (in inches).

                // Define fill style (e.g., green fill, solid pattern).
                customStyle.Fill.FillForegnd.Value = "#00FF00"; // Green fill color.
                customStyle.Fill.FillPattern.Value = 1;        // Solid fill pattern.

                // Add the stylesheet to the document.
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the custom stylesheet to every shape in every page.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Assign the stylesheet to text, fill, and line styles.
                        shape.TextStyle = customStyle;
                        shape.FillStyle = customStyle;
                        shape.LineStyle = customStyle;
                    }
                }

                // ------------------------------------------------------------
                // Render the diagram as an EMF image.
                // ------------------------------------------------------------
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram successfully saved as EMF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}