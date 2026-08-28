using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process
            string folderPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path of the folder containing Visio files: ");
                folderPath = Console.ReadLine();
                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Folder does not exist. Exiting.");
                    return;
                }
            }

            // Get all Visio files (common extensions) in the folder
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                // Process only supported Visio formats
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Remove existing BatchId property if it exists
                        for (int i = diagram.DocumentProps.CustomProps.Count - 1; i >= 0; i--)
                        {
                            var existingProp = diagram.DocumentProps.CustomProps[i];
                            if (existingProp.Name == "BatchId")
                            {
                                diagram.DocumentProps.CustomProps.Remove(existingProp);
                            }
                        }

                        // Create a new custom property
                        var batchIdProp = new CustomProp();
                        batchIdProp.Name = "BatchId";
                        batchIdProp.PropType = PropType.String;
                        batchIdProp.CustomValue.ValueString = Guid.NewGuid().ToString();

                        // Add the custom property to the diagram
                        diagram.DocumentProps.CustomProps.Add(batchIdProp);

                        // Save the diagram, overwriting the original file
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
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
    }