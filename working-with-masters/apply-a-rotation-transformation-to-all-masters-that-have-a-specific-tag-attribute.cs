using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_rotated.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the tag name and value to look for
                const string tagPropName = "Tag";
                const string tagPropValue = "Rotate";

                // Desired rotation angle in degrees
                double rotationDegrees = 45.0;
                // Convert degrees to radians (Angle cell expects radians)
                double rotationRadians = rotationDegrees * Math.PI / 180.0;

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    bool masterHasTag = false;

                    // Check each shape within the master for the specific tag property
                    foreach (Shape masterShape in master.Shapes)
                    {
                        if (masterShape.Props != null)
                        {
                            foreach (Prop prop in masterShape.Props)
                            {
                                if (prop.Name == tagPropName && prop.Value.Val == tagPropValue)
                                {
                                    masterHasTag = true;
                                    break;
                                }
                            }
                        }

                        if (masterHasTag)
                            break;
                    }

                    // If the master contains the tag, apply rotation to all its shapes
                    if (masterHasTag)
                    {
                        foreach (Shape masterShape in master.Shapes)
                        {
                            // Ensure the shape has an XForm cell collection
                            if (masterShape.XForm != null && masterShape.XForm.Angle != null)
                            {
                                // Set the rotation angle (in radians)
                                masterShape.XForm.Angle.Value = rotationRadians;
                            }
                        }
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