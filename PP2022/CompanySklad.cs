//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PP2022
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanySklad
    {
        public int ID { get; set; }
        public int IDCompany { get; set; }
        public int IDSklad { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Sklad Sklad { get; set; }
    }
}
