using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string diagramPath1 = @"C:\Diagrams\Diagram1.vsdx";
                string diagramPath2 = @"C:\Diagrams\Diagram2.vsdx";

                // Path to the log file where differences will be recorded
                string logFilePath = @"C:\Diagrams\HeaderComparisonLog.txt";

                // Load the first diagram
                Diagram diagram1 = new Diagram(diagramPath1);
                // Load the second diagram
                Diagram diagram2 = new Diagram(diagramPath2);

                // Retrieve the left header text from each diagram (null‑safe)
                string headerLeft1 = diagram1.HeaderFooter.HeaderLeft ?? string.Empty;
                string headerLeft2 = diagram2.HeaderFooter.HeaderLeft ?? string.Empty;

                // Compare the header texts and write the result to the log file
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"Comparison performed at {DateTime.Now}");
                    writer.WriteLine($"Diagram 1: {Path.GetFileName(diagramPath1)}");
                    writer.WriteLine($"Diagram 2: {Path.GetFileName(diagramPath2)}");

                    if (headerLeft1.Equals(headerLeft2, StringComparison.Ordinal))
                    {
                        writer.WriteLine("Result: Left header texts are identical.");
                    }
                    else
                    {
                        writer.WriteLine("Result: Left header texts differ.");
                        writer.WriteLine($" - Diagram 1 HeaderLeft: \"{headerLeft1}\"");
                        writer.WriteLine($" - Diagram 2 HeaderLeft: \"{headerLeft2}\"");
                    }

                    writer.WriteLine(new string('-', 50));
                }

                // Optional: inform the user via console
                Console.WriteLine("Header comparison completed. See log file for details.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }