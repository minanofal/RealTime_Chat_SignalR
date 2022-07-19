using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Message;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Message;
using ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using ChatAppV._0._0._2.EF.Repositories.UnitOFWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.ChatRepository
{
    public class MessageRepository : IMessageRepository
    {
        public IUnitOFWorkRepository _unitOfWork { get; private set; }
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOFWorkRepository(context);
            _context = context;
        }


        public async Task<MessageDisplayDto> SendMessage(MessageInputDto dto)
        {
            var Connection = await _unitOfWork.Connections.GetAsyncTraking(C => C.ConnectionId == dto.ConnectionId);
            if(Connection == null)
                return null;

            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == dto.SenderUserName);
            if (user == null)
                return null;

            if (Connection.User1ID != user.Id && Connection.User2ID != user.Id)
                return null;

            var message = await _unitOfWork.Messages.CreateAsync(new Core.Models.ChatModels.Message 
            { 
                ConnectionId = dto.ConnectionId,
                MessageText = dto.MessageText,
                SenderId =user.Id
            });
            var result = new MessageDisplayDto()
            {
                ConnectionId= dto.ConnectionId,
                SenderUserName = user.UserName,
                MessageText = dto.MessageText,
                CreatedAt = message.CreatedAt,
                Seen = message.Seen,
                Id = message.Id
                
            };

            return result;
        }

        public async Task<IEnumerable<MessageDisplayDto>> GetMessages(Guid id)
        {
           var connection = await _unitOfWork.Connections.GetAsyncTraking(C => C.ConnectionId == id);
            if (connection == null)
                return null;
            var messages = await _unitOfWork.Messages.GetAllAsyncNoTraking(M => M.ConnectionId == id, new[] { "Sender" });
            var result = messages.Select(M => new MessageDisplayDto
            {
                CreatedAt = M.CreatedAt,
                Id = M.Id,
                MessageText = M.MessageText,
                Seen = M.Seen,
                SenderUserName = M.Sender.UserName,
                ConnectionId = id
            });;
            return result;

        }

        public async Task<bool> Seen(Guid id , string username)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == username);
            if (user == null)
                return false;
            var connection = await _unitOfWork.Connections.GetAsyncTraking(C => C.ConnectionId == id);
            
            if (connection == null)
                return false;
            var messages = await _unitOfWork.Messages.GetRangeForEdite(M=>M.ConnectionId == id && !M.Seen && M.SenderId != user.Id);
            if (messages.Count() == 0)
                return false;
             foreach (var m in messages) {
                m.Seen = true;
            }

               

            await _unitOfWork.SaveChanges();
            return true;
            
        }
    }
}
