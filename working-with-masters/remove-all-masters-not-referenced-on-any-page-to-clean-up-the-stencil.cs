using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with the appropriate load rule if needed)
            Diagram diagram = new Diagram("input.vsdx");

            // Gather the names of masters that are actually used by shapes on any page
            HashSet<string> usedMasterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shapes that are instances of a master have the Master property set
                    if (shape.Master != null)
                    {
                        // Prefer the universal name; fall back to the local name
                        string masterName = !string.IsNullOrEmpty(shape.Master.NameU)
                                            ? shape.Master.NameU
                                            : shape.Master.Name;

                        if (!string.IsNullOrEmpty(masterName))
                            usedMasterNames.Add(masterName);
                    }
                }
            }

            // Determine which masters are not referenced
            List<Master> mastersToRemove = new List<Master>();
            foreach (Master master in diagram.Masters)
            {
                string masterName = !string.IsNullOrEmpty(master.NameU)
                                    ? master.NameU
                                    : master.Name;

                if (!usedMasterNames.Contains(masterName))
                    mastersToRemove.Add(master);
            }

            // Remove the unreferenced masters from the collection
            foreach (Master master in mastersToRemove)
            {
                diagram.Masters.Remove(master);
                master.Dispose(); // release unmanaged resources
            }

            // Save the cleaned diagram (replace with the appropriate save rule if needed)
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
