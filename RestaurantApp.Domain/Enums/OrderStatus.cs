using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "Preparing")]
        Preparing,
        [EnumMember(Value = "Ready")]
        Ready,
        [EnumMember(Value = "Delivered")]
        Delivered,
        [EnumMember(Value = "Cancelled")]
        Cancelled
    }


}
