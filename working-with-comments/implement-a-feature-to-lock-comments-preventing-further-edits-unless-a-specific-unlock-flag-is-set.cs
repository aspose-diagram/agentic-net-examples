using System;
using Aspose.Diagram;

namespace CommentLockExample
{
    // Simple class to manage comment locking logic
    public static class CommentLocker
    {
        // Flag indicating whether comments are unlocked for editing
        private static bool _unlockComments = false;

        // Unlock comments globally
        public static void UnlockComments()
        {
            _unlockComments = true;
        }

        // Lock comments globally
        public static void LockComments()
        {
            _unlockComments = false;
        }

        // Attempt to edit a comment; succeeds only if comments are unlocked
        public static void EditComment(Page page, long markerIndex, string newText)
        {
            if (!_unlockComments)
            {
                throw new InvalidOperationException("Comments are locked. Unlock them before editing.");
            }

            // Find the annotation with the specified MarkerIndex
            Annotation target = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                if (ann.MarkerIndex.Value == markerIndex)
                {
                    target = ann;
                    break;
                }
            }

            if (target == null)
            {
                throw new ArgumentException($"No comment found with MarkerIndex {markerIndex}.");
            }

            // Update the comment text
            target.Comment.Value = newText;
        }

        // Add a new comment to a page (or shape) – comments are added unlocked by default
        public static void AddComment(Page page, double x, double y, string text)
        {
            page.AddComment(x, y, text);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Add a sample comment
                CommentLocker.AddComment(page, 5.0, 5.0, "Initial comment");

                // Lock comments to prevent further edits
                CommentLocker.LockComments();

                // Attempt to edit the comment (will throw because comments are locked)
                try
                {
                    // Retrieve the MarkerIndex of the first comment for demonstration
                    long markerId = page.PageSheet.Annotations[0].MarkerIndex.Value;
                    CommentLocker.EditComment(page, markerId, "Edited text");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit failed: {ex.Message}");
                }

                // Unlock comments to allow editing
                CommentLocker.UnlockComments();

                // Now editing succeeds
                try
                {
                    long markerId = page.PageSheet.Annotations[0].MarkerIndex.Value;
                    CommentLocker.EditComment(page, markerId, "Edited after unlock");
                    Console.WriteLine("Comment edited successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit failed: {ex.Message}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}