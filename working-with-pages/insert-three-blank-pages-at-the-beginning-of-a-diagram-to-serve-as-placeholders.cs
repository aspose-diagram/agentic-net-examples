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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block as required.
            using (Diagram sourceDiagram = new Diagram(inputPath))
            {
                // Create a new empty diagram that will receive the blank pages and then the source pages.
                using (Diagram targetDiagram = new Diagram())
                {
                    // Insert three blank pages at the beginning of the target diagram.
                    for (int i = 0; i < 3; i++)
                    {
                        // Create a new blank page.
                        Page blankPage = new Page();

                        // Assign a unique ID. IDs start at 1 and must be unique within the diagram.
                        // Since the target diagram is empty, we can simply use i + 1.
                        blankPage.ID = i + 1;

                        // Give the page a descriptive name.
                        blankPage.Name = $"BlankPage{i + 1}";

                        // Add the blank page to the diagram.
                        targetDiagram.Pages.Add(blankPage);
                    }

                    // Combine the source diagram into the target diagram.
                    // This appends all pages (and masters) from the source after the existing pages.
                    targetDiagram.Combine(sourceDiagram);

                    // Save the resulting diagram to the specified output path in VSDX format.
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine("Successfully inserted three blank pages and saved the diagram.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}