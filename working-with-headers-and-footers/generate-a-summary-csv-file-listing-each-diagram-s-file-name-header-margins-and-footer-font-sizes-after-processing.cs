using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Folder containing diagram files (adjust as needed)
            string diagramsFolder = @"C:\Diagrams";

            // Output CSV file path
            string csvOutputPath = Path.Combine(diagramsFolder, "summary.csv");

            // Prepare CSV writer
            using (var writer = new StreamWriter(csvOutputPath, false))
            {
                // Write CSV header
                writer.WriteLine("FileName,HeaderMargin,FooterMargin,FooterFontSize");

                // Get all files in the folder
                string[] allFiles = Directory.GetFiles(diagramsFolder);

                foreach (string filePath in allFiles)
                {
                    // Process only supported Visio file extensions
                    string ext = Path.GetExtension(filePath).ToLowerInvariant();
                    if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsdm" && ext != ".vssx")
                    {
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Retrieve header and footer margins (in inches)
                        double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;
                        double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                        // Retrieve footer font size (point size stored as integer)
                        int footerFontSize = diagram.HeaderFooter.HeaderFooterFont.Height;

                        // Write a line to the CSV
                        string line = $"{Path.GetFileName(filePath)},{headerMargin},{footerMargin},{footerFontSize}";
                        writer.WriteLine(line);
                    }
                    catch (Exception ex)
                    {
                        // Log any errors to console and continue processing other files
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Summary CSV generated at: {csvOutputPath}");
        }
    }