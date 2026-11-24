using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: VsdToCsvConverter <input_folder_path>");
            return;
        }

        string inputFolder = args[0];

        // Verify that the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Error: The folder \"{inputFolder}\" does not exist.");
            return;
        }

        // Get all .vsd files in the specified folder.
        string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

        if (vsdFiles.Length == 0)
        {
            Console.WriteLine("No VSD files found in the specified folder.");
            return;
        }

        // Process each VSD file.
        foreach (string vsdPath in vsdFiles)
        {
            // Guard against missing file (should not happen as we just enumerated them).
            if (!File.Exists(vsdPath))
            {
                Console.Error.WriteLine($"Skipping missing file: {vsdPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(vsdPath);

                // Determine output CSV file path (same name, .csv extension, placed in same folder).
                string csvFileName = Path.GetFileNameWithoutExtension(vsdPath) + ".csv";
                string csvPath = Path.Combine(inputFolder, csvFileName);

                // Save the diagram as CSV.
                diagram.Save(csvPath, SaveFileFormat.Csv);

                Console.WriteLine($"Successfully converted \"{Path.GetFileName(vsdPath)}\" to \"{csvFileName}\".");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or saving.
                Console.Error.WriteLine($"Error processing file \"{vsdPath}\": {ex.Message}");
            }
        }
    }
}