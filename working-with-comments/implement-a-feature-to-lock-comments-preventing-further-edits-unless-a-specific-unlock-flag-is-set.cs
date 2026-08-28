using System;
using Aspose.Diagram;

namespace CommentLockExample
{
    // Helper class to manage comment lock state
    public class CommentLocker
    {
        private readonly System.Collections.Generic.HashSet<long> _lockedComments = new();

        // Lock a comment by its unique MarkerIndex
        public void LockComment(long commentId)
        {
            _lockedComments.Add(commentId);
        }

        // Unlock a comment by its unique MarkerIndex
        public void UnlockComment(long commentId)
        {
            _lockedComments.Remove(commentId);
        }

        // Attempt to edit a comment; respects lock unless unlockFlag is true
        public void EditComment(Page page, long commentId, string newText, bool unlockFlag = false)
        {
            // Find the annotation with the specified MarkerIndex
            Annotation target = null;
            foreach (Annotation ann in page.PageSheet.Annotations)
            {
                if (ann.MarkerIndex.Value == commentId)
                {
                    target = ann;
                    break;
                }
            }

            if (target == null)
                throw new Exception($"Comment with ID {commentId} not found.");

            // If the comment is locked and unlockFlag is not set, prevent editing
            if (_lockedComments.Contains(commentId) && !unlockFlag)
                throw new Exception($"Comment {commentId} is locked and cannot be edited.");

            // Perform the edit
            target.Comment.Value = newText;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Add a simple rectangle shape to the active page
                // Parameters: PinX, PinY, Master name, Master ID (0 for default)
                diagram.AddShape(5.0, 5.0, "Rectangle", 0);

                // Retrieve the shape we just added (first shape on the page)
                Page page = diagram.ActivePage;
                Shape shape = page.Shapes[1]; // Shapes collection is 1‑based

                // Add a comment associated with the shape
                page.AddComment(shape, "Initial comment text");

                // Retrieve the newly added annotation to obtain its MarkerIndex (unique ID)
                Annotation comment = null;
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    // The most recent annotation will have the highest MarkerIndex
                    if (comment == null || ann.MarkerIndex.Value > comment.MarkerIndex.Value)
                        comment = ann;
                }

                if (comment == null)
                    throw new Exception("Failed to add comment.");

                long commentId = comment.MarkerIndex.Value;
                Console.WriteLine($"Added comment with ID {commentId}.");

                // Initialize the locker and lock the comment
                CommentLocker locker = new CommentLocker();
                locker.LockComment(commentId);
                Console.WriteLine($"Comment {commentId} is now locked.");

                // Attempt to edit without unlocking (should fail)
                try
                {
                    locker.EditComment(page, commentId, "Attempted edit while locked");
                    Console.WriteLine("Edit succeeded unexpectedly.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Edit prevented: {ex.Message}");
                }

                // Unlock the comment and edit again
                locker.UnlockComment(commentId);
                Console.WriteLine($"Comment {commentId} has been unlocked.");

                try
                {
                    locker.EditComment(page, commentId, "Edited after unlock");
                    Console.WriteLine("Edit after unlock succeeded.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected failure: {ex.Message}");
                }

                // Save the diagram to a file
                string outputPath = "LockedCommentsDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}