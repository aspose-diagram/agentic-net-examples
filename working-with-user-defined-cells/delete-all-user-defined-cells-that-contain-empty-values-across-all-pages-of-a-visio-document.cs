using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio document (lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of user‑defined cells on the page sheet
                var users = page.PageSheet.Users;

                // Remove cells with empty values (iterate backwards to avoid index issues)
                for (int i = users.Count - 1; i >= 0; i--)
                {
                    var user = users[i];

                    // If the cell's value is null or an empty string, delete it
                    if (string.IsNullOrEmpty(user.Value?.ToString()))
                    {
                        users.RemoveAt(i);
                    }
                }
            }

            // Save the modified document (lifecycle rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
