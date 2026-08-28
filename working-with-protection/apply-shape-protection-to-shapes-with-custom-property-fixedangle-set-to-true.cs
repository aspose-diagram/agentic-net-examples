using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ShapeProtectionExample <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a custom property named "FixedAngle".
                    bool hasFixedAngleTrue = false;
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "FixedAngle" &&
                                prop.Value != null &&
                                prop.Value.Val != null &&
                                prop.Value.Val.Equals("true", StringComparison.OrdinalIgnoreCase))
                            {
                                hasFixedAngleTrue = true;
                                break;
                            }
                        }
                    }

                    // If the custom property is set to true, apply protection.
                    if (hasFixedAngleTrue)
                    {
                        // Lock rotation to prevent the shape from being rotated.
                        shape.Protection.LockRotate.Value = BOOL.True;

                        // Optionally lock other aspects (example: lock moving on X/Y).
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;

                        Console.WriteLine($"Applied protection to shape ID {shape.ID} on page '{page.Name}'.");
                    }
                }
            }

            // Save the modified diagram using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }