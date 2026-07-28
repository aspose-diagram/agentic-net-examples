using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_with_navigation.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page to add navigation hyperlinks
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[pageIndex];

                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Common button size (in inches)
                double btnWidth = 1.0;
                double btnHeight = 0.5;

                // -------------------------------------------------------------
                // Add "Previous" button (if not the first page)
                // -------------------------------------------------------------
                if (pageIndex > 0)
                {
                    // Position the button at bottom‑left corner with a small margin
                    double pinXPrev = btnWidth / 2.0 + 0.2;
                    double pinYPrev = btnHeight / 2.0 + 0.2;

                    // Add a rectangle shape using the built‑in "Rectangle" master
                    long prevShapeId = page.AddShape(pinXPrev, pinYPrev, btnWidth, btnHeight, "Rectangle");
                    Shape prevShape = page.Shapes.GetShape((int)prevShapeId);

                    // Set visual appearance
                    prevShape.Fill.FillForegnd.Value = "#D3D3D3"; // light gray background
                    prevShape.Line.LinePattern.Value = LinePatternValue.Solid;
                    prevShape.Line.LineWeight.Value = 0.02;

                    // Set the button text
                    prevShape.Text.Value.Clear();
                    prevShape.Text.Value.Add(new Txt("Previous"));

                    // Create hyperlink to the previous page (internal link via SubAddress)
                    Hyperlink prevLink = new Hyperlink();
                    prevLink.SubAddress.Value = diagram.Pages[pageIndex - 1].Name; // target page name
                    prevLink.Description.Value = "Go to previous page";
                    prevShape.Hyperlinks.Add(prevLink);
                }

                // -------------------------------------------------------------
                // Add "Next" button (if not the last page)
                // -------------------------------------------------------------
                if (pageIndex < diagram.Pages.Count - 1)
                {
                    // Position the button at bottom‑right corner with a margin from the right edge
                    double pinXNext = pageWidth - (btnWidth / 2.0) - 0.2;
                    double pinYNext = btnHeight / 2.0 + 0.2;

                    // Add a rectangle shape for the "Next" button
                    long nextShapeId = page.AddShape(pinXNext, pinYNext, btnWidth, btnHeight, "Rectangle");
                    Shape nextShape = page.Shapes.GetShape((int)nextShapeId);

                    // Set visual appearance
                    nextShape.Fill.FillForegnd.Value = "#D3D3D3"; // light gray background
                    nextShape.Line.LinePattern.Value = LinePatternValue.Solid;
                    nextShape.Line.LineWeight.Value = 0.02;

                    // Set the button text
                    nextShape.Text.Value.Clear();
                    nextShape.Text.Value.Add(new Txt("Next"));

                    // Create hyperlink to the next page (internal link via SubAddress)
                    Hyperlink nextLink = new Hyperlink();
                    nextLink.SubAddress.Value = diagram.Pages[pageIndex + 1].Name; // target page name
                    nextLink.Description.Value = "Go to next page";
                    nextShape.Hyperlinks.Add(nextLink);
                }
            }

            // Save the modified diagram with navigation buttons
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}