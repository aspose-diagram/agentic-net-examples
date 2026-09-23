using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (you can adjust the index or use GetPage by name)
                Page page = diagram.Pages[0];

                // Get the first shape on the page (replace with your target shape as needed)
                Shape shape = page.Shapes[0];

                // Create a timestamp string using the system's current time zone
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Build the comment text with the timestamp
                string commentText = $"[{timestamp}] Review this shape";

                // Add the comment (annotation) to the shape
                page.AddComment(shape, commentText);

                // Save the diagram with the new comment
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }