using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: the folder containing diagrams and the name of the field to remove
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: BatchRemoveField <folderPath> <fieldName>");
                return;
            }

            string folderPath = args[0];
            string fieldName = args[1];

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all Visio files (VSDX) in the specified folder
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);
                    bool modified = false;

                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Props != null)
                            {
                                // Collect matching properties to remove (avoid modifying collection during iteration)
                                List<Prop> toRemove = new List<Prop>();
                                foreach (Prop prop in shape.Props)
                                {
                                    if (prop.Name == fieldName)
                                    {
                                        toRemove.Add(prop);
                                    }
                                }

                                // Remove the collected properties
                                foreach (Prop prop in toRemove)
                                {
                                    shape.Props.Remove(prop);
                                    modified = true;
                                }
                            }
                        }
                    }

                    // Save changes only if any field was removed
                    if (modified)
                    {
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Updated: {Path.GetFileName(filePath)}");
                    }
                    else
                    {
                        Console.WriteLine($"No changes needed: {Path.GetFileName(filePath)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }
    }