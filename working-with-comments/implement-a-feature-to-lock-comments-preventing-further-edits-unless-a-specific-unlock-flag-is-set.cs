using System;
using Aspose.Diagram;

class Program
    {
        // Global flag controlling comment edit permission
        private static bool _unlockComments = false;

        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio diagram (must exist)
                string inputPath = "input.vsdx";
                // Path for the output diagram
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Add a new comment at coordinates (2,2)
                page.AddComment(2.0, 2.0, "Initial comment");

                // Retrieve the newly added annotation (last in the collection)
                Annotation addedAnnotation = null;
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    addedAnnotation = ann; // will end up as the last annotation
                }

                if (addedAnnotation == null)
                {
                    Console.WriteLine("Failed to add comment.");
                    return;
                }

                Console.WriteLine($"Comment added with MarkerIndex: {addedAnnotation.MarkerIndex.Value}");
                Console.WriteLine($"Current comment text: {addedAnnotation.Comment.Value}");

                // Prompt user to unlock comments
                Console.WriteLine("Type 'unlock' to allow editing the comment, or press Enter to keep it locked:");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.Equals("unlock", StringComparison.OrdinalIgnoreCase))
                {
                    UnlockComments();
                }

                // Attempt to edit the comment
                if (_unlockComments)
                {
                    addedAnnotation.Comment.Value = "Edited comment after unlock.";
                    Console.WriteLine("Comment edited successfully.");
                }
                else
                {
                    Console.WriteLine("Comments are locked. Edit operation skipped.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Method to set the unlock flag
        private static void UnlockComments()
        {
            _unlockComments = true;
            Console.WriteLine("Comments have been unlocked.");
        }
    }