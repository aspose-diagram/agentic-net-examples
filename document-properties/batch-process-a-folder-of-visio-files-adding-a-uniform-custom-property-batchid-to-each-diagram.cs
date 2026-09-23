using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for the folder containing Visio files
            Console.Write("Enter the full path to the folder with Visio files: ");
            string folderPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                Console.WriteLine("Invalid folder path.");
                return;
            }

            // Generate a uniform BatchId for this run (e.g., a GUID)
            string batchId = Guid.NewGuid().ToString();
            Console.WriteLine($"BatchId to apply: {batchId}");

            // Supported Visio extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm", ".vss", ".vst" };

            // Process each file in the folder
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                {
                    // Skip unsupported files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Remove any existing BatchId custom property
                    var customProps = diagram.DocumentProps.CustomProps;
                    for (int i = customProps.Count - 1; i >= 0; i--)
                    {
                        var prop = customProps[i];
                        if (prop.Name == "BatchId")
                        {
                            customProps.Remove(prop);
                        }
                    }

                    // Create and add the new BatchId custom property
                    CustomProp batchProp = new CustomProp();
                    batchProp.Name = "BatchId";
                    batchProp.PropType = PropType.String;
                    batchProp.CustomValue = new CustomValue();
                    batchProp.CustomValue.ValueString = batchId;
                    customProps.Add(batchProp);

                    // Determine the appropriate SaveFileFormat based on the file extension
                    SaveFileFormat saveFormat;
                    switch (ext)
                    {
                        case ".vsdx": saveFormat = SaveFileFormat.Vsdx; break;
                        case ".vsd":  saveFormat = SaveFileFormat.Vsd;  break;
                        case ".vdx":  saveFormat = SaveFileFormat.Vdx;  break;
                        case ".vsx":  saveFormat = SaveFileFormat.Vsx;  break;
                        case ".vtx":  saveFormat = SaveFileFormat.Vtx;  break;
                        case ".vssx": saveFormat = SaveFileFormat.Vssx; break;
                        case ".vstx": saveFormat = SaveFileFormat.Vstx; break;
                        case ".vsdm": saveFormat = SaveFileFormat.Vsdm; break;
                        case ".vssm": saveFormat = SaveFileFormat.Vssm; break;
                        case ".vstm": saveFormat = SaveFileFormat.Vstm; break;
                        case ".vss":  saveFormat = SaveFileFormat.Vss;  break;
                        case ".vst":  saveFormat = SaveFileFormat.Vst;  break;
                        default:
                            Console.WriteLine($"Unsupported file type: {filePath}");
                            continue;
                    }

                    // Save the diagram, overwriting the original file
                    diagram.Save(filePath, saveFormat);
                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }