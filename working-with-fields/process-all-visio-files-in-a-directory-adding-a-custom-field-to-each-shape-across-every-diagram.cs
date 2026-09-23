using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the directory to process
            string folderPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path of the folder containing Visio files: ");
                folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("The specified folder does not exist.");
                    return;
                }
            }

            // Get all Visio files (common extensions)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                {
                    continue; // Skip non‑Visio files
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape is not deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Check if the custom property already exists
                            bool exists = false;
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == "MyCustomField")
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            if (!exists)
                            {
                                // Create a new custom property (Prop)
                                Prop customProp = new Prop();
                                customProp.Name = "MyCustomField";
                                customProp.Label.Value = "MyCustomField";
                                customProp.Value.Val = "CustomValue";
                                customProp.Type.Value = TypePropValue.String;

                                // Add the property to the shape
                                shape.Props.Add(customProp);
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }