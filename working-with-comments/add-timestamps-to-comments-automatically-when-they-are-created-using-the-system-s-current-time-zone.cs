using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure there is at least one shape to attach the comment to
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("The page contains no shapes.");
                }

                // Get the first shape on the page
                Shape shape = page.Shapes[0];

                // Build the comment text with a timestamp using the system's current time zone
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
                string commentText = $"Comment added at {timestamp}";

                // Add the comment to the shape
                page.AddComment(shape, commentText);

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }