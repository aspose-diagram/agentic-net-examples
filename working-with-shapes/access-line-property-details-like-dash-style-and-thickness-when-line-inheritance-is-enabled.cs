using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with your actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has line inheritance enabled by comparing its line values
                        // with the inherited line values. If they are equal, the shape is inheriting
                        // the line formatting from its master or parent style.
                        bool isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                                               shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value &&
                                               shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;

                        if (isLineInherited)
                        {
                            // Retrieve dash style (LinePattern) and thickness (LineWeight) from the inherited line
                            LinePatternValue dashStyle = shape.InheritLine.LinePattern.Value;
                            double thickness = shape.InheritLine.LineWeight.Value; // thickness is in inches

                            // Output the details
                            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                            Console.WriteLine($"  Inherited Dash Style: {dashStyle}");
                            Console.WriteLine($"  Inherited Thickness: {thickness} inches");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }