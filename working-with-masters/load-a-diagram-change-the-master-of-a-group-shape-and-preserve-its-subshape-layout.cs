using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Find the first group shape on the page
                Shape? groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    Console.WriteLine("No group shape found on the page.");
                    return;
                }

                // Name of the new master to assign to the group shape
                string newMasterName = "NewGroupMaster";

                // Verify that the master exists in the diagram's master collection
                if (!diagram.Masters.IsExist(newMasterName))
                {
                    Console.WriteLine($"Master \"{newMasterName}\" does not exist in the diagram.");
                    return;
                }

                // Retrieve the master object
                Master newMaster = diagram.Masters.GetMasterByName(newMasterName);

                // Change the master of the group shape while keeping its sub‑shapes intact
                groupShape.Master = newMaster;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with updated group master.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }