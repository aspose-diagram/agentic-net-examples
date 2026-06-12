using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the Visio file path as the first argument
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: InheritanceChecker <input-visio-file>");
                return;
            }

            string inputPath = args[0];

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if fill is inherited (foreground color matches inherited value)
                    bool fillInherited = false;
                    try
                    {
                        fillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    }
                    catch
                    {
                        // If any part is missing, treat as not inherited
                        fillInherited = false;
                    }

                    // Determine if line is inherited (line color matches inherited value)
                    bool lineInherited = false;
                    try
                    {
                        lineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                    }
                    catch
                    {
                        // If any part is missing, treat as not inherited
                        lineInherited = false;
                    }

                    // Log shapes where inheritance status differs between fill and line
                    if (fillInherited != lineInherited)
                    {
                        Console.WriteLine($"Shape ID {shape.ID}, NameU '{shape.NameU}' has inconsistent inheritance:");
                        Console.WriteLine($"  FillInherited = {fillInherited}, LineInherited = {lineInherited}");
                    }
                }
            }
        }
    }