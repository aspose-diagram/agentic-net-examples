using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramDuplicateResourceValidator
{
    static void Main()
    {
        try
        {

            // Load the diagrams to be merged
            var diagram1 = new Diagram("diagram1.vsdx");
            var diagram2 = new Diagram("diagram2.vsdx");

            // Combine the second diagram into the first one
            diagram1.Combine(diagram2);

            // Validate that no duplicate master (resource) names exist after the merge
            ValidateNoDuplicateMasterNames(diagram1);

            // Save the combined diagram (optional, demonstrates use of the save rule)
            diagram1.Save("combined.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateNoDuplicateMasterNames(Diagram diagram)
    {
        var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Master master in diagram.Masters)
        {
            // Prefer the universal name; fall back to the local name if needed
            string name = master.NameU ?? master.Name;
            if (string.IsNullOrEmpty(name))
                continue;

            // If the name already exists, raise an error
            if (!seenNames.Add(name))
                throw new InvalidOperationException($"Duplicate master resource name detected: {name}");
        }
    }
}
