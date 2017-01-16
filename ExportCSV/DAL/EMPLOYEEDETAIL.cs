namespace ExportCSV.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPLOYEEDETAILS")]
    public partial class EmployeeDetail
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string County { get; set; }

        [StringLength(100)]
        public string Postal { get; set; }

        [StringLength(100)]
        public string Phone1 { get; set; }

        [StringLength(100)]
        public string Phone2 { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Web { get; set; }
    }
}
