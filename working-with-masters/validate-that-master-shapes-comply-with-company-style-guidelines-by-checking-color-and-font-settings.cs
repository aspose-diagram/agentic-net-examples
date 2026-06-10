using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the Visio file path as the first argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: MasterShapeValidator <visio-file-path>");
                    return;
                }

                string visioPath = args[0];

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Company style guidelines (example values)
                const string ExpectedFillColor = "#FF0000"; // Required fill foreground color
                var allowedFonts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "Calibri",
                    "Arial"
                };

                // Validate master shapes
                foreach (Master master in diagram.Masters)
                {
                    // Iterate all shapes that belong to the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // ----- Color validation -----
                        string shapeFillColor = shape.Fill.FillForegnd.Value;
                        if (!string.Equals(shapeFillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"[Color Violation] Master '{master.Name}' Shape ID {shape.ID} has fill color '{shapeFillColor}' (expected '{ExpectedFillColor}').");
                        }

                        // ----- Font validation on character runs -----
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string fontName = ch.FontName.Value;
                            if (!allowedFonts.Contains(fontName))
                            {
                                Console.WriteLine($"[Font Violation] Master '{master.Name}' Shape ID {shape.ID} uses font '{fontName}' which is not allowed.");
                            }
                        }
                    }
                }

                // ----- System font availability check -----
                // Enumerate installed system fonts using Aspose.Drawing.Text
                var installedFontCollection = new InstalledFontCollection();
                var installedFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var family in installedFontCollection.Families)
                {
                    // FontFamily type is not exposed; use the Name property directly
                    installedFontNames.Add(family.Name);
                }

                // Verify that every font referenced in the diagram is installed on the system
                foreach (Font diagramFont in diagram.Fonts)
                {
                    if (!installedFontNames.Contains(diagramFont.Name))
                    {
                        Console.WriteLine($"[Missing System Font] Diagram references font '{diagramFont.Name}' which is not installed on the system.");
                    }
                }

                Console.WriteLine("Master shape validation completed.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }