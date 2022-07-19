using ChatAppV._0._0._2.Core.DTOS.Inputs.Message;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Message;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories
{
    public interface IMessageRepository
    {
        IUnitOFWorkRepository _unitOfWork { get; }
        Task<MessageDisplayDto> SendMessage(MessageInputDto dto);
        Task<IEnumerable<MessageDisplayDto>> GetMessages(Guid id);
        Task<bool> Seen(Guid id , string username);

    }
}
