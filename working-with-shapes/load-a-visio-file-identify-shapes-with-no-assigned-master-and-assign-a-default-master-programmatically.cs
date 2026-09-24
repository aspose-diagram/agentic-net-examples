using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the default master name to assign
                const string defaultMasterName = "Rectangle";

                // Ensure the default master exists in the diagram
                Master defaultMaster = diagram.Masters.GetMasterByName(defaultMasterName);
                if (defaultMaster == null)
                {
                    // If the master is not present, import it from a standard Visio stencil
                    // Adjust the stencilPath to point to a valid .vssx file on your system
                    string stencilPath = "Basic_U.vssx";
                    diagram.AddMaster(stencilPath, defaultMasterName);
                    defaultMaster = diagram.Masters.GetMasterByName(defaultMasterName);
                    if (defaultMaster == null)
                    {
                        Console.WriteLine($"Failed to load default master '{defaultMasterName}'.");
                        return;
                    }
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify shapes without an assigned master
                        if (shape.Master == null)
                        {
                            // Assign the default master to the shape
                            shape.Master = defaultMaster;
                            Console.WriteLine($"Assigned default master to shape ID {shape.ID} on page {page.ID}.");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with updated masters.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }