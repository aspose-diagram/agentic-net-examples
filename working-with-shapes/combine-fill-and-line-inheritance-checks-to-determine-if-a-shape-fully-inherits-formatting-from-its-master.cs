using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : null;

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape is based on a master (has a master reference)
                    if (shape.Master == null)
                        continue;

                    bool fillInherited = IsFillFullyInherited(shape);
                    bool lineInherited = IsLineFullyInherited(shape);
                    bool fullyInherited = fillInherited && lineInherited;

                    // Output the inheritance status
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.NameU}, FullyInherited: {fullyInherited}");
                }
            }

            // Optionally save the diagram (no modifications made, just demonstration)
            if (!string.IsNullOrEmpty(outputPath))
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Determines if all relevant fill properties match the inherited values
    private static bool IsFillFullyInherited(Shape shape)
    {
        // Guard against null InheritFill
        if (shape.InheritFill == null)
            return false;

        // Compare each fill-related cell
        bool foregndMatch = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
        bool bkgndMatch = shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value;
        bool patternMatch = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;
        bool foregndTransMatch = shape.Fill.FillForegndTrans.Value == shape.InheritFill.FillForegndTrans.Value;
        bool bkgndTransMatch = shape.Fill.FillBkgndTrans.Value == shape.InheritFill.FillBkgndTrans.Value;
        bool shdwForegndMatch = shape.Fill.ShdwForegnd.Value == shape.InheritFill.ShdwForegnd.Value;
        bool shdwBkgndMatch = shape.Fill.ShdwBkgnd.Value == shape.InheritFill.ShdwBkgnd.Value;
        bool shdwPatternMatch = shape.Fill.ShdwPattern.Value == shape.InheritFill.ShdwPattern.Value;
        bool shdwTypeMatch = shape.Fill.ShapeShdwType.Value == shape.InheritFill.ShapeShdwType.Value;
        bool shdwObliqueAngleMatch = shape.Fill.ShapeShdwObliqueAngle.Value == shape.InheritFill.ShapeShdwObliqueAngle.Value;
        bool shdwOffsetXMatch = shape.Fill.ShapeShdwOffsetX.Value == shape.InheritFill.ShapeShdwOffsetX.Value;
        bool shdwOffsetYMatch = shape.Fill.ShapeShdwOffsetY.Value == shape.InheritFill.ShapeShdwOffsetY.Value;

        // Return true only if all fill properties are inherited
        return foregndMatch && bkgndMatch && patternMatch && foregndTransMatch &&
               bkgndTransMatch && shdwForegndMatch && shdwBkgndMatch && shdwPatternMatch &&
               shdwTypeMatch && shdwObliqueAngleMatch && shdwOffsetXMatch && shdwOffsetYMatch;
    }

    // Determines if all relevant line properties match the inherited values
    private static bool IsLineFullyInherited(Shape shape)
    {
        // Guard against null InheritLine
        if (shape.InheritLine == null)
            return false;

        // Compare each line-related cell
        bool patternMatch = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;
        bool colorMatch = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
        bool beginArrowMatch = shape.Line.BeginArrow.Value == shape.InheritLine.BeginArrow.Value;
        bool beginArrowSizeMatch = shape.Line.BeginArrowSize.Value == shape.InheritLine.BeginArrowSize.Value;
        bool endArrowMatch = shape.Line.EndArrow.Value == shape.InheritLine.EndArrow.Value;
        bool endArrowSizeMatch = shape.Line.EndArrowSize.Value == shape.InheritLine.EndArrowSize.Value;
        bool weightMatch = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

        // Return true only if all line properties are inherited
        return patternMatch && colorMatch && beginArrowMatch && beginArrowSizeMatch &&
               endArrowMatch && endArrowSizeMatch && weightMatch;
    }
}