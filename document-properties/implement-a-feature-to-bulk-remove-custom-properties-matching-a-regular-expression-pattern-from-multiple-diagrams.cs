using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get the folder containing diagrams
            string inputFolder;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                inputFolder = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder containing Visio files: ");
                inputFolder = Console.ReadLine();
                if (!Directory.Exists(inputFolder))
                {
                    Console.WriteLine("Folder does not exist. Exiting.");
                    return;
                }
            }

            // Get the regular expression pattern
            string pattern;
            if (args.Length > 1)
            {
                pattern = args[1];
            }
            else
            {
                Console.Write("Enter the regular expression pattern to match custom property names: ");
                pattern = Console.ReadLine();
            }

            Regex regex;
            try
            {
                regex = new Regex(pattern, RegexOptions.IgnoreCase);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid regex pattern: {ex.Message}");
                return;
            }

            // Supported Visio file extensions
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vsdm", ".vssx", ".vstx", ".vssm", ".vstm", ".vsd", ".vss", ".vst" };

            // Process each diagram file
            foreach (string filePath in Directory.GetFiles(inputFolder))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(extensions, ext) < 0)
                {
                    // Skip non-Visio files
                    continue;
                }

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Collect custom properties that match the pattern
                    var propsToRemove = new List<CustomProp>();
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

                    // Save the modified diagram (overwrite original)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)} (removed {propsToRemove.Count} properties)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Bulk removal operation completed.");
        }
    }