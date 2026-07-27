using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Define the input diagram file paths.
                string[] diagramPaths = new string[]
                {
                    @"C:\Diagrams\Diagram1.vsdx",
                    @"C:\Diagrams\Diagram2.vsdx",
                    // Add more paths as needed.
                };

                // Process each diagram.
                foreach (string inputPath in diagramPaths)
                {
                    // Load the diagram from file.
                    Diagram diagram = new Diagram(inputPath);

                    // Remove any custom property named "DeprecatedFlag".
                    var customProps = diagram.DocumentProps.CustomProps;
                    // Iterate backwards to safely remove items while iterating.
                    for (int i = customProps.Count - 1; i >= 0; i--)
                    {
                        var prop = customProps[i];
                        if (prop.Name == "DeprecatedFlag")
                        {
                            customProps.Remove(prop);
                        }
                    }

                    // Define the output path (overwrite the original file in this example).
                    string outputPath = inputPath; // Change if you want a different location.

                    // Save the modified diagram using the appropriate SaveFileFormat.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }