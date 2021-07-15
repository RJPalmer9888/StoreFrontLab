using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFrontLab.DATA.EF//.Metadata
{
    #region Archetype Metadata
    public class ArchetypeMetadata
    {
        [Display(Name = "Archetype")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "* Value must be 20 characters or less.")]
        public string Archetype { get; set; }
    }

    [MetadataType(typeof(ArchetypeMetadata))]
    public partial class Archetype { }
    #endregion

    #region Department Metadata
    public class DepartmentMetadata
    {
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    #endregion

    #region Element Metadata
    public class ElementMetadata
    {
        [Display(Name = "Element")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "* Value must be 20 characters or less.")]
        public string Element { get; set; }
    }

    [MetadataType(typeof(ElementMetadata))]
    public partial class Element { }
    #endregion

    #region Employee Metadata
    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "*Value must be 50 characters or less")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "*Value must be 50 characters or less")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //public Nullable<int> DepartmentID { get; set; }
        //public Nullable<int> ReportsTo { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region InStockStatus Metadata
    public class InStockStatusMetadata
    {
        [Display(Name = "Status")]
        [Required(ErrorMessage = "*")]
        [StringLength(15, ErrorMessage = "* Value must be 15 characters or less.")]
        public string Status { get; set; }
    }

    [MetadataType(typeof(InStockStatusMetadata))]
    public partial class InStockStatus { }
    #endregion

    #region Manufacturer Metadata
    public class ManufacturerMetadata
    {
        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
        public string Manufacturer { get; set; }
    }

    [MetadataType(typeof(ManufacturerMetadata))]
    public partial class Manufacturer { }
    #endregion

    #region Rarity Metadata
    public class RarityMetadata
    {
        [Display(Name = "Rarity")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "* Value must be 20 characters or less.")]
        public string Rarity { get; set; }
    }

    [MetadataType(typeof(RarityMetadata))]
    public partial class Rarity { }
    #endregion

    #region Weapon Metadata
    public class WeaponMetadata
    {
        [Display(Name = "Weapon Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "* Value must be 30 characters or less.")]
        public string WeaponName { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Required(ErrorMessage = "An archetype is required")]
        public int ArchetypeID { get; set; }

        [Required(ErrorMessage = "An element is required")]
        public int ElementID { get; set; }

        [Required(ErrorMessage = "A rarity is required")]
        public int RarityID { get; set; }

        //public Nullable<int> ManufacturerID { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required(ErrorMessage = "A stock status is required")]
        public int InStockID { get; set; }
    }

    [MetadataType(typeof(WeaponMetadata))]
    public partial class Weapon { }
    #endregion
}
