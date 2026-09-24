using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to validate
                string diagramPath = "input.vsdx";

                // Define the corporate palette (hex or RGB string formats)
                var allowedColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "#FF0000", // Red
                    "#00FF00", // Green
                    "#0000FF", // Blue
                    "RGB(255,0,0)",
                    "RGB(0,255,0)",
                    "RGB(0,0,255)"
                };

                // Load the diagram
                using (FileStream stream = new FileStream(diagramPath, FileMode.Open, FileAccess.Read))
                {
                    Diagram diagram = new Diagram(stream);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the text background color definition, if any
                            string bgColor = shape.TextBlock?.TextBkgnd?.Ufe?.F;

                            // If the shape has no text background defined, skip it
                            if (string.IsNullOrWhiteSpace(bgColor))
                                continue;

                            // Validate against the corporate palette
                            if (!allowedColors.Contains(bgColor.Trim()))
                            {
                                // Report the violation
                                Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has an invalid text background color: {bgColor}");

                                // Optionally, throw to stop processing
                                // throw new Exception($"Invalid text background color on shape ID {shape.ID}");
                            }
                        }
                    }
                }

                Console.WriteLine("Validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }