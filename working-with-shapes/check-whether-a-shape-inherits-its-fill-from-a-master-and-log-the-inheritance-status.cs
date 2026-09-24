using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (provide via command line or use default)
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked for deletion
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape's fill properties match the inherited fill values
                        bool inheritsFill =
                            shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                            shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value &&
                            shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                        // Log the inheritance status
                        Console.WriteLine($"Shape ID {shape.ID}, NameU '{shape.NameU}': Inherits Fill = {inheritsFill}");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }