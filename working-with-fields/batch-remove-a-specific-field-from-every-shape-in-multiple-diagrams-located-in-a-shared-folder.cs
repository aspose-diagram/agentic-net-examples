using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Name of the custom property (field) to remove from each shape
        private const string TargetFieldName = "MyField";

        static void Main(string[] args)
        {
            // Path to the folder containing the Visio files
            string folderPath = @"C:\VisioFiles";

            // Get all Visio files in the folder (adjust the pattern if needed)
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram using a using block to ensure proper disposal
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the current page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Ensure the shape has a Props collection
                                if (shape.Props != null)
                                {
                                    // Collect the properties that match the target field name
                                    List<Prop> propsToRemove = new List<Prop>();
                                    foreach (Prop prop in shape.Props)
                                    {
                                        if (prop.Name == TargetFieldName)
                                        {
                                            propsToRemove.Add(prop);
                                        }
                                    }

                                    // Remove the collected properties from the shape
                                    foreach (Prop prop in propsToRemove)
                                    {
                                        shape.Props.Remove(prop);
                                    }
                                }
                            }
                        }

                        // Save the modified diagram, overwriting the original file
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch field removal completed.");
        }
    }