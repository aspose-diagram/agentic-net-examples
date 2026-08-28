using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define the folder containing Visio diagram files.
                // Adjust the path as needed or pass it via command line arguments.
                string inputFolder = args.Length > 0 ? args[0] : @"C:\Diagrams";

                // Define the output CSV file path.
                string outputCsv = Path.Combine(inputFolder, "DiagramHeaderFooterSummary.csv");

                // Prepare a list to hold CSV lines.
                List<string> csvLines = new List<string>();

                // Add CSV header.
                csvLines.Add("FileName,HeaderMargin,FooterMargin,FooterFontSize");

                // Get all files with Visio extensions in the folder.
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in diagramFiles)
                {
                    // Filter supported Visio file formats based on extension.
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        continue;
                    }

                    // Load the diagram using a using block to ensure proper disposal.
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Retrieve header and footer margins (in inches).
                        double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;
                        double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                        // Retrieve the footer font size (point size). HeaderFooterFont.Height stores the size as an integer.
                        int footerFontSize = diagram.HeaderFooter.HeaderFooterFont.Height;

                        // Build a CSV line with the required information.
                        string fileName = Path.GetFileName(filePath);
                        string line = $"{fileName},{headerMargin},{footerMargin},{footerFontSize}";
                        csvLines.Add(line);
                    }
                }

                // Write all lines to the CSV file.
                try
                {
                    File.WriteAllLines(outputCsv, csvLines);
                    Console.WriteLine($"Summary CSV generated at: {outputCsv}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing CSV file: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }