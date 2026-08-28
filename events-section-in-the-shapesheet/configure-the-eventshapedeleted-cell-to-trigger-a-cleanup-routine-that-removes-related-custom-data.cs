using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the target shape by its universal name (adjust as needed)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "TargetShape")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("Target shape not found.");
                    return;
                }

                // Configure the shape's deletion event to call a cleanup routine.
                // The formula uses CALLTHIS to invoke a VBA macro named "CleanupRoutine".
                // Adjust the macro name if a different implementation is required.
                targetShape.Event.EventDrop.Ufe.F = "CALLTHIS(\"CleanupRoutine\")";

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Event cell configured and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // This method represents the cleanup routine that would be called by the event.
        // In a real Visio environment, the CALLTHIS macro would invoke this logic.
        // Here we provide a C# implementation that can be called manually if needed.
        static void CleanupCustomData(Diagram diagram, long shapeId)
        {
            // Locate the shape on the first page (adjust if multiple pages are used)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Remove all custom properties (Props)
            if (shape.Props != null)
            {
                List<Prop> propsToRemove = new List<Prop>();
                foreach (Prop prop in shape.Props)
                {
                    propsToRemove.Add(prop);
                }

                foreach (Prop prop in propsToRemove)
                {
                    shape.Props.Remove(prop);
                }
            }

            // Remove all user-defined cells (Users)
            if (shape.Users != null)
            {
                List<User> usersToRemove = new List<User>();
                foreach (User user in shape.Users)
                {
                    usersToRemove.Add(user);
                }

                foreach (User user in usersToRemove)
                {
                    shape.Users.Remove(user);
                }
            }

            Console.WriteLine($"Custom data removed from shape ID {shapeId}.");
        }
    }