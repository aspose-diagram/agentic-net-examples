using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: DetectInheritanceInconsistency <inputVisioFile> [outputLogFile]");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional output log file path; if omitted, logs are written only to console.
        string? logPath = args.Length > 1 ? args[1] : null;
        StreamWriter? logWriter = null;
        if (logPath != null)
        {
            try
            {
                // Create (or overwrite) the log file.
                logWriter = new StreamWriter(logPath, false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create log file '{logPath}': {ex.Message}");
                // Continue with console-only logging.
                logWriter = null;
            }
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the fill properties are inherited.
                    bool fillInherited = false;
                    try
                    {
                        // Compare a representative fill cell (foreground color) with its inherited counterpart.
                        fillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    }
                    catch
                    {
                        // If any fill cell is unavailable, treat as not inherited.
                        fillInherited = false;
                    }

                    // Determine if the line properties are inherited.
                    bool lineInherited = false;
                    try
                    {
                        // Compare a representative line cell (line color) with its inherited counterpart.
                        lineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                    }
                    catch
                    {
                        // If any line cell is unavailable, treat as not inherited.
                        lineInherited = false;
                    }

                    // Log shapes where fill and line inheritance statuses differ.
                    if (fillInherited != lineInherited)
                    {
                        string message = $"Page {pageIndex + 1}, Shape ID {shape.ID}, NameU '{shape.NameU}': " +
                                         $"FillInherited={fillInherited}, LineInherited={lineInherited}";
                        Console.WriteLine(message);
                        logWriter?.WriteLine(message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Ensure the log file is properly closed.
            logWriter?.Close();
        }
    }
}