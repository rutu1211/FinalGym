
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace The_Gym
{

using System;
    using System.Collections.Generic;
    
public partial class Fee
{

    public int ID { get; set; }

    public Nullable<int> GYM_ID { get; set; }

    public Nullable<int> Student_ID { get; set; }

    public Nullable<int> Branch_ID { get; set; }

    public Nullable<int> Plane_ID { get; set; }

    public Nullable<int> Offer_ID { get; set; }

    public Nullable<System.DateTime> Start_Date { get; set; }

    public Nullable<System.DateTime> End_Date { get; set; }

    public Nullable<System.DateTime> Payment_Date { get; set; }

    public Nullable<int> Discount_On_Bill_Type { get; set; }

    public Nullable<decimal> Discount_On_Bill { get; set; }

    public Nullable<decimal> Payment_Amount { get; set; }

    public string GST { get; set; }

    public Nullable<decimal> GST_Amount { get; set; }

    public Nullable<bool> Status { get; set; }

}

}
