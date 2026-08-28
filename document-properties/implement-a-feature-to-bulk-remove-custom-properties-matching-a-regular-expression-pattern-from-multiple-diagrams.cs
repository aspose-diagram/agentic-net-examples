using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            // args[0] - folder path containing diagram files
            // args[1] - regular expression pattern to match custom property names
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: BulkCustomPropertyRemoval <folderPath> <regexPattern>");
                return;
            }

            string folderPath = args[0];
            string pattern = args[1];

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all Visio files (VSDX, VSD, VDX, etc.) in the folder
            string[] diagramFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in diagramFiles)
            {
                // Filter supported Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Access custom properties collection
                    var customProps = diagram.DocumentProps.CustomProps;

                    // Iterate over a copy of the collection to avoid modification during enumeration
                    var propsToRemove = new System.Collections.Generic.List<CustomProp>();
                    foreach (CustomProp prop in customProps)
                    {
                        if (prop != null && !string.IsNullOrEmpty(prop.Name))
                        {
                            if (Regex.IsMatch(prop.Name, pattern))
                            {
                                propsToRemove.Add(prop);
                            }
                        }
                    }

                    // Remove matching properties
                    foreach (CustomProp prop in propsToRemove)
                    {
                        customProps.Remove(prop);
                        Console.WriteLine($"Removed custom property '{prop.Name}' from file '{Path.GetFileName(filePath)}'.");
                    }

                    // Save the diagram back (overwrite original file)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    diagram.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Bulk removal completed.");
        }
    }