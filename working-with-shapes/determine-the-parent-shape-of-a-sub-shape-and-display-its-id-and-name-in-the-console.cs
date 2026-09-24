using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has a parent (i.e., it is a sub‑shape of a group)
                        if (shape.ParentShape != null)
                        {
                            Shape parent = shape.ParentShape;

                            // Display sub‑shape and its parent information
                            Console.WriteLine($"Sub‑shape ID: {shape.ID}, Name: {shape.Name}");
                            Console.WriteLine($"Parent shape ID: {parent.ID}, Name: {parent.Name}");
                            Console.WriteLine(); // Blank line for readability
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