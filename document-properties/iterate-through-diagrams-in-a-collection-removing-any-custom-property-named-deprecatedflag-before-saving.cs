using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files
                string inputFolder = @"C:\Visio\Input";
                // Output folder for processed files
                string outputFolder = @"C:\Visio\Output";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all Visio files (VSDX) in the input folder
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string filePath in diagramFiles)
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Access custom properties collection
                    var customProps = diagram.DocumentProps.CustomProps;

                    // Iterate backwards to safely remove items
                    for (int i = customProps.Count - 1; i >= 0; i--)
                    {
                        var prop = customProps[i];
                        // Remove property named "DeprecatedFlag"
                        if (prop.Name == "DeprecatedFlag")
                        {
                            customProps.Remove(prop);
                        }
                    }

                    // Prepare output file path
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }