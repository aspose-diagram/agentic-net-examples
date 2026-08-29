using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Subtitle" style sheet (if it exists)
            StyleSheet subtitleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                // Compare style name case‑sensitively as defined in Visio
                if (ss.Name == "Subtitle")
                {
                    subtitleStyle = ss;
                    break;
                }
            }

            // If the style is missing, report but continue without applying it
            if (subtitleStyle == null)
            {
                Console.Error.WriteLine("Warning: 'Subtitle' style not found in the document. No styles will be applied.");
            }

            // Iterate over every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape; empty strings are ignored
                    string plainText = shape.Text.Value.Text;

                    // Check if the text starts with a numeric character (0‑9)
                    if (!string.IsNullOrWhiteSpace(plainText) && char.IsDigit(plainText[0]))
                    {
                        // Apply the "Subtitle" style to the shape's text if the style was found
                        if (subtitleStyle != null)
                        {
                            shape.TextStyle = subtitleStyle;
                        }
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}