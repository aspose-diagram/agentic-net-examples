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
                Console.WriteLine("Please provide a directory path as an argument.");
                return;
            }

            string directoryPath = args[0];

            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"The directory \"{directoryPath}\" does not exist.");
                return;
            }

            // Supported Visio file extensions
            string[] visioExtensions = new[]
            {
                ".vsd", ".vsdx", ".vdx", ".vss", ".vssx", ".vst", ".vstx"
            };

            // Enumerate all Visio files in the directory (including subfolders)
            var visioFiles = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                                     .Where(f => visioExtensions.Contains(Path.GetExtension(f).ToLower()));

            foreach (string visioFilePath in visioFiles)
            {
                try
                {
                    // Load the Visio diagram from the file
                    using (Diagram diagram = new Diagram(visioFilePath))
                    {
                        // Prepare a memory stream for the HTML output
                        using (MemoryStream htmlStream = new MemoryStream())
                        {
                            // Save the diagram as HTML into the stream
                            diagram.Save(htmlStream, SaveFileFormat.Html);

                            // Reset stream position before reading
                            htmlStream.Position = 0;

                            // Determine the output HTML file path
                            string htmlFilePath = Path.ChangeExtension(visioFilePath, ".html");

                            // Write the HTML stream to the output file
                            using (FileStream fileStream = new FileStream(htmlFilePath, FileMode.Create, FileAccess.Write))
                            {
                                htmlStream.CopyTo(fileStream);
                            }

                            Console.WriteLine($"Converted \"{visioFilePath}\" to \"{htmlFilePath}\"");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to convert \"{visioFilePath}\": {ex.Message}");
                }
            }
        }
    }