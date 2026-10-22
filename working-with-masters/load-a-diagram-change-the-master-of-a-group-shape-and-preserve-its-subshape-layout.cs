using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            bool groupProcessed = false;

            // Iterate through all pages to find a group shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a group shape by its Type
                    if (shape.Type == TypeValue.Group && shape.Master != null)
                    {
                        // Store layout information of each child shape
                        List<ChildInfo> childInfos = new List<ChildInfo>();
                        foreach (Shape child in shape.Shapes)
                        {
                            childInfos.Add(new ChildInfo
                            {
                                Shape = child,
                                PinX = child.XForm.PinX.Value,
                                PinY = child.XForm.PinY.Value,
                                Width = child.XForm.Width.Value,
                                Height = child.XForm.Height.Value,
                                Angle = child.XForm.Angle.Value
                            });
                        }

                        // Define the new master name (must exist in the diagram's masters collection)
                        string newMasterName = "Rectangle"; // Example master; ensure it exists

                        // Retrieve the target master
                        Master? targetMaster = diagram.Masters.GetMasterByName(newMasterName);
                        if (targetMaster == null)
                        {
                            Console.Error.WriteLine($"Master \"{newMasterName}\" not found in the diagram.");
                            return;
                        }

                        // Change the master of the group shape
                        // Assign the new master to the shape (if the property is settable)
                        shape.Master = targetMaster;

                        // Reapply stored layout to each child shape to preserve positions
                        foreach (ChildInfo info in childInfos)
                        {
                            info.Shape.XForm.PinX.Value = info.PinX;
                            info.Shape.XForm.PinY.Value = info.PinY;
                            info.Shape.XForm.Width.Value = info.Width;
                            info.Shape.XForm.Height.Value = info.Height;
                            info.Shape.XForm.Angle.Value = info.Angle;
                        }

                        groupProcessed = true;
                        break; // Exit after processing the first group shape
                    }
                }

                if (groupProcessed)
                    break;
            }

            if (!groupProcessed)
            {
                Console.WriteLine("No group shape found in the diagram.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper class to store child shape layout information
    private class ChildInfo
    {
        public Shape Shape { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
    }
}