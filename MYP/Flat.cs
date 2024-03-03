//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MYP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flat()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int id { get; set; }
        public string photo { get; set; }
        public int price { get; set; }
        public int rooms { get; set; }
        public double area { get; set; }
        public int floor { get; set; }
        public int purchased { get; set; }

        public string answer
        {
            get
            {
                if (purchased == 0)
                    return "Нет";
                else if (purchased == 1)
                    return "Да";
                else
                    return "Удалена";
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
