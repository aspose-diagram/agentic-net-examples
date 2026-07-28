using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if fill properties are inherited
                        bool fillInherited = false;
                        if (shape.InheritFill != null && shape.Fill != null)
                        {
                            // Compare foreground color and fill pattern as representative inheritance flags
                            fillInherited =
                                shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                                shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;
                        }

                        // Determine if line properties are inherited
                        bool lineInherited = false;
                        if (shape.InheritLine != null && shape.Line != null)
                        {
                            // Compare line color and line pattern as representative inheritance flags
                            lineInherited =
                                shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                                shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;
                        }

                        // Log shapes where fill and line inheritance flags are inconsistent
                        if (fillInherited != lineInherited)
                        {
                            Console.WriteLine($"Inconsistent inheritance detected:");
                            Console.WriteLine($"  Page Name: {page.Name}");
                            Console.WriteLine($"  Shape ID: {shape.ID}");
                            Console.WriteLine($"  Shape Name: {shape.Name}");
                            Console.WriteLine($"  Fill Inherited: {fillInherited}");
                            Console.WriteLine($"  Line Inherited: {lineInherited}");
                        }
                    }
                }

                // Optionally save the diagram unchanged (demonstrates save usage)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }