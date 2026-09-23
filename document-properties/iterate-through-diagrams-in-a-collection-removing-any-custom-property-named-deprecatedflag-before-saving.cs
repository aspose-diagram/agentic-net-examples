using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Define the folder containing the Visio files
            string inputFolder = @"C:\VisioFiles\Input";
            string outputFolder = @"C:\VisioFiles\Output";

            // Ensure the output folder exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get all Visio files (VSDX, VDX, VSD) in the input folder
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            List<string> supportedExtensions = new List<string> { ".vsdx", ".vsd", ".vdx" };

            foreach (string filePath in visioFiles)
            {
                if (!supportedExtensions.Contains(Path.GetExtension(filePath).ToLower()))
                    continue; // Skip non‑Visio files

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Remove custom properties named "DeprecatedFlag"
                    var customProps = diagram.DocumentProps.CustomProps;
                    // Collect properties to remove to avoid modifying the collection while iterating
                    List<CustomProp> propsToRemove = new List<CustomProp>();
                    foreach (CustomProp prop in customProps)
                    {
                        if (prop.Name == "DeprecatedFlag")
                        {
                            propsToRemove.Add(prop);
                        }
                    }

                    foreach (CustomProp prop in propsToRemove)
                    {
                        customProps.Remove(prop);
                    }

                    // Save the modified diagram to the output folder (overwrite if exists)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
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
