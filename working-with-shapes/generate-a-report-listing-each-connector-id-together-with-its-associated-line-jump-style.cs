using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file. Adjust as needed or pass as a command‑line argument.
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                Console.WriteLine("Connector ID | Line Jump Style");
                Console.WriteLine("-----------------------------");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Filter only 1‑D connector shapes that are not deleted.
                        if (shape.OneD && shape.Del == BOOL.False)
                        {
                            // Retrieve the line jump style from the shape's layout.
                            var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                            // Output the connector ID and its line jump style.
                            Console.WriteLine($"{shape.ID,12} | {jumpStyle}");
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }