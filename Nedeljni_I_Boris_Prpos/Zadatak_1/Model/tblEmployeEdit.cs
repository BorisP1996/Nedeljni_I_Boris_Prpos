//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmployeEdit
    {
        public int EmployeEditID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<double> Salary { get; set; }
        public Nullable<int> ExperienceYear { get; set; }
        public Nullable<int> EducationID { get; set; }
        public string EditStatus { get; set; }
        public int ManagerID { get; set; }
    }
}