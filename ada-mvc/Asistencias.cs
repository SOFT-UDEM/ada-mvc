//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ada_mvc
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asistencias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asistencias()
        {
            this.DetalleDeAsistencias = new HashSet<DetalleDeAsistencias>();
        }
    
        public int IdAsistencia { get; set; }
        public Nullable<int> CodEmpleado { get; set; }
    
        public virtual Empleados Empleados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleDeAsistencias> DetalleDeAsistencias { get; set; }
    }
}
