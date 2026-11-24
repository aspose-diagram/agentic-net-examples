using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path (same folder, same name with .pdf)
        string outputPath = Path.ChangeExtension(inputPath, ".pdf");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate that shapes inheriting fill from masters have the same gradient as the master
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only shapes that have an associated master
                    if (shape.Master == null)
                        continue;

                    // Retrieve the master shape (first shape in the master collection)
                    Shape masterShape = null;
                    foreach (Shape ms in shape.Master.Shapes)
                    {
                        masterShape = ms;
                        break; // take the first shape as representative
                    }

                    if (masterShape == null)
                        continue; // no master shape to compare against

                    // Compare gradient fill settings between shape and its master
                    bool patternMatch = shape.Fill.FillPattern.Value == masterShape.Fill.FillPattern.Value;
                    bool gradientEnabledMatch = shape.Fill.GradientFill.GradientEnabled.Value == masterShape.Fill.GradientFill.GradientEnabled.Value;
                    bool gradientDirMatch = shape.Fill.GradientFill.GradientDir.Value == masterShape.Fill.GradientFill.GradientDir.Value;

                    // Simple validation: if any of the key gradient properties differ, report
                    if (!patternMatch || !gradientEnabledMatch || !gradientDirMatch)
                    {
                        Console.Error.WriteLine($"Gradient mismatch on page {page.Name}, shape ID {shape.ID} (master: {shape.Master.Name}).");
                        throw new Exception("Inherited gradient validation failed.");
                    }
                }
            }

            // Set up PDF save options with a page-saving callback for logging
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PdfPageCallback();

            // Export the diagram to PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

// Custom callback to log page saving events
class PdfPageCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");
        // Example: stop after first page (uncomment if needed)
        // args.HasMorePages = false;
    }
}