using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input parameters
        string diagramPath = "input.vsdx";          // Path to the source Visio file
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string sourcePageName = "Page-1";           // Name of the page containing the shape to clone
        string targetPageName = "Page-2";           // Name of the page where the clone will be inserted
        long shapeIdToClone = 1;                    // ID of the shape to clone (as displayed in Visio)

        // Guard: ensure the source file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the source page
            Page sourcePage = null;
            foreach (Page p in diagram.Pages)
            {
                if (p.Name == sourcePageName)
                {
                    sourcePage = p;
                    break;
                }
            }
            if (sourcePage == null)
            {
                Console.Error.WriteLine($"Source page \"{sourcePageName}\" not found.");
                return;
            }

            // Locate or create the target page
            Page targetPage = null;
            foreach (Page p in diagram.Pages)
            {
                if (p.Name == targetPageName)
                {
                    targetPage = p;
                    break;
                }
            }
            if (targetPage == null)
            {
                // Create a new page with a unique ID
                targetPage = new Page();
                targetPage.Name = targetPageName;
                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                targetPage.ID = maxId + 1;
                diagram.Pages.Add(targetPage);
            }

            // Retrieve the shape to clone from the source page
            Shape sourceShape = sourcePage.Shapes.GetShape((int)shapeIdToClone);
            if (sourceShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeIdToClone} not found on page \"{sourcePageName}\".");
                return;
            }

            // Ensure the shape has an associated master (required for adding a new shape)
            if (sourceShape.Master == null)
            {
                Console.Error.WriteLine("The source shape does not have an associated master; cannot clone.");
                return;
            }

            // Add a new shape to the target page using the same master and position
            double pinX = sourceShape.XForm.PinX.Value;
            double pinY = sourceShape.XForm.PinY.Value;
            string masterName = sourceShape.Master.Name;

            long newShapeId = targetPage.AddShape(pinX, pinY, masterName, false);
            Shape clonedShape = targetPage.Shapes.GetShape((int)newShapeId);
            if (clonedShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            // Copy user-defined cells from the source shape to the cloned shape
            foreach (User srcUser in sourceShape.Users)
            {
                User newUser = new User();
                newUser.NameU = srcUser.NameU;
                // Copy Prompt cell value
                newUser.Prompt.Value = srcUser.Prompt.Value;
                // Copy Value cell value
                newUser.Value.Val = srcUser.Value.Val;
                clonedShape.Users.Add(newUser);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape cloned and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}