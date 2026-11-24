using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Path to the output Visio file (optional).
        string outputPath = "output.vsdx";

        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Ensure the diagram has at least one page and one shape.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page.
                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes.");
                    return;
                }

                // Access the first shape on the page.
                Shape shape = page.Shapes[0];

                // -----------------------------------------------------------------
                // Set custom width and height for the shape's text block.
                // Width and height are in inches.
                // -----------------------------------------------------------------
                double customTextWidth = 2.0;   // example width
                double customTextHeight = 1.0;  // example height

                shape.TextXForm.TxtWidth.Value = customTextWidth;
                shape.TextXForm.TxtHeight.Value = customTextHeight;

                // -----------------------------------------------------------------
                // Retrieve the text block's pin position (center of the text block).
                // -----------------------------------------------------------------
                double pinX = shape.TextXForm.TxtPinX.Value;
                double pinY = shape.TextXForm.TxtPinY.Value;

                // -----------------------------------------------------------------
                // Calculate the bounding box of the text block.
                // Left = PinX - Width/2, Right = PinX + Width/2
                // Bottom = PinY - Height/2, Top = PinY + Height/2
                // -----------------------------------------------------------------
                double left   = pinX - customTextWidth / 2.0;
                double right  = pinX + customTextWidth / 2.0;
                double bottom = pinY - customTextHeight / 2.0;
                double top    = pinY + customTextHeight / 2.0;

                // Output the calculated bounding box.
                Console.WriteLine("Text block bounding box (in inches):");
                Console.WriteLine($"Left   : {left}");
                Console.WriteLine($"Right  : {right}");
                Console.WriteLine($"Bottom : {bottom}");
                Console.WriteLine($"Top    : {top}");

                // -----------------------------------------------------------------
                // Optionally save the modified diagram to a new file.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}