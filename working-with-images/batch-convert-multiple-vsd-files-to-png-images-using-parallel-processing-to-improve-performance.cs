using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input folder and output folder
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VsdToPngBatch <inputFolder> <outputFolder>");
                return;
            }

            string inputFolder = args[0];
            string outputFolder = args[1];

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Get all .vsd files in the input folder (non‑recursive)
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd");

            // Process files in parallel to improve performance
            Parallel.ForEach(vsdFiles, vsdFile =>
            {
                try
                {
                    // Load the VSD diagram using the appropriate load format
                    using (Diagram diagram = new Diagram(vsdFile, LoadFileFormat.Vsd))
                    {
                        // Build output PNG file path
                        string pngFileName = Path.GetFileNameWithoutExtension(vsdFile) + ".png";
                        string pngPath = Path.Combine(outputFolder, pngFileName);

                        // Save the diagram as PNG
                        diagram.Save(pngPath, SaveFileFormat.Png);
                    }

                    Console.WriteLine($"Converted: {Path.GetFileName(vsdFile)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting {Path.GetFileName(vsdFile)}: {ex.Message}");
                }
            });

            Console.WriteLine("Batch conversion completed.");
        }
    }