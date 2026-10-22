using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input folder path
        string folderPath = args.Length > 0 ? args[0] : string.Empty;
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.WriteLine("Please provide the folder path containing Visio files as the first argument.");
            return;
        }

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Define supported Visio extensions
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst" };

        // Process each file in the folder
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, extension) < 0)
            {
                // Skip unsupported files
                continue;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Determine BatchId value (using file name without extension)
                string batchIdValue = Path.GetFileNameWithoutExtension(filePath);

                // Check if the custom property already exists
                bool propertyExists = false;
                foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
                {
                    if (string.Equals(existingProp.Name, "BatchId", StringComparison.OrdinalIgnoreCase))
                    {
                        // Update existing property's value
                        existingProp.CustomValue.ValueString = batchIdValue;
                        propertyExists = true;
                        break;
                    }
                }

                // If not existing, create and add a new custom property
                if (!propertyExists)
                {
                    CustomProp batchProp = new CustomProp();
                    batchProp.Name = "BatchId";
                    batchProp.PropType = PropType.String;
                    batchProp.CustomValue.ValueString = batchIdValue;
                    diagram.DocumentProps.CustomProps.Add(batchProp);
                }

                // Determine appropriate SaveFileFormat based on original extension
                SaveFileFormat format = GetSaveFileFormat(extension);

                // Save the diagram, overwriting the original file
                diagram.Save(filePath, format);

                // Dispose diagram to free resources
                diagram.Dispose();

                Console.WriteLine($"Processed file: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            default:
                // Default to Vsdx if unknown (should not happen due to prior filtering)
                return SaveFileFormat.Vsdx;
        }
    }
}