using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file
                string inputPath = "input.vsdx"; // TODO: replace with actual path
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (you can change the index as needed)
                Page page = diagram.Pages[0];

                // ID of the shape to be cloned – replace with the actual shape ID
                int originalShapeId = 1;

                // Retrieve the original shape
                Shape originalShape = page.Shapes.GetShape(originalShapeId);
                if (originalShape == null)
                {
                    throw new Exception($"Shape with ID {originalShapeId} not found.");
                }

                // Ensure the shape has a master (required for AddShape)
                if (originalShape.Master == null)
                {
                    throw new Exception("The shape does not have an associated master.");
                }

                // Preserve master name
                string masterName = originalShape.Master.Name;

                // Preserve original geometry
                double origPinX = originalShape.XForm.PinX.Value;
                double origPinY = originalShape.XForm.PinY.Value;
                double origWidth = originalShape.XForm.Width.Value;
                double origHeight = originalShape.XForm.Height.Value;
                double origAngle = originalShape.XForm.Angle.Value; // angle in radians

                // Define where the cloned shape will be placed (offset by 2 inches on X axis)
                double newPinX = origPinX + 2.0;
                double newPinY = origPinY;

                // Add the cloned shape using the same master
                long newShapeIdLong = page.AddShape(newPinX, newPinY, masterName);
                // Retrieve the newly added shape
                Shape clonedShape = page.Shapes.GetShape((int)newShapeIdLong);
                if (clonedShape == null)
                {
                    throw new Exception("Failed to create the cloned shape.");
                }

                // Copy size and rotation
                clonedShape.XForm.Width.Value = origWidth;
                clonedShape.XForm.Height.Value = origHeight;
                clonedShape.XForm.Angle.Value = origAngle;

                // Preserve gluing settings (GlueType)
                clonedShape.Misc.GlueType.Value = originalShape.Misc.GlueType.Value;

                // Preserve existing connections (glue) by replicating Connect objects
                // Iterate over all connections on the page
                foreach (Connect conn in page.Connects)
                {
                    bool isFromOriginal = conn.FromSheet == originalShapeId;
                    bool isToOriginal = conn.ToSheet == originalShapeId;

                    if (isFromOriginal || isToOriginal)
                    {
                        // Create a new connection for the cloned shape
                        Connect newConn = new Connect();

                        // If the original shape was the source, replace FromSheet with the cloned shape ID
                        newConn.FromSheet = isFromOriginal ? (int)newShapeIdLong : conn.FromSheet;
                        newConn.FromCell = conn.FromCell;

                        // If the original shape was the target, replace ToSheet with the cloned shape ID
                        newConn.ToSheet = isToOriginal ? (int)newShapeIdLong : conn.ToSheet;
                        newConn.ToCell = conn.ToCell;

                        // Add the new connection to the page
                        page.Connects.Add(newConn);
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }