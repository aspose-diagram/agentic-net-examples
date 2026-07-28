using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Example: Change global document protection settings
                SetDocumentProtection(diagram, protectBackgrounds: BOOL.True);
                SetDocumentProtection(diagram, protectMasters: BOOL.False);
                SetDocumentProtection(diagram, protectShapes: BOOL.True);
                SetDocumentProtection(diagram, protectStyles: BOOL.False);

                // Example: Change protection on a specific shape (first shape on first page)
                if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                {
                    Shape shape = diagram.Pages[0].Shapes[0];
                    long shapeId = shape.ID;

                    SetShapeProtection(shape, "LockMoveX", BOOL.True);
                    SetShapeProtection(shape, "LockMoveY", BOOL.False);
                    SetShapeProtection(shape, "LockWidth", BOOL.True);
                    SetShapeProtection(shape, "LockHeight", BOOL.True);
                    SetShapeProtection(shape, "LockRotate", BOOL.False);
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

        // Sets a global document protection property and logs the change
        static void SetDocumentProtection(Diagram diagram, BOOL? protectBackgrounds = null,
                                          BOOL? protectMasters = null, BOOL? protectShapes = null,
                                          BOOL? protectStyles = null)
        {
            if (protectBackgrounds.HasValue)
            {
                diagram.DocumentSettings.ProtectBkgnds = protectBackgrounds.Value;
                LogProtectionChange("Document", "ProtectBkgnds", protectBackgrounds.Value);
            }

            if (protectMasters.HasValue)
            {
                diagram.DocumentSettings.ProtectMasters = protectMasters.Value;
                LogProtectionChange("Document", "ProtectMasters", protectMasters.Value);
            }

            if (protectShapes.HasValue)
            {
                diagram.DocumentSettings.ProtectShapes = protectShapes.Value;
                LogProtectionChange("Document", "ProtectShapes", protectShapes.Value);
            }

            if (protectStyles.HasValue)
            {
                diagram.DocumentSettings.ProtectStyles = protectStyles.Value;
                LogProtectionChange("Document", "ProtectStyles", protectStyles.Value);
            }
        }

        // Sets a specific protection flag on a shape and logs the change
        static void SetShapeProtection(Shape shape, string propertyName, BOOL value)
        {
            switch (propertyName)
            {
                case "LockMoveX":
                    shape.Protection.LockMoveX.Value = value;
                    break;
                case "LockMoveY":
                    shape.Protection.LockMoveY.Value = value;
                    break;
                case "LockWidth":
                    shape.Protection.LockWidth.Value = value;
                    break;
                case "LockHeight":
                    shape.Protection.LockHeight.Value = value;
                    break;
                case "LockRotate":
                    shape.Protection.LockRotate.Value = value;
                    break;
                case "LockDelete":
                    shape.Protection.LockDelete.Value = value;
                    break;
                // Add additional cases as needed for other protection properties
                default:
                    throw new Exception($"Unsupported protection property: {propertyName}");
            }

            LogProtectionChange($"Shape ID {shape.ID}", propertyName, value);
        }

        // Centralized logging method
        static void LogProtectionChange(string elementIdentifier, string propertyName, BOOL value)
        {
            string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
            Console.WriteLine($"{timestamp} - {elementIdentifier} - {propertyName} set to {value}");
        }
    }