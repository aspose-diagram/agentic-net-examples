using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Variables to hold the input and output folder paths
        string inputFolder = null;
        string outputFolder = null;

        // Simple command‑line parsing
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-i":
                case "--input":
                    if (i + 1 < args.Length)
                    {
                        inputFolder = args[++i];
                    }
                    break;

                case "-o":
                case "--output":
                    if (i + 1 < args.Length)
                    {
                        outputFolder = args[++i];
                    }
                    break;
            }
        }

        // Validate arguments
        if (string.IsNullOrWhiteSpace(inputFolder) || string.IsNullOrWhiteSpace(outputFolder))
        {
            Console.WriteLine("Usage: app -i <inputFolder> -o <outputFolder>");
            return;
        }

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Optional: set custom font folders if required
        // FontConfigs.SetFontFolders(new[] { @"C:\Windows\Fonts" }, true);

        // Process each Visio file in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Filter supported Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsd" && ext != ".vsdx" && ext != ".vss" && ext != ".vssx")
                continue;

            try
            {
                // Load the diagram using default LoadOptions
                Diagram diagram = new Diagram(filePath, new LoadOptions());

                // Define the output file name (convert to PDF in this example)
                string outputFileName = Path.GetFileNameWithoutExtension(filePath) + ".pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the diagram to the desired format
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine($"Converted: {filePath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to convert '{filePath}': {ex.Message}");
            }
        }
    }
}
