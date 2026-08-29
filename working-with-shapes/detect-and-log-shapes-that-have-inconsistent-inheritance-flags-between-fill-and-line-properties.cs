using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be analyzed.
                // Adjust the file path as needed.
                string diagramPath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if fill properties are inherited from the parent style/master.
                        bool fillInherited =
                            shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                            shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value &&
                            shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                        // Determine if line properties are inherited from the parent style/master.
                        bool lineInherited =
                            shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                            shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value &&
                            shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;

                        // Log shapes where fill inheritance flag differs from line inheritance flag.
                        if (fillInherited != lineInherited)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) has inconsistent inheritance:");
                            Console.WriteLine($"  Fill Inherited: {fillInherited}");
                            Console.WriteLine($"  Line Inherited: {lineInherited}");
                        }
                    }
                }

                // Optionally, save the diagram after analysis (no changes made here).
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }