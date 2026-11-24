using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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

        // Prompt the user for the name that will replace the placeholder "[Name]".
        Console.Write("Enter the name to replace '[Name]' with: ");
        string userName = Console.ReadLine() ?? string.Empty;

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each text run (FormatTxt) within the shape's text collection.
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast the FormatTxt to Txt to access the actual text string.
                        if (fmt is Txt txt && !string.IsNullOrEmpty(txt.Text))
                        {
                            // Replace the placeholder "[Name]" with the user‑provided name.
                            if (txt.Text.Contains("[Name]"))
                            {
                                txt.Text = txt.Text.Replace("[Name]", userName);
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}