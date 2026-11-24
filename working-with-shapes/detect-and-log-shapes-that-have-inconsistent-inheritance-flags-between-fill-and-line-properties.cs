using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (required)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output log file path (optional)
        string logPath = args.Length > 1 ? args[1] : "inheritance_log.txt";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the log file (overwrite if exists)
                using (StreamWriter logWriter = new StreamWriter(logPath, false))
                {
                    logWriter.WriteLine($"Inheritance inconsistency report for diagram: {Path.GetFileName(inputPath)}");
                    logWriter.WriteLine($"Generated on: {DateTime.Now}");
                    logWriter.WriteLine();

                    int pageIndex = 0; // Manual page counter because Page.Index does not exist

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;

                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            bool fillInherited = false;
                            bool lineInherited = false;

                            try
                            {
                                // Compare fill color with inherited fill color
                                // Matching values indicate inheritance is active
                                fillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                            }
                            catch (Exception ex)
                            {
                                // If any part of the fill inheritance check fails, treat as not inherited
                                Console.Error.WriteLine($"Fill inheritance check failed for Shape ID {shape.ID}: {ex.Message}");
                                fillInherited = false;
                            }

                            try
                            {
                                // Compare line color with inherited line color
                                lineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                            }
                            catch (Exception ex)
                            {
                                // If any part of the line inheritance check fails, treat as not inherited
                                Console.Error.WriteLine($"Line inheritance check failed for Shape ID {shape.ID}: {ex.Message}");
                                lineInherited = false;
                            }

                            // Log shapes where fill and line inheritance flags differ
                            if (fillInherited != lineInherited)
                            {
                                string message = $"Page {pageIndex}, Shape ID {shape.ID}, Name \"{shape.NameU}\" - " +
                                                 $"FillInherited: {fillInherited}, LineInherited: {lineInherited}";
                                Console.WriteLine(message);
                                logWriter.WriteLine(message);
                            }
                        }
                    }

                    logWriter.WriteLine();
                    logWriter.WriteLine("Report generation completed.");
                }

                Console.WriteLine($"Inheritance log saved to: {logPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}