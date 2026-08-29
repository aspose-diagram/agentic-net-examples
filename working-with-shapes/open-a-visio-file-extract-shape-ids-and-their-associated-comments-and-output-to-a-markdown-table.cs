using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the input Visio file path
            string inputPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                inputPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                inputPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Console.WriteLine("No file path provided. Exiting.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Output markdown table header
            Console.WriteLine("| Page | Shape ID | Comment |");
            Console.WriteLine("|------|----------|---------|");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Access the annotations (comments) collection for the current page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the shape ID associated with the comment
                    int shapeId = annotation.ShapeID;

                    // Retrieve the comment text
                    string commentText = annotation.Comment.Value ?? string.Empty;

                    // Escape pipe characters in comment to keep markdown table integrity
                    commentText = commentText.Replace("|", "\\|");

                    // Output a row in the markdown table
                    Console.WriteLine($"| {page.Name} | {shapeId} | {commentText} |");
                }
            }
        }
    }