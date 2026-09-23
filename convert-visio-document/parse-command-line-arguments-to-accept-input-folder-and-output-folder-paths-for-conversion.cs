using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect exactly two arguments: input folder and output folder
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramConverter <inputFolder> <outputFolder>");
                return;
            }

            // Parse command line arguments
            string inputFolder = args[0];
            string outputFolder = args[1];

            // Validate input folder
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Error: Input folder \"{inputFolder}\" does not exist.");
                return;
            }

            // Ensure output folder exists; create if it does not
            if (!Directory.Exists(outputFolder))
            {
                try
                {
                    Directory.CreateDirectory(outputFolder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: Could not create output folder \"{outputFolder}\". {ex.Message}");
                    return;
                }
            }

            // Example conversion loop (placeholder for actual conversion logic)
            // This demonstrates loading each Visio file from the input folder and saving it to the output folder.
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
            foreach (string filePath in visioFiles)
            {
                try
                {
                    // Load the Visio diagram using Aspose.Diagram
                    Diagram diagram = new Diagram(filePath);

                    // Determine output file path (same name with .pdf extension as an example)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputFilePath = Path.Combine(outputFolder, fileNameWithoutExt + ".pdf");

                    // Save the diagram to PDF (or any other supported format)
                    diagram.Save(outputFilePath, SaveFileFormat.Pdf);

                    Console.WriteLine($"Converted: {Path.GetFileName(filePath)} -> {Path.GetFileName(outputFilePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to convert \"{filePath}\": {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }