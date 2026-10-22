using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <sourceMasterName> <targetMasterName> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string sourceMasterName = args[1];
        string targetMasterName = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve source and target masters.
            Master? sourceMaster = null;
            Master? targetMaster = null;

            // Find source master by name.
            foreach (Master master in diagram.Masters)
            {
                if (master.Name == sourceMasterName)
                {
                    sourceMaster = master;
                }
                if (master.Name == targetMasterName)
                {
                    targetMaster = master;
                }
                if (sourceMaster != null && targetMaster != null)
                {
                    break;
                }
            }

            if (sourceMaster == null)
            {
                Console.Error.WriteLine($"Source master \"{sourceMasterName}\" not found in diagram.");
                return;
            }

            if (targetMaster == null)
            {
                Console.Error.WriteLine($"Target master \"{targetMasterName}\" not found in diagram.");
                return;
            }

            // Iterate through each page and replace shapes that use the source master.
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs that need replacement to avoid modifying collection while iterating.
                List<long> shapesToReplace = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master and compare its name.
                    if (shape.Master != null && shape.Master.Name == sourceMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Process each shape that uses the source master.
                foreach (long oldShapeId in shapesToReplace)
                {
                    Shape oldShape = page.Shapes.GetShape(oldShapeId);

                    // Preserve geometry.
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Add a new shape using the target master at the same location and size.
                    // Note: AddShape overload with size parameters is not documented; use the overload with pinX, pinY, masterName.
                    // After adding, set size manually.
                    long newShapeId = page.AddShape(pinX, pinY, targetMaster.Name);
                    Shape newShape = page.Shapes.GetShape(newShapeId);
                    newShape.XForm.Width.Value = width;
                    newShape.XForm.Height.Value = height;

                    // Preserve connections: update any Connect objects that reference the old shape ID.
                    foreach (Connect conn in page.Connects)
                    {
                        if (conn.FromSheet == oldShapeId)
                        {
                            conn.FromSheet = newShapeId;
                        }
                        if (conn.ToSheet == oldShapeId)
                        {
                            conn.ToSheet = newShapeId;
                        }
                    }

                    // Hide the old shape instead of deleting it (removal APIs are not guaranteed).
                    oldShape.Del = BOOL.True;
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}