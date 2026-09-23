using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the shared folder containing the Visio diagrams.
                // Adjust this path as needed or obtain it from command‑line arguments.
                string folderPath = @"C:\SharedDiagrams";

                // Name of the custom property (field) to remove from every shape.
                const string targetFieldName = "MyCustomField";

                // Get all Visio files with .vsdx extension in the folder.
                string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string diagramPath in diagramFiles)
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(diagramPath);

                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a Props collection.
                            if (shape.Props != null)
                            {
                                // Collect the properties that match the target name.
                                List<Prop> propsToRemove = new List<Prop>();
                                foreach (Prop prop in shape.Props)
                                {
                                    if (prop.Name == targetFieldName)
                                    {
                                        propsToRemove.Add(prop);
                                    }
                                }

                                // Remove the collected properties.
                                foreach (Prop prop in propsToRemove)
                                {
                                    shape.Props.Remove(prop);
                                }
                            }
                        }
                    }

                    // Save the modified diagram back to the same file.
                    diagram.Save(diagramPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Batch removal of field \"{0}\" completed for {1} files.", targetFieldName, diagramFiles.Length);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }