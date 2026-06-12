using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape has a custom line (line inheritance disabled)
                        // Compare the shape's line color with the inherited line color
                        // If they differ, the line is not inherited
                        if (shape.Line.LineColor.Value != shape.InheritLine.LineColor.Value)
                        {
                            // Apply a custom highlight color (e.g., bright red)
                            shape.Line.LineColor.Value = "#FF0000";
                        }
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }