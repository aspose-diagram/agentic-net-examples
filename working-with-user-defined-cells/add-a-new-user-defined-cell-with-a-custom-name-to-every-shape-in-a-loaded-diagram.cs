using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Create a new user-defined cell
                    User customCell = new User();
                    customCell.Name = "MyCustomCell";
                    customCell.Value.Val = "CustomValue";
                    customCell.Prompt.Value = "Custom user-defined cell";

                    // Add the cell to the shape
                    shape.Users.Add(customCell);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
