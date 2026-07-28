using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the input Visio file path.
                // If a path is passed as a command‑line argument, use it; otherwise use a default placeholder.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");
                    Console.WriteLine(new string('-', 60));

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve required properties.
                        long shapeId = shape.ID;
                        string shapeName = shape.Name;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Output the shape information.
                        Console.WriteLine($"ID: {shapeId}");
                        Console.WriteLine($"Name: {shapeName}");
                        Console.WriteLine($"Width: {width} in");
                        Console.WriteLine($"Height: {height} in");
                        Console.WriteLine($"PinX (absolute): {pinX} in");
                        Console.WriteLine($"PinY (absolute): {pinY} in");
                        Console.WriteLine(new string('-', 30));
                    }

                    Console.WriteLine(); // Blank line between pages.
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }