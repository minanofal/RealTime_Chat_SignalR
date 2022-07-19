using ChatAppV._0._0._2.Core.Models.Auithentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Models.ChatModels
{
    public class FriendRequist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string ResiverId { get; set; }
        public ApplicationUser Resiver { get; set; }

    }
}
