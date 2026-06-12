using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            // Check the current jump style of the connector.
                            // If it is the default (no explicit jump) or undefined,
                            // change it to the square jump style.
                            var currentJumpStyle = shape.Layout.ConLineJumpStyle.Value;
                            if (currentJumpStyle == ConLineJumpStyleValue.PageDefault ||
                                currentJumpStyle == ConLineJumpStyleValue.Undefined)
                            {
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Square;
                            }
                        }
                    }
                }

                // Save the modified diagram.
                // Replace "output.vsdx" with the desired output path.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }