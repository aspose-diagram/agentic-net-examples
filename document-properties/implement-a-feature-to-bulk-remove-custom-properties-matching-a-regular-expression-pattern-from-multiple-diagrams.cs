using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files
                string inputFolder = @"C:\Visio\Input";
                // Output folder where modified files will be saved
                string outputFolder = @"C:\Visio\Output";
                // Regular expression pattern to match custom property names
                string pattern = @"^Temp_.*$";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Compile the regex once for efficiency
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                // Process each Visio file in the input folder
                string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in files)
                {
                    // Only process supported Visio formats
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        Console.WriteLine($"Skipping unsupported file: {filePath}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Collect custom properties that match the pattern
                        var propsToRemove = new System.Collections.Generic.List<CustomProp>();
                        foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                        {
                            if (regex.IsMatch(prop.Name))
                            {
                                propsToRemove.Add(prop);
                            }
                        }

                        // Remove the matching custom properties
                        foreach (CustomProp prop in propsToRemove)
                        {
                            diagram.DocumentProps.CustomProps.Remove(prop);
                        }

                        // Save the modified diagram to the output folder
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);

                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }

                Console.WriteLine("Bulk removal operation completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }