using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Names of the deprecated master and the replacement master
                string deprecatedMasterName = "OldMaster";
                string updatedMasterName = "NewMaster";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the replacement master exists in the diagram
                if (!diagram.Masters.IsExist(updatedMasterName))
                {
                    Console.WriteLine($"Error: Replacement master \"{updatedMasterName}\" does not exist in the diagram.");
                    return;
                }

                // Retrieve the replacement master once for reuse
                Master replacementMaster = diagram.Masters.GetMasterByName(updatedMasterName);

                // Iterate through all pages and shapes, replacing the master where needed
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses the deprecated master
                        if (shape.Master != null && shape.Master.Name == deprecatedMasterName)
                        {
                            // Replace the master with the updated one
                            shape.Master = replacementMaster;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }