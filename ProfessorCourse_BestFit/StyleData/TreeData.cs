using ProfessorCourse_BestFit.DAL;
using ProfessorCourse_BestFit.Models;
using ProfessorCourse_BestFit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace ProfessorCourse_BestFit.StyleData
{
    public class TreeData 
    {
        private readonly ProfessorCourseBestFitEntities _context;
        private readonly Department_DAL department_DAL;

        public TreeData()
        {
            _context = new ProfessorCourseBestFitEntities();
            department_DAL = new Department_DAL();
        }

        // Populates a TreeView control with example nodes. 
        private void InitializeTreeView()
        {
            TreeView treeView1 = new TreeView();
            treeView1.BeginUpdate();
            treeView1.Nodes.Add("Parent");
            treeView1.Nodes[0].Nodes.Add("Child 1");
            treeView1.Nodes[0].Nodes.Add("Child 2");
            treeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            treeView1.EndUpdate();
        }
    }
}