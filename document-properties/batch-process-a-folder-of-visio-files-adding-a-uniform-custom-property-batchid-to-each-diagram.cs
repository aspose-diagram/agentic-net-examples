using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process; use first argument or current directory
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Generate a uniform BatchId for this run
            string batchId = Guid.NewGuid().ToString();
            Console.WriteLine($"BatchId for this run: {batchId}");

            // Supported Visio file extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vss", ".vstx", ".vst" };

            // Process each file in the folder
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                {
                    // Skip non-Visio files
                    continue;
                }

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Remove existing BatchId property if present
                        var customProps = diagram.DocumentProps.CustomProps;
                        for (int i = customProps.Count - 1; i >= 0; i--)
                        {
                            var existingProp = customProps[i];
                            if (existingProp.Name.Equals("BatchId", StringComparison.OrdinalIgnoreCase))
                            {
                                customProps.Remove(existingProp);
                            }
                        }

                        // Create and add the new BatchId custom property
                        CustomProp batchProp = new CustomProp();
                        batchProp.Name = "BatchId";
                        batchProp.PropType = PropType.String;
                        batchProp.CustomValue.ValueString = batchId;
                        customProps.Add(batchProp);

                        // Save the diagram back to its original format
                        SaveFileFormat format = GetSaveFileFormat(ext);
                        diagram.Save(filePath, format);
                    }

                    Console.WriteLine($"Processed file: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFileFormat(string extension)
        {
            return extension switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd"  => SaveFileFormat.Vsd,
                ".vdx"  => SaveFileFormat.Vdx,
                ".vsx"  => SaveFileFormat.Vsx,
                ".vtx"  => SaveFileFormat.Vtx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vss"  => SaveFileFormat.Vss,
                ".vstx" => SaveFileFormat.Vstx,
                ".vst"  => SaveFileFormat.Vst,
                _ => SaveFileFormat.Vsdx // Default fallback
            };
        }
    }