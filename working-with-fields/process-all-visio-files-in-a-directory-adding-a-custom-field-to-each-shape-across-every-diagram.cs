using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory to process
        string directoryPath;
        if (args.Length > 0)
        {
            directoryPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the directory containing Visio files: ");
            directoryPath = Console.ReadLine();
        }

        // Guard: directory must exist
        if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine("The specified directory does not exist.");
            return;
        }

        // Define supported Visio extensions
        string[] supportedExtensions = new string[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vsdm", ".vssx", ".vss", ".vstx",
            ".vst", ".vssm", ".vstm"
        };

        // Get all files with supported extensions
        string[] allFiles = Directory.GetFiles(directoryPath);
        foreach (string filePath in allFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(supportedExtensions, ext) < 0)
            {
                // Skip non-Visio files
                continue;
            }

            // Guard: file must exist (redundant but per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a new custom property (Prop)
                        Prop customProp = new Prop();
                        customProp.Name = "MyCustomField";
                        customProp.Label.Value = "My Custom Field";
                        customProp.Value.Val = "DefaultValue";
                        customProp.Type.Value = TypePropValue.String;

                        // Add the custom property to the shape
                        shape.Props.Add(customProp);
                    }
                }

                // Determine the appropriate SaveFileFormat based on extension
                SaveFileFormat format;
                switch (ext)
                {
                    case ".vsdx":
                        format = SaveFileFormat.Vsdx;
                        break;
                    case ".vsd":
                        format = SaveFileFormat.Vsd;
                        break;
                    case ".vsx":
                        format = SaveFileFormat.Vsx;
                        break;
                    case ".vtx":
                        format = SaveFileFormat.Vtx;
                        break;
                    case ".vsdm":
                        format = SaveFileFormat.Vsdm;
                        break;
                    case ".vssx":
                        format = SaveFileFormat.Vssx;
                        break;
                    case ".vss":
                        format = SaveFileFormat.Vss;
                        break;
                    case ".vstx":
                        format = SaveFileFormat.Vstx;
                        break;
                    case ".vst":
                        format = SaveFileFormat.Vst;
                        break;
                    case ".vssm":
                        format = SaveFileFormat.Vssm;
                        break;
                    case ".vstm":
                        format = SaveFileFormat.Vstm;
                        break;
                    default:
                        // Unsupported extension; skip saving
                        Console.Error.WriteLine($"Unsupported file format for: {filePath}");
                        diagram.Dispose();
                        continue;
                }

                // Save the modified diagram (overwrites original file)
                diagram.Save(filePath, format);

                // Release resources
                diagram.Dispose();

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}