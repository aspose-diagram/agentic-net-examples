using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <directoryPath>");
            return;
        }

        string directoryPath = args[0];

        // Verify the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory does not exist: {directoryPath}");
            return;
        }

        // Define Visio file extensions to process
        string[] visioExtensions = new string[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vsdm", ".vssx", ".vss", ".vstx",
            ".vst", ".vssm", ".vstm"
        };

        // Get all files in the directory
        string[] allFiles = Directory.GetFiles(directoryPath);

        foreach (string filePath in allFiles)
        {
            // Process only supported Visio files
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(visioExtensions, ext) < 0)
            {
                continue;
            }

            // Guard against missing file (should not happen after GetFiles, but per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram inside a using block for proper disposal
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the custom property already exists
                            bool propertyExists = false;
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == "MyCustomField")
                                {
                                    propertyExists = true;
                                    break;
                                }
                            }

                            // Add the custom property if it does not exist
                            if (!propertyExists)
                            {
                                Prop customProp = new Prop();
                                customProp.Name = "MyCustomField";
                                customProp.Label.Value = "My Custom Field";
                                customProp.Value.Val = "DefaultValue";
                                customProp.Type.Value = TypePropValue.String;
                                shape.Props.Add(customProp);
                            }
                        }
                    }

                    // Determine the appropriate save format based on file extension
                    SaveFileFormat saveFormat = SaveFileFormat.Vsdx; // default fallback
                    switch (ext)
                    {
                        case ".vsdx":
                            saveFormat = SaveFileFormat.Vsdx;
                            break;
                        case ".vsd":
                            saveFormat = SaveFileFormat.Vsd;
                            break;
                        case ".vsx":
                            saveFormat = SaveFileFormat.Vsx;
                            break;
                        case ".vtx":
                            saveFormat = SaveFileFormat.Vtx;
                            break;
                        case ".vsdm":
                            saveFormat = SaveFileFormat.Vsdm;
                            break;
                        case ".vssx":
                            saveFormat = SaveFileFormat.Vssx;
                            break;
                        case ".vss":
                            saveFormat = SaveFileFormat.Vss;
                            break;
                        case ".vstx":
                            saveFormat = SaveFileFormat.Vstx;
                            break;
                        case ".vst":
                            saveFormat = SaveFileFormat.Vst;
                            break;
                        case ".vssm":
                            saveFormat = SaveFileFormat.Vssm;
                            break;
                        case ".vstm":
                            saveFormat = SaveFileFormat.Vstm;
                            break;
                        default:
                            Console.Error.WriteLine($"Unsupported Visio format for file: {filePath}");
                            continue;
                    }

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, saveFormat);
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}