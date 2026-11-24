using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output PDF path, required user-defined cell names (comma‑separated)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputPdfPath> <requiredCellNamesCommaSeparated>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string requiredCellsArg = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse required cell names into an array (trim whitespace)
        string[] requiredCellNames = requiredCellsArg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < requiredCellNames.Length; i++)
        {
            requiredCellNames[i] = requiredCellNames[i].Trim();
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool validationFailed = false;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Collect existing user‑defined cell names for the current shape
                        // Use a simple array to store names (no LINQ per rules)
                        string[] existingNames = new string[shape.Users.Count];
                        int nameIndex = 0;
                        foreach (User user in shape.Users)
                        {
                            existingNames[nameIndex++] = user.NameU;
                        }

                        // Verify each required cell name is present
                        foreach (string requiredName in requiredCellNames)
                        {
                            bool found = false;
                            for (int n = 0; n < existingNames.Length; n++)
                            {
                                if (existingNames[n] == requiredName)
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                validationFailed = true;
                                Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.ID} is missing required user-defined cell \"{requiredName}\".");
                            }
                        }
                    }
                }

                // If any shape failed validation, abort the export
                if (validationFailed)
                {
                    Console.Error.WriteLine("Validation failed. PDF export aborted.");
                    return;
                }

                // All shapes contain the required cells – proceed to export to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Example: export hidden pages as well (plain bool, not BOOL)
                pdfOptions.ExportHiddenPage = true;
                // Set high resolution for raster images inside the PDF
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;

                // Save the diagram as PDF using the options
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}