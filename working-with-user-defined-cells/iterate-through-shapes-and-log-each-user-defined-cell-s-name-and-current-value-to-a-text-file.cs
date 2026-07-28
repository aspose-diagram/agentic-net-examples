using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (default if not provided)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                // Output log file path (default if not provided)
                string outputPath = args.Length > 1 ? args[1] : "UserCellsLog.txt";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Open a StreamWriter to write the log
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Iterate through user-defined cells (Users collection)
                            foreach (User userCell in shape.Users)
                            {
                                // Prefer universal name if available
                                string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;
                                // Get the current value (may be empty)
                                string cellValue = userCell.Value?.Val ?? string.Empty;

                                // Write a line to the log file
                                writer.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, User Cell: {cellName}, Value: {cellValue}");
                            }
                        }
                    }
                }

                Console.WriteLine($"User-defined cells have been logged to: {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }