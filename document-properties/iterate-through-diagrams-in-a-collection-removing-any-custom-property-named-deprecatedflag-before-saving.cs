using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

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

                // Get all Visio files (adjust extension as needed)
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string filePath in diagramFiles)
                {
                    // Load diagram using the constructor that accepts a file path
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Access custom properties collection
                        CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                        // Collect properties named "DeprecatedFlag" to remove
                        List<CustomProp> propsToRemove = new List<CustomProp>();
                        foreach (CustomProp prop in customProps)
                        {
                            if (prop.Name == "DeprecatedFlag")
                            {
                                propsToRemove.Add(prop);
                            }
                        }

                        // Remove the identified custom properties
                        foreach (CustomProp prop in propsToRemove)
                        {
                            customProps.Remove(prop);
                        }

                        // Save the modified diagram to the output folder
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                }

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }