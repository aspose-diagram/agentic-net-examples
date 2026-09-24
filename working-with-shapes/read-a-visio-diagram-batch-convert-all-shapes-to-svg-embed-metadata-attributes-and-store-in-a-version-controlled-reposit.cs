using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (first argument) and output root folder (second argument)
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioBatchSvgExport <inputVisioPath> <outputRootFolder>");
                return;
            }

            string inputVisioPath = args[0];
            string outputRootFolder = args[1];

            if (!File.Exists(inputVisioPath))
            {
                Console.WriteLine($"Input file not found: {inputVisioPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create a version‑specific subfolder using the diagram's built‑in version string
            string versionFolderName = $"Version_{diagram.Version.Replace(' ', '_')}";
            string exportFolder = Path.Combine(outputRootFolder, versionFolderName);
            Directory.CreateDirectory(exportFolder);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique SVG file name per shape
                    string svgFileName = $"Page{page.ID}_Shape{shape.ID}.svg";
                    string svgFilePath = Path.Combine(exportFolder, svgFileName);

                    // Export the shape to SVG
                    SVGSaveOptions svgOptions = new SVGSaveOptions
                    {
                        ExportHiddenPage = false,
                        ExportGuideShapes = false,
                        SVGFitToViewPort = true
                    };
                    shape.ToSvg(svgFilePath, svgOptions);

                    // Embed metadata into the generated SVG file
                    try
                    {
                        XDocument svgDoc = XDocument.Load(svgFilePath);
                        XElement metadata = new XElement("metadata",
                            new XElement("DiagramVersion", diagram.Version),
                            new XElement("PageID", page.ID),
                            new XElement("ShapeID", shape.ID),
                            new XElement("ShapeNameU", shape.NameU ?? string.Empty));

                        // Add any custom document properties as additional metadata entries
                        foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
                        {
                            metadata.Add(new XElement(prop.Name, prop.CustomValue.ValueString ?? string.Empty));
                        }

                        // Insert the metadata element as the first child of the root <svg> element
                        XElement rootSvg = svgDoc.Root;
                        if (rootSvg != null)
                        {
                            rootSvg.AddFirst(metadata);
                            svgDoc.Save(svgFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to embed metadata for shape {shape.ID}: {ex.Message}");
                    }
                }
            }

            // Add a custom property to record the export timestamp
            CustomProp exportTimeProp = new CustomProp
            {
                Name = "LastExportTime",
                PropType = PropType.String,
                CustomValue = { ValueString = DateTime.UtcNow.ToString("o") }
            };
            diagram.DocumentProps.CustomProps.Add(exportTimeProp);

            // Save the diagram back (optional, preserves any added metadata)
            string diagramSavePath = Path.Combine(outputRootFolder, $"Exported_{Path.GetFileName(inputVisioPath)}");
            diagram.Save(diagramSavePath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Export completed. SVG files are located in: {exportFolder}");
        }
    }