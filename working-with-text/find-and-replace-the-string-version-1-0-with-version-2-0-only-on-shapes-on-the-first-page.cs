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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work only on the first page (index 0).
            Page firstPage = diagram.Pages[0];

            const string oldText = "Version 1.0";
            const string newText = "Version 2.0";

            // Iterate through all shapes on the first page.
            foreach (Shape shape in firstPage.Shapes)
            {
                // Iterate through each text segment within the shape.
                foreach (FormatTxt fmt in shape.Text.Value)
                {
                    // Cast to Txt to access the Text property.
                    if (fmt is Txt txt && txt.Text != null && txt.Text.Contains(oldText))
                    {
                        // Replace the target substring.
                        txt.Text = txt.Text.Replace(oldText, newText);
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}