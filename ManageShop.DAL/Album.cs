//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManageShop.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string AlbumId { get; set; }
        public Nullable<bool> HasPosted { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string PhotoCover { get; set; }
        public string FbLink { get; set; }
        public string Caterogies { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
