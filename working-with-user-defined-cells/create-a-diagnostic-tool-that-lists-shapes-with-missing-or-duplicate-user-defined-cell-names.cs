using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the Visio file path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
                return;
            }

            string inputPath = args[0];
            Diagram diagram;

            // Load the diagram
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            var reportLines = new List<string>();
            int pageIndex = 0;

            // Iterate through pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check for missing user‑defined cells
                    if (shape.Users == null || shape.Users.Count == 0)
                    {
                        string msg = $"Page {pageIndex}, Shape ID {shape.ID} ({shape.NameU}) has no user‑defined cells.";
                        Console.WriteLine(msg);
                        reportLines.Add(msg);
                        continue;
                    }

                    // Detect duplicate user‑defined cell names within the shape
                    var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    var duplicateNames = new List<string>();

                    foreach (User user in shape.Users)
                    {
                        string name = user.NameU ?? user.Name;
                        if (!seenNames.Add(name))
                        {
                            duplicateNames.Add(name);
                        }
                    }

                    if (duplicateNames.Count > 0)
                    {
                        string dupList = string.Join(", ", duplicateNames);
                        string msg = $"Page {pageIndex}, Shape ID {shape.ID} ({shape.NameU}) has duplicate user‑defined cell names: {dupList}.";
                        Console.WriteLine(msg);
                        reportLines.Add(msg);
                    }
                }

                pageIndex++;
            }

            // Write the diagnostic report to a text file
            string reportPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "diagnostic_report.txt");
            try
            {
                File.WriteAllLines(reportPath, reportLines);
                Console.WriteLine($"Diagnostic report written to: {reportPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write report file: {ex.Message}");
            }

            // Save a copy of the diagram (unchanged) to demonstrate save usage
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "diagnostic_output.vsdx");
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }