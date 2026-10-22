using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate that shapes inheriting fill from masters display the master's gradient correctly
                ValidateInheritedGradient(diagram);

                // Prepare PDF save options with a simple page-saving callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new SimplePageSavingCallback();

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Validates that each shape which uses a master inherits the master's gradient fill correctly
    private static void ValidateInheritedGradient(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Process only shapes that have an associated master
                if (shape.Master != null)
                {
                    Master master = shape.Master;

                    // Ensure the master contains at least one shape definition
                    if (master.Shapes == null || master.Shapes.Count == 0)
                    {
                        Console.WriteLine($"Master '{master.Name}' has no shape definitions. Skipping shape ID {shape.ID}.");
                        continue;
                    }

                    // Retrieve the first shape of the master (the visual representation)
                    Shape masterShape = master.Shapes[0];

                    // Compare fill pattern (gradient pattern value is typically 25)
                    int shapePattern = shape.InheritFill.FillPattern.Value;
                    int masterPattern = masterShape.Fill.FillPattern.Value;

                    // Compare gradient enabled flag
                    BOOL shapeGradientEnabled = shape.InheritFill.GradientFill.GradientEnabled.Value;
                    BOOL masterGradientEnabled = masterShape.Fill.GradientFill.GradientEnabled.Value;

                    // Validate pattern
                    if (shapePattern != masterPattern)
                    {
                        throw new Exception($"Shape ID {shape.ID} on page {page.ID} has mismatched FillPattern (shape: {shapePattern}, master: {masterPattern}).");
                    }

                    // Validate gradient enabled flag
                    if (shapeGradientEnabled != masterGradientEnabled)
                    {
                        throw new Exception($"Shape ID {shape.ID} on page {page.ID} has mismatched GradientEnabled (shape: {shapeGradientEnabled}, master: {masterGradientEnabled}).");
                    }

                    // Log successful validation
                    Console.WriteLine($"Shape ID {shape.ID} correctly inherits gradient from master '{master.Name}'.");
                }
            }
        }
    }

    // Simple implementation of IPageSavingCallback to log page save events
    private class SimplePageSavingCallback : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");
        }
    }
}