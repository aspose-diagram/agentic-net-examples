using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if line properties are inherited from the master/style
                        bool isColorInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                        bool isPatternInherited = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;
                        bool isWeightInherited = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

                        // Retrieve actual line property values
                        string lineColor = shape.Line.LineColor.Value;
                        LinePatternValue dashStyle = shape.Line.LinePattern.Value;
                        double thickness = shape.Line.LineWeight.Value; // inches

                        // Output the details
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Name: {shape.Name}");
                        Console.WriteLine($"  Line Color: {lineColor} (Inherited: {isColorInherited})");
                        Console.WriteLine($"  Dash Style (Pattern): {dashStyle} (Inherited: {isPatternInherited})");
                        Console.WriteLine($"  Thickness (Weight): {thickness} inches (Inherited: {isWeightInherited})");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }