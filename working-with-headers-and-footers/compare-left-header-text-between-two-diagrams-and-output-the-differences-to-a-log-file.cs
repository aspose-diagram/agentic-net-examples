using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect at least two file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2> [LogFilePath]");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];
        string logPath = args.Length >= 3 ? args[2] : "header_diff.log";

        // Guard: ensure both diagram files exist.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load the first diagram.
            Diagram diagram1 = new Diagram(diagramPath1);
            // Load the second diagram.
            Diagram diagram2 = new Diagram(diagramPath2);

            // Retrieve left header text from both diagrams.
            string headerLeft1 = diagram1.HeaderFooter.HeaderLeft ?? string.Empty;
            string headerLeft2 = diagram2.HeaderFooter.HeaderLeft ?? string.Empty;

            // Prepare log content.
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                if (headerLeft1 == headerLeft2)
                {
                    // No differences found.
                    logWriter.WriteLine("Left header texts are identical.");
                    Console.WriteLine("Left header texts are identical. No differences logged.");
                }
                else
                {
                    // Log the differences.
                    logWriter.WriteLine("Differences found in left header text:");
                    logWriter.WriteLine($"Diagram 1 ({Path.GetFileName(diagramPath1)}): \"{headerLeft1}\"");
                    logWriter.WriteLine($"Diagram 2 ({Path.GetFileName(diagramPath2)}): \"{headerLeft2}\"");
                    Console.WriteLine($"Differences logged to {logPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}