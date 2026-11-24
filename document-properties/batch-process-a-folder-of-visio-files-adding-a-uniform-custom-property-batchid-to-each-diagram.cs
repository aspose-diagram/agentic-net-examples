using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: first argument or prompt the user.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the folder containing Visio files: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Define supported Visio extensions and their corresponding SaveFileFormat values.
        var supportedExtensions = new (string Ext, SaveFileFormat Format)[]
        {
            (".vsdx", SaveFileFormat.Vsdx),
            (".vsd",  SaveFileFormat.Vsd),
            (".vsx",  SaveFileFormat.Vsx),
            (".vtx",  SaveFileFormat.Vtx),
            (".vssx", SaveFileFormat.Vssx),
            (".vss",  SaveFileFormat.Vss),
            (".vstx", SaveFileFormat.Vstx),
            (".vst",  SaveFileFormat.Vst)
        };

        // Retrieve all files in the folder.
        string[] allFiles = Directory.GetFiles(folderPath);
        foreach (string filePath in allFiles)
        {
            // Guard: ensure the file still exists (it could have been removed meanwhile).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found, skipping: " + filePath);
                continue;
            }

            // Determine the file extension in lower case.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();

            // Find a matching SaveFileFormat for the extension.
            SaveFileFormat? targetFormat = null;
            foreach (var pair in supportedExtensions)
            {
                if (pair.Ext == ext)
                {
                    targetFormat = pair.Format;
                    break;
                }
            }

            // If the extension is not supported, skip the file.
            if (targetFormat == null)
            {
                Console.WriteLine("Unsupported file type, skipping: " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block to ensure proper disposal.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Create a new custom property named "BatchId".
                    CustomProp batchProp = new CustomProp();
                    batchProp.Name = "BatchId";
                    // Set the property type to String.
                    batchProp.PropType = PropType.String;
                    // Assign a uniform value to the property.
                    batchProp.CustomValue.ValueString = "Batch_001";

                    // Add the custom property to the document's custom properties collection.
                    diagram.DocumentProps.CustomProps.Add(batchProp);

                    // Save the diagram back to the original file, preserving its format.
                    diagram.Save(filePath, targetFormat.Value);
                }

                Console.WriteLine("Processed file successfully: " + filePath);
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}