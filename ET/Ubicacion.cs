//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ET
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ubicacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ubicacion()
        {
            this.Producto = new HashSet<Producto>();
        }
    
        public int ID_Ubicacion { get; set; }
        public bool Activo { get; set; }
        public int ID_Rack { get; set; }
        public int ID_Bodega { get; set; }
        public Nullable<bool> Ocupado { get; set; }
    
        public virtual Bodega Bodega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual Rack Rack { get; set; }
    }
}
