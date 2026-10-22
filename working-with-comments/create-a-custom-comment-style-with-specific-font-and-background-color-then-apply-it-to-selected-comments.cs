using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Define custom style parameters
                string customFontName = "Arial";
                string backgroundColor = "#FFFF00"; // Yellow background

                // Sample comments to add and style
                string[] commentsToAdd = new[] { "Important Note 1", "Important Note 2" };

                // Starting coordinates for placing comment shapes
                double startX = 1.0;
                double startY = 1.0;
                double shapeWidth = 2.0;
                double shapeHeight = 1.0;
                double verticalSpacing = 1.5;

                for (int i = 0; i < commentsToAdd.Length; i++)
                {
                    double pinX = startX;
                    double pinY = startY + i * verticalSpacing;

                    // Add a rectangle shape that will act as the visual comment
                    long shapeId = page.DrawRectangle(pinX, pinY, shapeWidth, shapeHeight);

                    // Retrieve the created shape
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Failed to retrieve shape with ID {shapeId}.");
                        continue;
                    }

                    // Clear any existing text and add the comment text
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(commentsToAdd[i]));

                    // Ensure there is at least one character formatting entry
                    if (shape.Chars.Count == 0)
                    {
                        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                        ch.IX = 0; // Apply to all characters
                        ch.FontName.Value = customFontName;
                        shape.Chars.Add(ch);
                    }
                    else
                    {
                        // Apply the custom font to all existing character entries
                        for (int c = 0; c < shape.Chars.Count; c++)
                        {
                            shape.Chars[c].FontName.Value = customFontName;
                        }
                    }

                    // Apply background fill (solid color)
                    shape.Fill.FillPattern.Value = 1; // Solid fill
                    shape.Fill.FillForegnd.Value = backgroundColor;

                    // Link a shape-level comment to this shape
                    page.AddComment(shape, commentsToAdd[i]);
                }

                // Update existing comments that contain the word "Important"
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    if (ann.Comment?.Value != null && ann.Comment.Value.Contains("Important"))
                    {
                        // Retrieve the associated shape if any
                        if (ann.ShapeID > 0)
                        {
                            Shape linkedShape = page.Shapes.GetShape(ann.ShapeID);
                            if (linkedShape != null)
                            {
                                // Update shape text to match the comment (optional)
                                linkedShape.Text.Value.Clear();
                                linkedShape.Text.Value.Add(new Txt(ann.Comment.Value));

                                // Apply font
                                if (linkedShape.Chars.Count == 0)
                                {
                                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                                    ch.IX = 0;
                                    ch.FontName.Value = customFontName;
                                    linkedShape.Chars.Add(ch);
                                }
                                else
                                {
                                    for (int c = 0; c < linkedShape.Chars.Count; c++)
                                    {
                                        linkedShape.Chars[c].FontName.Value = customFontName;
                                    }
                                }

                                // Apply background fill
                                linkedShape.Fill.FillPattern.Value = 1;
                                linkedShape.Fill.FillForegnd.Value = backgroundColor;
                            }
                        }

                        // Optionally modify the comment text itself
                        ann.Comment.Value = "[Styled] " + ann.Comment.Value;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}