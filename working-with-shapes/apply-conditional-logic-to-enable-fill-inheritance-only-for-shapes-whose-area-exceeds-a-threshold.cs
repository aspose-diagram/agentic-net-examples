using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, area threshold.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <output.vsdx> <areaThreshold>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the source Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the output directory exists (create if necessary).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Parse the area threshold; if parsing fails, report and exit.
        if (!double.TryParse(args[2], out double areaThreshold))
        {
            Console.Error.WriteLine($"Invalid area threshold: {args[2]}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve width and height (in inches) from the shape's XForm.
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Compute the shape's area.
                    double area = width * height;

                    // Apply fill inheritance only when the area exceeds the threshold.
                    if (area > areaThreshold)
                    {
                        // Copy inherited fill pattern.
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        // Copy inherited foreground fill color.
                        shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                        // Copy inherited background fill color.
                        shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                        // (Optional) Copy inherited foreground transparency.
                        shape.Fill.FillForegndTrans.Value = shape.InheritFill.FillForegndTrans.Value;
                        // (Optional) Copy inherited background transparency.
                        shape.Fill.FillBkgndTrans.Value = shape.InheritFill.FillBkgndTrans.Value;
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format.
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