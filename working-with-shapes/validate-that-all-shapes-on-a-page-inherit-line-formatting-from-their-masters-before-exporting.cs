using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and each shape to validate line inheritance
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Only validate shapes that have a master (i.e., can inherit formatting)
                        if (shape.Master == null)
                            continue;

                        // Ensure InheritLine collection is available
                        if (shape.InheritLine == null)
                            continue;

                        bool inheritsLine =
                            shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                            shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value &&
                            shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value &&
                            shape.Line.BeginArrow.Value == shape.InheritLine.BeginArrow.Value &&
                            shape.Line.EndArrow.Value == shape.InheritLine.EndArrow.Value;

                        if (!inheritsLine)
                        {
                            throw new Exception(
                                $"Shape ID {shape.ID} on page ID {page.ID} does not inherit line formatting from its master.");
                        }
                    }
                }

                // All shapes passed validation; save the diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }