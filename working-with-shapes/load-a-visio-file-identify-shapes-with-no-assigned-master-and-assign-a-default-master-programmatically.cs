using System.IO;
using System;
using Aspose.Diagram;

class AssignDefaultMaster
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and the output file
            string sourceFile = "input.vsdx";
            string outputFile = "output.vsdx";

            // Name of the default master to assign (must exist in a stencil)
            string defaultMasterName = "Rectangle";

            // Load the Visio diagram (uses the Diagram constructor – lifecycle rule)
            using (Diagram diagram = new Diagram(sourceFile))
            {
                // Try to locate the default master already present in the document
                int masterId = -1;
                foreach (Master m in diagram.Masters)
                {
                    if (string.Equals(m.NameU, defaultMasterName, StringComparison.OrdinalIgnoreCase))
                    {
                        masterId = m.ID;
                        break;
                    }
                }

                // If the master is not found, add it from a stencil/template file
                if (masterId == -1)
                {
                    // Path to a stencil that contains the required master (adjust as needed)
                    string stencilPath = "basic_u.vssx";

                    // AddMaster adds the master to the diagram and returns its unique ID (rule usage)
                    masterId = diagram.AddMaster(stencilPath, defaultMasterName);
                }

                // Retrieve the Master object using the obtained ID
                Master defaultMaster = diagram.Masters[masterId];

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
                        }
                    }
                }

                // Save the modified diagram (uses the Diagram.Save method – lifecycle rule)
                diagram.Save(outputFile, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
