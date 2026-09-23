using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_paginated.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    // Determine the target page for navigation (next page, wrap to first)
                    int targetIndex = (i + 1) % diagram.Pages.Count;
                    Page targetPage = diagram.Pages[targetIndex];

                    // Add a simple rectangle shape that will act as the navigation button
                    // Position: top‑right corner of the page (adjust as needed)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
                    double rectWidth = 1.0;   // inches
                    double rectHeight = 0.5; // inches
                    double pinX = pageWidth - rectWidth / 2 - 0.2; // 0.2 inch margin from right edge
                    double pinY = pageHeight - rectHeight / 2 - 0.2; // 0.2 inch margin from top edge

                    long shapeId = page.DrawRectangle(pinX, pinY, rectWidth, rectHeight);
                    Shape navShape = page.Shapes.GetShape(shapeId);

                    // Set visible text on the shape
                    navShape.Text.Value.Clear();
                    navShape.Text.Value.Add(new Txt("Next Page"));

                    // Style the shape (optional)
                    navShape.Fill.FillForegnd.Value = "#DDEEFF";
                    navShape.Line.LineColor.Value = "#0000FF";

                    // Create a hyperlink that points to the target page
                    Hyperlink link = new Hyperlink();
                    link.Name = "NavLink";
                    link.Description.Value = $"Navigate to page \"{targetPage.Name}\"";
                    // SubAddress is used for internal page navigation; leave Address empty
                    link.SubAddress.Value = targetPage.Name;

                    // Add the hyperlink to the shape
                    navShape.Hyperlinks.Add(link);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }