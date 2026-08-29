using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU}");

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // ----- Fill inheritance check -----
                        bool inheritsFill =
                            shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                            shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value &&
                            shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                        // ----- Line inheritance check -----
                        bool inheritsLine =
                            shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                            shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value &&
                            shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value &&
                            shape.Line.BeginArrow.Value == shape.InheritLine.BeginArrow.Value &&
                            shape.Line.EndArrow.Value == shape.InheritLine.EndArrow.Value;

                        // Determine if the shape fully inherits formatting (both fill and line)
                        bool fullyInherits = inheritsFill && inheritsLine;

                        // Output the result
                        string result = fullyInherits ? "fully inherits formatting" : "has custom formatting";
                        Console.WriteLine($"  Shape ID {shape.ID} (NameU: {shape.NameU}) {result}.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }