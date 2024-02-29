using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace RebarComments
{
    class RebarCollectorСomments
    {
        //*****************************
        // GetRebars_Class
        //*****************************
        public List<Rebar> GetRebars_Class(Document doc)
        {
            //FilteredElementCollector collector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Rebar);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            ICollection<Element> Rebars = collector.OfClass(typeof(Rebar)).ToElements();
            
            List<Rebar> List_Rebars = new List<Rebar>();
            
            foreach (Rebar r in Rebars)
            {
                List_Rebars.Add(r);
            }
            return List_Rebars;
        }

        //*****************************
        // GetRebar_Category
        //*****************************
       /*
        public List<Rebar> GetRebars_Category(Document doc)
        {
            IList<Element> collector = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rebar)
                .WhereElementIsNotElementType().ToElements();

            List<Rebar> List_Rebars = new List<Rebar>();

            foreach (Element e in collector)
            {
                Rebar r = e as Rebar;
                List_Rebars.Add(r);
            }
            return List_Rebars;
        }
       */

    }

}
