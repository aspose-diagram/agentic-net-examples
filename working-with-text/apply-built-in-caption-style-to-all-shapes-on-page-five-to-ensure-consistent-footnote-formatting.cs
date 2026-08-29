using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least five pages (zero‑based index 4)
            if (diagram.Pages.Count < 5)
            {
                Console.Error.WriteLine("The diagram does not contain a page five.");
                return;
            }

            // Retrieve page five (index 4)
            Page pageFive = diagram.Pages[4];

            // Locate the built‑in "Caption" style sheet
            StyleSheet captionStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Caption")
                {
                    captionStyle = ss;
                    break;
                }
            }

            // If the style is not found, report and exit
            if (captionStyle == null)
            {
                Console.Error.WriteLine("Built‑in style \"Caption\" was not found in the diagram.");
                return;
            }

            // Apply the Caption style to every shape on page five
            foreach (Shape shape in pageFive.Shapes)
            {
                // Assign the style to the shape's text formatting
                shape.TextStyle = captionStyle;
            }

            // Save the modified diagram to the output file (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully applied \"Caption\" style to all shapes on page five and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}