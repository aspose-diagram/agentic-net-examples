using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputReportPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "EventSectionReport.txt";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("=== EventSection Configuration Report ===");
                reportBuilder.AppendLine($"Diagram: {Path.GetFileName(inputPath)}");
                reportBuilder.AppendLine($"Generated on: {DateTime.Now}");
                reportBuilder.AppendLine();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Collect event formulas
                        var events = new StringBuilder();

                        // EventDrop
                        string dropFormula = shape.Event?.EventDrop?.Ufe?.F;
                        if (!string.IsNullOrWhiteSpace(dropFormula))
                            events.AppendLine($"  EventDrop: {dropFormula}");

                        // EventDblClick
                        string dblClickFormula = shape.Event?.EventDblClick?.Ufe?.F;
                        if (!string.IsNullOrWhiteSpace(dblClickFormula))
                            events.AppendLine($"  EventDblClick: {dblClickFormula}");

                        // EventXFMod
                        string xfModFormula = shape.Event?.EventXFMod?.Ufe?.F;
                        if (!string.IsNullOrWhiteSpace(xfModFormula))
                            events.AppendLine($"  EventXFMod: {xfModFormula}");

                        // EventMultiDrop
                        string multiDropFormula = shape.Event?.EventMultiDrop?.Ufe?.F;
                        if (!string.IsNullOrWhiteSpace(multiDropFormula))
                            events.AppendLine($"  EventMultiDrop: {multiDropFormula}");

                        // If any custom event is defined, add shape info to the report
                        if (events.Length > 0)
                        {
                            reportBuilder.AppendLine($"Page: {page.NameU} (ID: {page.ID})");
                            reportBuilder.AppendLine($"  Shape ID: {shape.ID}, NameU: {shape.NameU}");
                            reportBuilder.Append(events.ToString());
                            reportBuilder.AppendLine();
                        }
                    }
                }

                // Write the report to the specified output file
                File.WriteAllText(outputPath, reportBuilder.ToString());
                Console.WriteLine($"EventSection report generated successfully at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}