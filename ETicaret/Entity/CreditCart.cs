using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Entity
{
    public class CreditCart
    {
        public int Id { get; set; }
        public string CartName { get; set; }
        public virtual List<Taksit> Taksits{ get; set; }

}

   public class Taksit
    {
        public int Id { get; set; }
        public int CreditCartId { get; set; }
        public int CategoryId { get; set; }
        public int TaksitSayisi { get; set; }
        public virtual Category Cat { get; set; }
        public virtual CreditCart Credit { get; set; }

         

    }
}