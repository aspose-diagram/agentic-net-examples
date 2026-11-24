using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the formula from the EventXFMod cell (closest substitute for EventCalc)
                        string formula = shape.Event?.EventXFMod?.Ufe?.F;

                        // Simple evaluation: treat the formula as true if it contains "TRUE" or equals "1"
                        bool condition = false;
                        if (!string.IsNullOrEmpty(formula))
                        {
                            string trimmed = formula.Trim().ToUpperInvariant();
                            if (trimmed.Contains("TRUE") || trimmed == "1")
                                condition = true;
                        }

                        // Apply conditional formatting based on the evaluation result
                        if (condition)
                        {
                            // Highlight shape with a red dashed border and red fill foreground
                            shape.Line.LinePattern.Value = LinePatternValue.Dash;
                            shape.Line.LineWeight.Value = 0.03; // thickness in inches
                            shape.Line.BeginArrow.Value = 4;    // arrow style
                            shape.Line.EndArrow.Value = 4;
                            shape.Fill.FillForegnd.Value = "#FF0000"; // red fill foreground
                        }
                        else
                        {
                            // Set a green solid border and green fill foreground
                            shape.Line.LinePattern.Value = LinePatternValue.Solid;
                            shape.Line.LineWeight.Value = 0.02;
                            shape.Line.BeginArrow.Value = 0;
                            shape.Line.EndArrow.Value = 0;
                            shape.Fill.FillForegnd.Value = "#00FF00"; // green fill foreground
                        }
                    }
                }

                // Determine output path (same directory with a suffix)
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}