using System;
using System.IO;
using System.Globalization;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Folder containing Visio files (adjust as needed)
            string inputFolder = Directory.GetCurrentDirectory();
            // Output CSV file path
            string csvPath = Path.Combine(inputFolder, "summary.csv");

            // Prepare CSV header
            using (var writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("FileName,HeaderMarginInches,FooterMarginInches,FooterFontSizePoints");

                // Process each Visio file in the folder
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
                foreach (string filePath in visioFiles)
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Retrieve header and footer margins (in inches)
                    double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;
                    double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                    // Retrieve footer font size (points). Height property stores point size as integer.
                    int footerFontSize = diagram.HeaderFooter.HeaderFooterFont.Height;

                    // Write CSV line
                    string line = string.Format(CultureInfo.InvariantCulture,
                        "\"{0}\",{1:F3},{2:F3},{3}",
                        Path.GetFileName(filePath),
                        headerMargin,
                        footerMargin,
                        footerFontSize);
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine("Summary CSV generated at: " + csvPath);
        }
    }