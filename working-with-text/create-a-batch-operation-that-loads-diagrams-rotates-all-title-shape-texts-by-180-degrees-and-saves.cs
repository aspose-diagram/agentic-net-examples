using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing diagram files; can be passed as first argument
            string inputFolder = args.Length > 0 ? args[0] : "Diagrams";

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Folder not found: {inputFolder}");
                return;
            }

            // Process Visio files with .vsdx extension (add other extensions if needed)
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Identify title shapes by universal name (case‑insensitive)
                            if (!string.IsNullOrEmpty(shape.NameU) &&
                                shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                            {
                                // Rotate the text block by 180 degrees (π radians)
                                shape.TextXForm.TxtAngle.Value = Math.PI;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    diagram.Dispose();

                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }