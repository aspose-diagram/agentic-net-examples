using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    bool setLandscape = false;

                    // Simple content analysis: if any shape on the page is wider than tall,
                    // switch the page orientation to Landscape; otherwise keep Portrait.
                    foreach (Shape shape in page.Shapes)
                    {
                        double shapeWidth = shape.XForm.Width.Value;
                        double shapeHeight = shape.XForm.Height.Value;

                        if (shapeWidth > shapeHeight)
                        {
                            setLandscape = true;
                            break;
                        }
                    }

                    // Apply the determined orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value =
                        setLandscape ? PrintPageOrientationValue.Landscape : PrintPageOrientationValue.Portrait;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
