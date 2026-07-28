using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect at least the diagram file path; optional second argument is the log file path.
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: PageDimensionValidator <diagramPath> [logPath]");
                return;
            }

            string diagramPath = args[0];
            string logPath = args.Length >= 2 ? args[1] : null;

            // Define the expected page dimensions (in inches) for each page name.
            var template = new Dictionary<string, (double Width, double Height)>(StringComparer.OrdinalIgnoreCase)
            {
                // Example entries – adjust as needed for your template.
                { "Page-1", (8.27, 11.69) }, // A4 portrait
                { "Page-2", (11.69, 8.27) }  // A4 landscape
            };

            var logLines = new List<string>();

            try
            {
                // Load the Visio diagram.
                using (var diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        string pageName = page.Name;
                        double actualWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                        if (template.TryGetValue(pageName, out var expected))
                        {
                            const double tolerance = 0.01; // inches tolerance for comparison
                            bool widthMatch = Math.Abs(actualWidth - expected.Width) <= tolerance;
                            bool heightMatch = Math.Abs(actualHeight - expected.Height) <= tolerance;

                            if (widthMatch && heightMatch)
                            {
                                string msg = $"[OK] Page \"{pageName}\" matches template (Width={actualWidth:F2}, Height={actualHeight:F2}).";
                                Console.WriteLine(msg);
                                logLines.Add(msg);
                            }
                            else
                            {
                                string msg = $"[MISMATCH] Page \"{pageName}\" dimensions differ. Expected (W={expected.Width:F2}, H={expected.Height:F2}), Actual (W={actualWidth:F2}, H={actualHeight:F2}).";
                                Console.WriteLine(msg);
                                logLines.Add(msg);
                            }
                        }
                        else
                        {
                            string msg = $"[WARNING] No template entry for page \"{pageName}\". Actual dimensions (W={actualWidth:F2}, H={actualHeight:F2}).";
                            Console.WriteLine(msg);
                            logLines.Add(msg);
                        }
                    }
                }

                // Write the collected log to a file if a log path was provided.
                if (!string.IsNullOrEmpty(logPath))
                {
                    File.WriteAllLines(logPath, logLines);
                    Console.WriteLine($"Log written to: {logPath}");
                }
            }
            catch (Exception ex)
            {
                string error = $"Error processing diagram: {ex.Message}";
                Console.WriteLine(error);
                if (!string.IsNullOrEmpty(logPath))
                {
                    File.WriteAllLines(logPath, new[] { error });
                }
            }
        }
    }