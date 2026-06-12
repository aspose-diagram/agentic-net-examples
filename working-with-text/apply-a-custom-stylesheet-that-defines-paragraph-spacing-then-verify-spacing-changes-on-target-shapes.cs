using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx"; // replace with actual path
                Diagram diagram = new Diagram(inputPath);

                // Create a custom StyleSheet (can be used for other formatting)
                StyleSheet customStyle = new StyleSheet();
                customStyle.ID = diagram.StyleSheets.Count + 1;
                customStyle.Name = "CustomParagraphStyle";
                // Add the stylesheet to the diagram
                diagram.StyleSheets.Add(customStyle);

                // Apply the stylesheet to the first page (full page style application)
                Page page = diagram.Pages[0];
                page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

                // Define the target shape name (adjust as needed)
                string targetShapeName = "TargetShape";

                // Locate the target shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception($"Shape with NameU '{targetShapeName}' not found.");
                }

                // Ensure the shape has at least one paragraph
                if (targetShape.Paras.Count == 0)
                {
                    // Add a default paragraph if none exists
                    targetShape.Paras.Add(new Para());
                }

                // Set paragraph spacing values (in inches)
                double spBefore = 0.2; // space before paragraph
                double spAfter = 0.3;  // space after paragraph
                double spLine = 1.0;   // line spacing multiplier

                targetShape.Paras[0].SpBefore.Value = spBefore;
                targetShape.Paras[0].SpAfter.Value = spAfter;
                targetShape.Paras[0].SpLine.Value = spLine;

                // Verify that the spacing values were applied correctly
                bool spacingCorrect = true;

                if (Math.Abs(targetShape.Paras[0].SpBefore.Value - spBefore) > 0.0001)
                    spacingCorrect = false;
                if (Math.Abs(targetShape.Paras[0].SpAfter.Value - spAfter) > 0.0001)
                    spacingCorrect = false;
                if (Math.Abs(targetShape.Paras[0].SpLine.Value - spLine) > 0.0001)
                    spacingCorrect = false;

                if (!spacingCorrect)
                {
                    throw new Exception("Paragraph spacing verification failed.");
                }
                else
                {
                    Console.WriteLine("Paragraph spacing applied and verified successfully.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx"; // replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }