/* *******************************************************************************************
 * CommandRebarComments.cs
 * 
 * Rebar Comments (API REVIT 2023) 
 * Application (add-ins)
 * -------------------------------------------------------------------------------------------
 * Visual Studio 2022 
 * C# | .NET Framework 4.8
 * -------------------------------------------------------------------------------------------
 * Revit API docs: 
 * https://www.revitapidocs.com/2023/
 * 
 * Writing sgiman @ 2024 
 * *******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

/*
 * R e b a r s 
 * comments (length, diameter)
 * version 2 (FilteredElementCollector)
 */

namespace RebarComments
{
    //****************************
    // Command: Rebars Comments
    //****************************
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RebarCommentsAdd : IExternalCommand
    {
        public object ActiveUIDocument { get; private set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application documents. 
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            // --- RebarCollectorСomments ---
            RebarCollectorСomments sc = new RebarCollectorСomments();
            List<Rebar> ListRebars = sc.GetRebars_Class(doc);
            //List<Rebar> ListRebars = sc.GetRebars_Category(doc);

            // OUTPUT TEST
            if (ListRebars.Count != 0)
            {
                //TaskDialog.Show("Structural Rebars", "\n---LENGTH and RADIUS added in comments (Structural Rebars):  \n" + SB(ListRebars).ToString());
                TaskDialog.Show("Structural Rebars", "FOUND REBARS: " + ListRebars.Count);
            }
            else
            {
                TaskDialog.Show("Structural Rebars", "REBARS NOT FOUND");
                return Result.Succeeded;
            }

            
            // --- T R A N S A C T I O N ---
            using (Transaction transaction = new Transaction(doc, "Rebar Comments"))
            {
                transaction.Start();

                foreach (Rebar rebar in ListRebars)
                {
                    // --- Get Length (properties) ---
                    //var val_length = rebar.get_Parameter(BuiltInParameter.REBAR_ELEM_TOTAL_LENGTH).AsDouble();
                    double val_length = rebar.get_Parameter(BuiltInParameter.REBAR_ELEM_TOTAL_LENGTH).AsDouble();
                    double val_length_metric = UnitUtils.ConvertFromInternalUnits(val_length, UnitTypeId.Millimeters);

                    // --- Get Radius (type properties) ---
                    double val_radius = rebar.get_Parameter(BuiltInParameter.REBAR_BAR_DIAMETER).AsDouble();
                    double val_radius_metric = UnitUtils.ConvertFromInternalUnits(val_radius, UnitTypeId.Millimeters);

                    // --- GET-SET parameter ("Comments") ---
                    string str = "Ø" + val_radius_metric + " ,Grad 60, L=" + val_length_metric;
                    rebar.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(str);
                }

                transaction.Commit();
            }

                return Result.Succeeded;

        } // --- class Command : IExternalCommand ---


        //********************
        // StringBuilder
        //********************
        public StringBuilder SB(List<Rebar> Rebars)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Rebar r in Rebars)
            {
                sb.Append(r.Name + " " + r.Id + "\n");
            }

            return sb;
        }

    } // --- class RebarComments : IExternalCommand ---

}
