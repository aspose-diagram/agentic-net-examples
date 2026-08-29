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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve user-defined cells "Length" and "Width"
                    double length = 0;
                    double width = 0;
                    bool hasLength = false;
                    bool hasWidth = false;

                    foreach (User user in shape.Users)
                    {
                        if (user.Name.Equals("Length", StringComparison.OrdinalIgnoreCase))
                        {
                            hasLength = double.TryParse(user.Value.Val, out length);
                        }
                        else if (user.Name.Equals("Width", StringComparison.OrdinalIgnoreCase))
                        {
                            hasWidth = double.TryParse(user.Value.Val, out width);
                        }
                    }

                    // If both cells exist and contain valid numbers, calculate Area
                    if (hasLength && hasWidth)
                    {
                        double area = length * width;

                        // Check if an "Area" user-defined cell already exists
                        User areaCell = null;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name.Equals("Area", StringComparison.OrdinalIgnoreCase))
                            {
                                areaCell = user;
                                break;
                            }
                        }

                        if (areaCell == null)
                        {
                            // Create a new user-defined cell for Area
                            areaCell = new User();
                            areaCell.Name = "Area";
                            shape.Users.Add(areaCell);
                        }

                        // Set the calculated value
                        areaCell.Value.Val = area.ToString();
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
