using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Create a new stylesheet to define paragraph spacing
            StyleSheet customStyle = new StyleSheet
            {
                // Assign a unique ID based on existing stylesheets
                ID = diagram.StyleSheets.Count + 1,
                // Give the style a recognizable name
                Name = "CustomParaSpacing"
            };

            // Define paragraph spacing (in inches) for the style
            // SpBefore and SpAfter control spacing before and after each paragraph
            Para para = new Para
            {
                SpBefore = { Value = 0.2 }, // 0.2 inches before paragraph
                SpAfter = { Value = 0.2 }   // 0.2 inches after paragraph
            };
            // Add the paragraph definition to the stylesheet
            customStyle.Paras.Add(para);

            // Add the custom stylesheet to the diagram's collection
            diagram.StyleSheets.Add(customStyle);

            // Target shape name (adjust as needed); here we apply to all shapes for demo
            const string targetShapeName = "TargetShape";

            // Expected spacing values for verification
            const double expectedSpacing = 0.2;
            const double tolerance = 0.0001;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the custom style to shapes matching the target name
                    // If you want to apply to all shapes, remove the name check
                    if (shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Assign the custom stylesheet to the shape's text style
                        shape.TextStyle = customStyle;
                    }

                    // Verify paragraph spacing on the shape (if it has paragraphs)
                    if (shape.Paras != null && shape.Paras.Count > 0)
                    {
                        // Use the first paragraph for verification
                        Para firstPara = shape.Paras[0];

                        bool beforeOk = Math.Abs(firstPara.SpBefore.Value - expectedSpacing) < tolerance;
                        bool afterOk = Math.Abs(firstPara.SpAfter.Value - expectedSpacing) < tolerance;

                        if (beforeOk && afterOk)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} ('{shape.NameU}') spacing verified.");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} ('{shape.NameU}') spacing mismatch. " +
                                $"SpBefore={firstPara.SpBefore.Value}, SpAfter={firstPara.SpAfter.Value}");
                        }
                    }
                }
            }

            // Save the modified diagram with the new stylesheet applied
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram exceptions
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}