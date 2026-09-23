using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume the circle shape is on the first page
                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Ellipse" (a circle)
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        // Bring this shape to the front of the Z‑order
                        page.BringToFront(shape.ID);
                        break; // Circle found and moved; exit loop
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }