using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Verify that a directory path was provided
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a directory path containing Visio files.");
                return;
            }

            string inputDirectory = args[0];

            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine($"The directory \"{inputDirectory}\" does not exist.");
                return;
            }

            // Define Visio file extensions to process
            string[] visioExtensions = new[] { ".vsd", ".vsdx", ".vdx", ".vss", ".vssx", ".vst", ".vstx", ".vsx", ".vtx", ".vdw" };

            // Get all Visio files in the directory (non‑recursive)
            var visioFiles = Directory.GetFiles(inputDirectory)
                                      .Where(f => visioExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                                      .ToList();

            if (!visioFiles.Any())
            {
                Console.WriteLine("No Visio files found in the specified directory.");
                return;
            }

            foreach (var visioFilePath in visioFiles)
            {
                try
                {
                    // Load the Visio diagram from file using the Diagram constructor (lifecycle rule)
                    using (var diagram = new Diagram(visioFilePath))
                    {
                        // Prepare an output stream for HTML (lifecycle rule)
                        using (var htmlStream = new MemoryStream())
                        {
                            // Save the diagram to the stream in HTML format (save rule)
                            diagram.Save(htmlStream, SaveFileFormat.Html);

                            // Determine output HTML file path (same name with .html extension)
                            string htmlFilePath = Path.ChangeExtension(visioFilePath, ".html");

                            // Write the stream content to the HTML file
                            File.WriteAllBytes(htmlFilePath, htmlStream.ToArray());

                            Console.WriteLine($"Converted \"{Path.GetFileName(visioFilePath)}\" to HTML successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to convert \"{Path.GetFileName(visioFilePath)}\": {ex.Message}");
                }
            }
        }
    }