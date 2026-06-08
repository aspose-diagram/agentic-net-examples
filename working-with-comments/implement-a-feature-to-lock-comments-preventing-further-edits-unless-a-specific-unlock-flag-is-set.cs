using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    private static bool UnlockComments = false;

    static void Main()
    {
        try
        {
            using (Diagram diagram = new Diagram())
            {
                Page page = diagram.Pages[0];

                // Add a page-level comment.
                page.AddComment(2.0, 2.0, "Initial comment");

                // Retrieve the newly added annotation.
                Annotation comment = null;
                foreach (Annotation a in page.PageSheet.Annotations)
                {
                    if (a.Comment.Value == "Initial comment")
                    {
                        comment = a;
                        break;
                    }
                }

                if (comment == null)
                {
                    Console.WriteLine("Failed to add comment.");
                    return;
                }

                Console.WriteLine($"Added comment with MarkerIndex = {comment.MarkerIndex.Value}");

                Console.WriteLine("Attempting to edit comment while locked...");
                EditComment(page, comment.MarkerIndex.Value, "Edited while locked");

                UnlockComments = true;
                Console.WriteLine("Comments unlocked.");

                Console.WriteLine("Attempting to edit comment after unlocking...");
                EditComment(page, comment.MarkerIndex.Value, "Edited after unlocking");

                diagram.Save("LockedCommentsDiagram.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved as LockedCommentsDiagram.vsdx");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void EditComment(Page page, long markerIndex, string newText)
    {
        Annotation target = null;
        foreach (Annotation annotation in page.PageSheet.Annotations)
        {
            if (annotation.MarkerIndex.Value == markerIndex)
            {
                target = annotation;
                break;
            }
        }

        if (target == null)
        {
            Console.WriteLine("Comment not found.");
            return;
        }

        if (!UnlockComments)
        {
            Console.WriteLine("Comment is locked. Edit operation aborted.");
            return;
        }

        target.Comment.Value = newText;
        Console.WriteLine($"Comment updated to: \"{target.Comment.Value}\"");
    }
}