using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram.
                Page page = diagram.Pages[0];

                // Find the first shape on the page.
                Shape targetShape = null;
                foreach (Shape s in page.Shapes)
                {
                    targetShape = s;
                    break;
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shapes were found on the first page.");
                    return;
                }

                // Ensure the shape is not marked as deleted.
                if (targetShape.Del == BOOL.True)
                {
                    Console.WriteLine("The selected shape is marked as deleted and cannot be modified.");
                    return;
                }

                // Apply a dashed line pattern to the shape.
                targetShape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Save the modified diagram.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Dashed line style applied and diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }