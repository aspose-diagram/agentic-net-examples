using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID to assign a unique ID to the new page
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new blank page
                Page newPage = new Page();

                // Assign a unique ID (cast to int) and a name to the new page
                newPage.ID = (int)(maxPageId + 1);
                newPage.Name = $"BlankPage_{newPage.ID}";

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);

                // Set the page size to A4 dimensions (width: 8.27 inches, height: 11.69 inches)
                newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
                newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Blank page added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}