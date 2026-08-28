using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ask user for the age limit (in days)
            Console.Write("Enter the maximum age of comments (in days): ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int maxAgeDays) || maxAgeDays < 0)
            {
                Console.WriteLine("Invalid number of days.");
                return;
            }

            // Remove comments older than the specified number of days
            RemoveOldComments(diagram, maxAgeDays);

            // Save the modified diagram (overwrites the original file)
            diagram.Save(inputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Old comments removed and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Removes all annotations (comments) that are older than the given number of days.
    /// </summary>
    /// <param name="diagram">The diagram to process.</param>
    /// <param name="days">Comments older than this many days will be removed.</param>
    static void RemoveOldComments(Diagram diagram, int days)
    {
        // Calculate the cutoff date
        DateTime cutoff = DateTime.Now.AddDays(-days);

        // Iterate through each page in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Access the collection of annotations on the page
            var annotations = page.PageSheet.Annotations;

            // Iterate backwards so that removal does not affect the index order
            for (int i = annotations.Count - 1; i >= 0; i--)
            {
                var annotation = annotations[i];

                // The Date property holds the creation date of the comment.
                // It is a DateValue; its .Value returns a DateTime.
                DateTime commentDate = annotation.Date.Value;

                // If the comment is older than the cutoff, remove it.
                if (commentDate < cutoff)
                {
                    annotations.RemoveAt(i);
                }
            }
        }
    }
}
