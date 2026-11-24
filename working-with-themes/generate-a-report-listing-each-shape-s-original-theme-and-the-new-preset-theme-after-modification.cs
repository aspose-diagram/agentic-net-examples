using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputReportPath] [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string reportPath = args.Length > 1 ? args[1] : "ThemeReport.txt";
        string outputDiagramPath = args.Length > 2 ? args[2] : "ModifiedDiagram.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // StringBuilder to accumulate the report content
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("Shape Theme Modification Report");
        reportBuilder.AppendLine($"Source Diagram: {Path.GetFileName(inputPath)}");
        reportBuilder.AppendLine($"Generated On: {DateTime.Now}");
        reportBuilder.AppendLine();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Original theme cannot be read (write‑only), so we record it as NotAvailable
                        string originalTheme = "NotAvailable";

                        // Apply a new preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;

                        // Record the new theme (we know what we set)
                        string newTheme = PresetThemeValue.Bubble.ToString();

                        // Append information about this shape to the report
                        reportBuilder.AppendLine($"Shape ID {shape.ID}: Original Theme = {originalTheme}, New Theme = {newTheme}");
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

            // Write the report to the specified file
            File.WriteAllText(reportPath, reportBuilder.ToString());
            Console.WriteLine($"Report generated at: {reportPath}");
            Console.WriteLine($"Modified diagram saved at: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}