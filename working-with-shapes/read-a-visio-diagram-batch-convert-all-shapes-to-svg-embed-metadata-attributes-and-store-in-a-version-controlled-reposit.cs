using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = args.Length > 0 ? args[0] : @"C:\Visio\Input";
            // Output folder that acts as a simple version‑controlled repository
            string outputFolder = args.Length > 1 ? args[1] : @"C:\Visio\Repo";

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            Directory.CreateDirectory(outputFolder);

            // Process each Visio file in the input folder
            foreach (string visioPath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                try
                {
                    Console.WriteLine($"Processing file: {Path.GetFileName(visioPath)}");

                    // Load the diagram
                    Diagram diagram = new Diagram(visioPath);

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Embed a custom property with export timestamp
                            const string metaPropName = "ExportedOn";
                            bool propExists = false;
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == metaPropName)
                                {
                                    propExists = true;
                                    break;
                                }
                            }

                            if (!propExists)
                            {
                                Prop exportProp = new Prop();
                                exportProp.Name = metaPropName;
                                exportProp.Value.Val = DateTime.UtcNow.ToString("o");
                                // Set the property type to string
                                exportProp.Type.Value = TypePropValue.String;
                                shape.Props.Add(exportProp);
                            }

                            // Build a unique SVG file name: Diagram_Page_ShapeId.svg
                            string diagramName = Path.GetFileNameWithoutExtension(visioPath);
                            string pageName = string.IsNullOrEmpty(page.Name) ? $"Page{page.ID}" : page.Name;
                            string svgFileName = $"{diagramName}_{pageName}_Shape{shape.ID}.svg";
                            string svgPath = Path.Combine(outputFolder, svgFileName);

                            // Export the shape to SVG
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            shape.ToSvg(svgPath, svgOptions);
                        }
                    }

                    // Save the modified diagram (with embedded metadata) back to the repository
                    string savedDiagramPath = Path.Combine(outputFolder, Path.GetFileName(visioPath));
                    diagram.Save(savedDiagramPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch SVG export completed.");
        }
    }