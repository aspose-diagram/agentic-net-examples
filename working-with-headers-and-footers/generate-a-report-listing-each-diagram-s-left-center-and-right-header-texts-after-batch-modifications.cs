using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. Use the first argument if provided,
            // otherwise default to the current working directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Find all Visio files (VSDX) in the folder.
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

            if (diagramFiles.Length == 0)
            {
                Console.WriteLine("No Visio files found in the specified folder.");
                return;
            }

            // Prepare a simple text report.
            string reportPath = Path.Combine(folderPath, "HeaderFooterReport.txt");
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("Visio Header/Footer Report");
                writer.WriteLine($"Generated on {DateTime.Now}");
                writer.WriteLine(new string('-', 50));

                foreach (string file in diagramFiles)
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(file);

                    // Retrieve header texts; treat null as empty string.
                    string left = diagram.HeaderFooter.HeaderLeft ?? string.Empty;
                    string center = diagram.HeaderFooter.HeaderCenter ?? string.Empty;
                    string right = diagram.HeaderFooter.HeaderRight ?? string.Empty;

                    // Output to console.
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Header Left   : {left}");
                    Console.WriteLine($"  Header Center : {center}");
                    Console.WriteLine($"  Header Right  : {right}");
                    Console.WriteLine();

                    // Write to the report file.
                    writer.WriteLine($"File: {Path.GetFileName(file)}");
                    writer.WriteLine($"  Header Left   : {left}");
                    writer.WriteLine($"  Header Center : {center}");
                    writer.WriteLine($"  Header Right  : {right}");
                    writer.WriteLine();
                }

                writer.WriteLine("Report generation completed.");
            }

            Console.WriteLine($"Report saved to: {reportPath}");
        }
    }