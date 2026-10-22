using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: source diagram, output diagram, master shape name, comma‑separated target shape names
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <source.vsdx> <output.vsdx> <masterShapeName> <targetShapeName1,targetShapeName2,...>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];
        string masterShapeName = args[2];
        string[] targetShapeNames = args[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Guard: source file must exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Locate the master/template shape by its NameU (case‑insensitive)
                Shape? masterShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (string.Equals(shape.NameU, masterShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            masterShape = shape;
                            break;
                        }
                    }
                    if (masterShape != null) break;
                }

                if (masterShape == null)
                {
                    Console.Error.WriteLine($"Master shape \"{masterShapeName}\" not found in the diagram.");
                    return;
                }

                // Iterate over each target shape name and copy geometry from the master shape
                foreach (string targetName in targetShapeNames)
                {
                    bool found = false;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (string.Equals(shape.NameU, targetName, StringComparison.OrdinalIgnoreCase))
                            {
                                // Copy XForm geometry (position, size, rotation)
                                if (masterShape.XForm != null && shape.XForm != null)
                                {
                                    shape.XForm.PinX.Value = masterShape.XForm.PinX.Value;
                                    shape.XForm.PinY.Value = masterShape.XForm.PinY.Value;
                                    shape.XForm.Width.Value = masterShape.XForm.Width.Value;
                                    shape.XForm.Height.Value = masterShape.XForm.Height.Value;
                                    shape.XForm.Angle.Value = masterShape.XForm.Angle.Value;
                                }

                                // Copy TextXForm geometry (text block position, size, rotation)
                                if (masterShape.TextXForm != null && shape.TextXForm != null)
                                {
                                    shape.TextXForm.TxtPinX.Value = masterShape.TextXForm.TxtPinX.Value;
                                    shape.TextXForm.TxtPinY.Value = masterShape.TextXForm.TxtPinY.Value;
                                    shape.TextXForm.TxtLocPinX.Value = masterShape.TextXForm.TxtLocPinX.Value;
                                    shape.TextXForm.TxtLocPinY.Value = masterShape.TextXForm.TxtLocPinY.Value;
                                    shape.TextXForm.TxtWidth.Value = masterShape.TextXForm.TxtWidth.Value;
                                    shape.TextXForm.TxtHeight.Value = masterShape.TextXForm.TxtHeight.Value;
                                    shape.TextXForm.TxtAngle.Value = masterShape.TextXForm.TxtAngle.Value;
                                }

                                Console.WriteLine($"Copied geometry from master \"{masterShapeName}\" to target \"{targetName}\" (Shape ID: {shape.ID}).");
                                found = true;
                                break;
                            }
                        }
                        if (found) break;
                    }

                    if (!found)
                    {
                        Console.Error.WriteLine($"Target shape \"{targetName}\" not found in the diagram. Skipping.");
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}