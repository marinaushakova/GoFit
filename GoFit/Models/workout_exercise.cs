//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoFit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class workout_exercise
    {
        public int id { get; set; }
        public int workout_id { get; set; }
        public int exercise_id { get; set; }
        public int position { get; set; }
        public decimal duration { get; set; }
        public System.DateTime timestamp { get; set; }
    
        public virtual exercise exercise { get; set; }
        public virtual workout workout { get; set; }
    }
}