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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Collect IDs of masters that are actually used by shapes on any page
            HashSet<int> usedMasterIds = new HashSet<int>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Some shapes may not be based on a master; skip those
                    if (shape.Master != null)
                    {
                        usedMasterIds.Add(shape.Master.ID);
                    }
                }
            }

            // Determine which masters are unused
            List<Master> mastersToRemove = new List<Master>();
            foreach (Master master in diagram.Masters)
            {
                if (!usedMasterIds.Contains(master.ID))
                {
                    mastersToRemove.Add(master);
                }
            }

            // Remove the unused masters from the collection
            foreach (Master master in mastersToRemove)
            {
                diagram.Masters.Remove(master);
                master.Dispose(); // optional cleanup of unmanaged resources
            }

            // Save the cleaned diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
