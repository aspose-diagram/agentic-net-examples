using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputReportFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : null;

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare a StringBuilder for the report.
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Shape ID,Shape Name,Fill Inherited,Line Inherited");

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape ID and name.
                    long shapeId = shape.ID;
                    string shapeName = shape.NameU ?? string.Empty;

                    // Determine fill inheritance status.
                    bool isFillInherited = false;
                    try
                    {
                        // Compare the foreground fill value with the inherited value.
                        isFillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    }
                    catch
                    {
                        // If any property is unavailable, treat as not inherited.
                        isFillInherited = false;
                    }

                    // Determine line inheritance status.
                    bool isLineInherited = false;
                    try
                    {
                        // Compare the line color value with the inherited line color.
                        isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                    }
                    catch
                    {
                        // If any property is unavailable, treat as not inherited.
                        isLineInherited = false;
                    }

                    // Append the information to the report.
                    reportBuilder.AppendLine($"{shapeId},{shapeName},{(isFillInherited ? "Inherited" : "Not Inherited")},{(isLineInherited ? "Inherited" : "Not Inherited")}");
                }
            }

            // Output the report to console.
            Console.WriteLine(reportBuilder.ToString());

            // If an output file path was provided, write the report to the file.
            if (!string.IsNullOrEmpty(outputPath))
            {
                try
                {
                    File.WriteAllText(outputPath, reportBuilder.ToString());
                    Console.WriteLine($"Report saved to \"{outputPath}\".");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error writing report to file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}