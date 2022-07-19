using ChatAppV._0._0._2.Core.Models.Auithentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Models.ChatModels
{
    public class Connection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ConnectionId { get; set; }
        public string User1ID { get; set; }
        public string User2ID { get; set; }

        public ApplicationUser User1 { get; set; }
        public ApplicationUser User2 { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
