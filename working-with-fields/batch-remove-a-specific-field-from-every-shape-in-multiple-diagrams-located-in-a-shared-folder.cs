using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Folder containing the source Visio files
            string sourceFolder = @"C:\Diagrams";

            // Folder where the modified files will be saved
            string outputFolder = Path.Combine(sourceFolder, "Processed");
            Directory.CreateDirectory(outputFolder);

            // Name of the custom property (field) to remove from every shape
            const string targetFieldName = "MyField";

            // Process each Visio file in the source folder (adjust the search pattern if needed)
            foreach (string filePath in Directory.GetFiles(sourceFolder, "*.vsdx"))
            {
                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Ensure the Props collection is available
                                if (shape.Props != null)
                                {
                                    // Collect matching properties to remove (cannot modify collection while iterating)
                                    List<Prop> toRemove = new List<Prop>();
                                    foreach (Prop prop in shape.Props)
                                    {
                                        if (prop.Name == targetFieldName)
                                        {
                                            toRemove.Add(prop);
                                        }
                                    }

                                    // Remove the identified properties
                                    foreach (Prop prop in toRemove)
                                    {
                                        shape.Props.Remove(prop);
                                    }
                                }
                            }
                        }

                        // Build the output file path (overwrite the original name in the output folder)
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string outputPath = Path.Combine(outputFolder, fileName + ".vsdx");

                        // Save the modified diagram
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Processed and saved: {filePath}");
                }
                catch (Exception ex)
                {
                    // Log any errors for the current file and continue with the next one
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }