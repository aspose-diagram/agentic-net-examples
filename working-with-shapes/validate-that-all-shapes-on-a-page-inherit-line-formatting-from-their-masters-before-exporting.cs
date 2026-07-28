using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "validated_output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                bool validationFailed = false;

                // Iterate through each page and each shape on the page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Compare line formatting with the inherited values from the master
                        bool lineColorInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                        bool linePatternInherited = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;
                        bool lineWeightInherited = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

                        if (!lineColorInherited || !linePatternInherited || !lineWeightInherited)
                        {
                            Console.WriteLine($"Validation Warning: Shape ID {shape.ID} on page '{page.Name}' does not fully inherit line formatting.");
                            validationFailed = true;
                        }
                    }
                }

                if (validationFailed)
                {
                    // If any shape failed validation, abort saving
                    throw new Exception("One or more shapes do not inherit line formatting from their masters. Export aborted.");
                }

                // Save the diagram after successful validation
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }