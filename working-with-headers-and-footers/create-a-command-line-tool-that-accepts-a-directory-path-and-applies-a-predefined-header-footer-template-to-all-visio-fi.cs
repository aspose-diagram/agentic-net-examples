using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Verify that a directory path was provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: VisioHeaderFooterTool <directoryPath>");
                return;
            }

            string directoryPath = args[0];

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Error: Directory does not exist - {directoryPath}");
                return;
            }

            // Supported Visio extensions and their corresponding SaveFileFormat values
            var formatMap = new Dictionary<string, SaveFileFormat>(StringComparer.OrdinalIgnoreCase)
            {
                { ".vsdx", SaveFileFormat.Vsdx },
                { ".vsd",  SaveFileFormat.Vsd },
                { ".vdx",  SaveFileFormat.Vdx },
                { ".vsx",  SaveFileFormat.Vsx },
                { ".vtx",  SaveFileFormat.Vtx },
                { ".vssx", SaveFileFormat.Vssx },
                { ".vss",  SaveFileFormat.Vss },
                { ".vstx", SaveFileFormat.Vstx },
                { ".vst",  SaveFileFormat.Vst },
                { ".vssm", SaveFileFormat.Vssm },
                { ".vstm", SaveFileFormat.Vstm },
                { ".vsdm", SaveFileFormat.Vsdm }
            };

            // Retrieve all files in the directory (non‑recursive)
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath);
                if (!formatMap.ContainsKey(extension))
                {
                    // Skip non‑Visio files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply header/footer template
                    diagram.HeaderFooter.HeaderLeft   = "Company Name";
                    diagram.HeaderFooter.HeaderCenter = "Confidential";
                    diagram.HeaderFooter.HeaderRight  = "Created: &d";

                    diagram.HeaderFooter.FooterLeft   = "Document ID: 12345";
                    diagram.HeaderFooter.FooterCenter = "Page &p of &P";
                    diagram.HeaderFooter.FooterRight  = "Generated on &d";

                    // Set margins (in inches)
                    diagram.HeaderFooter.HeaderMargin.Value = 0.5;
                    diagram.HeaderFooter.FooterMargin.Value = 0.5;

                    // Configure global font for header/footer
                    var hfFont = diagram.HeaderFooter.HeaderFooterFont;
                    hfFont.FaceName = "Arial";
                    hfFont.Weight   = 700;   // Bold
                    hfFont.Height   = -16;   // Approx. 12pt (negative mapping per specification)

                    // Save the diagram back in its original format
                    SaveFileFormat saveFormat = formatMap[extension];
                    diagram.Save(filePath, saveFormat);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Header/footer application completed.");
        }
    }