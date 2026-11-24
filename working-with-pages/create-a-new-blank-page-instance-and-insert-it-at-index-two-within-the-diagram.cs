using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID to assign a unique ID to the new page.
                int maxPageId = 0; // ID is an int, so use int here.
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new blank page instance.
                Page newPage = new Page();
                newPage.ID = maxPageId + 1;                     // Assign a unique ID (int).
                newPage.Name = $"Page-{newPage.ID}";           // Optional: give the page a name.

                // Add the new page to the diagram's Pages collection.
                diagram.Pages.Add(newPage);

                // Insert the new page at index 2 (zero‑based). MoveTo repositions the page.
                // If the diagram has fewer than 3 pages, the page will be placed at the end.
                newPage.MoveTo(2);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
