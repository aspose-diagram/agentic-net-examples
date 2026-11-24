using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page (source) and a second page (target)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page (source)
                Page srcPage = diagram.Pages[0];

                // Find the triangle shape on the source page.
                // Heuristic: look for a shape whose NameU contains "Triangle" (case‑insensitive).
                Shape triangleShape = null;
                foreach (Shape s in srcPage.Shapes)
                {
                    // Skip deleted shapes
                    if (s.Del == BOOL.True) continue;

                    if (!string.IsNullOrEmpty(s.NameU) && s.NameU.IndexOf("Triangle", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        triangleShape = s;
                        break;
                    }
                }

                // If not found, fall back to the first non‑deleted shape.
                if (triangleShape == null)
                {
                    foreach (Shape s in srcPage.Shapes)
                    {
                        if (s.Del == BOOL.False)
                        {
                            triangleShape = s;
                            break;
                        }
                    }
                }

                if (triangleShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found to clone.");
                    return;
                }

                // Determine target page (second page). Create it if it does not exist.
                Page targetPage;
                if (diagram.Pages.Count >= 2)
                {
                    targetPage = diagram.Pages[1];
                }
                else
                {
                    // Create a new page and add it to the diagram
                    targetPage = new Page();
                    diagram.Pages.Add(targetPage);
                }

                // Original position of the triangle shape
                double origPinX = triangleShape.XForm.PinX.Value;
                double origPinY = triangleShape.XForm.PinY.Value;

                // Offset coordinates (hard‑coded 2 inches right and 2 inches up)
                double offsetX = 2.0;
                double offsetY = 2.0;

                double newPinX = origPinX + offsetX;
                double newPinY = origPinY + offsetY;

                // Determine the master name to use for the new shape.
                // Prefer the master name; if unavailable, fall back to the shape's NameU.
                string masterName = null;
                if (triangleShape.Master != null && !string.IsNullOrEmpty(triangleShape.Master.Name))
                {
                    masterName = triangleShape.Master.Name;
                }
                else if (!string.IsNullOrEmpty(triangleShape.NameU))
                {
                    masterName = triangleShape.NameU;
                }

                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Unable to determine master name for the shape.");
                    return;
                }

                // Add a new shape on the target page using the same master.
                // The fourth parameter (isCalculate) must be a plain bool, not BOOL.
                long newShapeId = targetPage.AddShape(newPinX, newPinY, masterName, false);
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy size (width & height)
                newShape.XForm.Width.Value = triangleShape.XForm.Width.Value;
                newShape.XForm.Height.Value = triangleShape.XForm.Height.Value;

                // Copy fill formatting (if any)
                newShape.Fill.FillForegnd.Value = triangleShape.Fill.FillForegnd.Value;
                newShape.Fill.FillBkgnd.Value = triangleShape.Fill.FillBkgnd.Value;
                newShape.Fill.FillPattern.Value = triangleShape.Fill.FillPattern.Value;

                // Copy line formatting
                newShape.Line.LinePattern.Value = triangleShape.Line.LinePattern.Value;
                newShape.Line.LineWeight.Value = triangleShape.Line.LineWeight.Value;
                newShape.Line.LineColor.Value = triangleShape.Line.LineColor.Value;

                // Copy text content
                newShape.Text.Value.Clear();
                foreach (FormatTxt fmt in triangleShape.Text.Value)
                {
                    if (fmt is Txt txt)
                    {
                        // Preserve each text run
                        newShape.Text.Value.Add(new Txt(txt.Text));
                    }
                    else
                    {
                        // Preserve other format items as‑is
                        newShape.Text.Value.Add(fmt);
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}