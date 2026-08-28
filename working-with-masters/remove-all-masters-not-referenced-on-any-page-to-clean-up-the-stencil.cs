using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input stencil path and optional output path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <stencilPath> [outputPath]");
            return;
        }

        string stencilPath = args[0];
        // Verify the stencil file exists.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        // Determine output path – use provided or create a new file name.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(stencilPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(stencilPath) + "_cleaned" + Path.GetExtension(stencilPath));

        // Load the diagram (stencil) inside a try/catch to capture Aspose errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(stencilPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load stencil: {ex.Message}");
            return;
        }

        // Collect IDs of masters that are actually used by shapes on any page.
        var usedMasterIds = new HashSet<int>();
        try
        {
            foreach (Page page in diagram.Pages) // iterate all pages
            {
                foreach (Shape shape in page.Shapes) // iterate all shapes on the page
                {
                    // If the shape has an associated master, record its ID.
                    if (shape.Master != null)
                    {
                        usedMasterIds.Add(shape.Master.ID);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while scanning pages: {ex.Message}");
            diagram.Dispose();
            return;
        }

        // Identify masters that are not referenced anywhere.
        var mastersToRemove = new List<Master>();
        try
        {
            foreach (Master master in diagram.Masters) // iterate master collection
            {
                if (!usedMasterIds.Contains(master.ID))
                {
                    mastersToRemove.Add(master); // mark for removal
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while enumerating masters: {ex.Message}");
            diagram.Dispose();
            return;
        }

        // Remove the unused masters from the diagram.
        try
        {
            foreach (Master master in mastersToRemove)
            {
                diagram.Masters.Remove(master);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while removing masters: {ex.Message}");
            diagram.Dispose();
            return;
        }

        // Save the cleaned stencil using the appropriate format.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vssx);
            Console.WriteLine($"Cleaned stencil saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save cleaned stencil: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            diagram.Dispose();
        }
    }
}