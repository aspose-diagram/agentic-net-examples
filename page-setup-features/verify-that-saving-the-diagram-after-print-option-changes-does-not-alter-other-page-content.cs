using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Prepare containers to hold original page content for later verification
        // -----------------------------------------------------------------
        List<long> originalShapeIds = new List<long>();
        List<string> originalShapeTexts = new List<string>();

        // -----------------------------------------------------------------
        // Load diagram, capture original page content, modify print options,
        // and save the diagram.
        // -----------------------------------------------------------------
        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture content of the first page before any changes
            Page originalPage = diagram.Pages[0];
            foreach (Shape shape in originalPage.Shapes)
            {
                originalShapeIds.Add(shape.ID);
                // Retrieve plain text; handle possible nulls safely
                string txt = shape.Text?.Value?.Text ?? string.Empty;
                originalShapeTexts.Add(txt);
            }

            // Modify print options on the first page
            PrintProps printProps = originalPage.PageSheet.PrintProps;
            // Set orientation to Landscape
            printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
            // Set scaling factors (100%)
            printProps.ScaleX.Value = 1.0;
            printProps.ScaleY.Value = 1.0;
            // Set margins (0.5 inches on each side)
            printProps.PageLeftMargin.Value = 0.5;
            printProps.PageRightMargin.Value = 0.5;
            printProps.PageTopMargin.Value = 0.5;
            printProps.PageBottomMargin.Value = 0.5;
            // Enable fit to page (optional)
            printProps.OnPage.Value = BOOL.True;

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification and saving: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the saved diagram and verify that page content is unchanged.
        // -----------------------------------------------------------------
        try
        {
            // Load the saved diagram
            Diagram savedDiagram = new Diagram(outputPath);

            // Capture content of the first page after saving
            Page savedPage = savedDiagram.Pages[0];
            List<long> savedShapeIds = new List<long>();
            List<string> savedShapeTexts = new List<string>();
            foreach (Shape shape in savedPage.Shapes)
            {
                savedShapeIds.Add(shape.ID);
                string txt = shape.Text?.Value?.Text ?? string.Empty;
                savedShapeTexts.Add(txt);
            }

            // Verify that the number of shapes matches
            if (originalShapeIds.Count != savedShapeIds.Count)
            {
                throw new Exception("Shape count mismatch after saving.");
            }

            // Verify each shape ID and text are unchanged
            for (int i = 0; i < originalShapeIds.Count; i++)
            {
                if (originalShapeIds[i] != savedShapeIds[i])
                {
                    throw new Exception($"Shape ID mismatch at index {i}: original={originalShapeIds[i]}, saved={savedShapeIds[i]}");
                }

                if (originalShapeTexts[i] != savedShapeTexts[i])
                {
                    throw new Exception($"Shape text mismatch for shape ID {originalShapeIds[i]}: original=\"{originalShapeTexts[i]}\", saved=\"{savedShapeTexts[i]}\"");
                }
            }

            Console.WriteLine("Verification succeeded: page content unchanged after modifying print options and saving.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}
