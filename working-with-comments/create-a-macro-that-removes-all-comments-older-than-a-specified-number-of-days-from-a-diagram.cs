using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input file path, number of days, output file path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: <input.vsdx> <days> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string daysArg = args[1];
        string outputPath = args[2];

        if (!int.TryParse(daysArg, out int days) || days < 0)
        {
            Console.WriteLine("Invalid number of days.");
            return;
        }

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Calculate the cutoff date
        DateTime cutoffDate = DateTime.Now.AddDays(-days);

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            // Access the annotations collection on the page sheet
            var annotations = page.PageSheet.Annotations;

            // Iterate backwards to safely remove items while iterating
            for (int i = annotations.Count - 1; i >= 0; i--)
            {
                var annotation = annotations[i];

                // Read the comment's creation date (assumed to be a DateTime value)
                // The Date property is read‑only but its Value can be accessed.
                DateTime commentDate = annotation.Date.Value;

                // Remove the annotation if it is older than the cutoff
                if (commentDate < cutoffDate)
                {
                    annotations.RemoveAt(i);
                }
            }
        }

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
