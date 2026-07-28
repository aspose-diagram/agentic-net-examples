using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (can be an existing diagram or a blank one)
        string sourcePath = "input.vsdx";
        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Path where the diagram with the generated legend will be saved
        string outputPath = "output_with_legend.vsdx";

        try
        {
            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourcePath);

            // Collect distinct external data categories from DataRecordSets
            HashSet<string> categories = new HashSet<string>();
            foreach (DataRecordSet recordSet in diagram.DataRecordSets)
            {
                // Use the name of the record set as the category identifier (NameU is not available)
                if (!string.IsNullOrWhiteSpace(recordSet.Name))
                {
                    categories.Add(recordSet.Name);
                }
            }

            // If there are no categories, nothing to add
            if (categories.Count == 0)
            {
                Console.WriteLine("No external data categories found. Legend will not be created.");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                return;
            }

            // Use the first page (or create one if none exists)
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                page = new Page(1);
                diagram.Pages.Add(page);
            }

            // Legend layout parameters (all measurements are in inches)
            double startX = 1.0;          // left margin
            double startY = 1.0;          // top margin
            double boxWidth = 2.0;        // width of each legend box
            double boxHeight = 0.5;       // height of each legend box
            double verticalSpacing = 0.6; // space between entries

            // Simple color palette for legend entries
            string[] palette = new string[]
            {
                "#FFB300", "#803E75", "#FF6800", "#A6BDD7",
                "#C10020", "#CEA262", "#817066", "#007D34",
                "#F6768E", "#00538A", "#FF7A5C", "#53377A"
            };

            int index = 0;
            foreach (string category in categories)
            {
                double posY = startY + index * verticalSpacing;

                // Add a rectangle shape for the legend entry
                long shapeId = page.AddShape(startX, posY, boxWidth, boxHeight, "Rectangle", false);
                Shape legendShape = page.Shapes.GetShape(shapeId);

                // Set fill color from the palette (cycle if more categories than colors)
                string fillColor = palette[index % palette.Length];
                legendShape.Fill.FillForegnd.Value = fillColor;

                // Add the category text inside the rectangle
                legendShape.Text.Value.Clear();
                legendShape.Text.Value.Add(new Txt(category));

                // Center the text vertically within the rectangle
                legendShape.TextXForm.TxtLocPinY.Value = 0.0;               // top of the text block
                legendShape.TextXForm.TxtPinY.Value = boxHeight / 2.0;      // vertical center

                index++;
            }

            // Save the updated diagram with the legend
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with legend to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}