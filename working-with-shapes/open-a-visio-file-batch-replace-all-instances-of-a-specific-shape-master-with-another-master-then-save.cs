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

                // Names of the masters to replace
                string oldMasterName = "OldMaster";
                string newMasterName = "NewMaster";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Verify that the new master exists in the diagram
                if (!diagram.Masters.IsExist(newMasterName))
                {
                    Console.WriteLine($"Error: Master \"{newMasterName}\" does not exist in the diagram.");
                    return;
                }

                // Retrieve the new master once for reuse
                Master replacementMaster = diagram.Masters.GetMasterByName(newMasterName);

                // Iterate through all pages and shapes, replacing the old master with the new one
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an associated master and that it matches the old master name
                        if (shape.Master != null && shape.Master.Name == oldMasterName)
                        {
                            // Replace the master reference
                            shape.Master = replacementMaster;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }