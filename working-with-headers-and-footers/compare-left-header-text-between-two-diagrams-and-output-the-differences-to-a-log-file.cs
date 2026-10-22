using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <DiagramPath1> <DiagramPath2> <LogFilePath>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];
        string logFilePath = args[2];

        // Guard: ensure both diagram files exist.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"Error: File not found – {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"Error: File not found – {diagramPath2}");
            return;
        }

        try
        {
            // Load the first diagram.
            Diagram diagram1 = new Diagram(diagramPath1);
            // Load the second diagram.
            Diagram diagram2 = new Diagram(diagramPath2);

            // Retrieve left header text from each diagram.
            string leftHeader1 = diagram1.HeaderFooter?.HeaderLeft ?? string.Empty;
            string leftHeader2 = diagram2.HeaderFooter?.HeaderLeft ?? string.Empty;

            // Prepare log content.
            string logContent;
            if (leftHeader1 == leftHeader2)
            {
                logContent = $"No differences found. Both diagrams have identical left header text:\r\n\"{leftHeader1}\"";
            }
            else
            {
                logContent = $"Differences detected in left header text:\r\n" +
                             $"Diagram 1 ({Path.GetFileName(diagramPath1)}): \"{leftHeader1}\"\r\n" +
                             $"Diagram 2 ({Path.GetFileName(diagramPath2)}): \"{leftHeader2}\"";
            }

            // Ensure the directory for the log file exists.
            string logDir = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            // Write the comparison result to the log file.
            File.WriteAllText(logFilePath, logContent);
            Console.WriteLine($"Comparison completed. Results written to: {logFilePath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}