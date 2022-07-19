using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Inputs.Connection;
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
    public class ConnectionRepository : IConnectionRepository
    {
        public IUnitOFWorkRepository _unitOfWork { get; private set; }
        private readonly IMapper _mapper;
        public ConnectionRepository(ApplicationDbContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOFWorkRepository(context);
            _mapper = mapper;


        }
        public async Task<IEnumerable<Friends>> GetAllFriends(string userName)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == userName);
            if (user == null)
                return null;
            var friends = await _unitOfWork.Connections
                .GetAllAsyncNoTraking(C => C.User1ID == user.Id, new[] { "User2", "Messages" });
            var result = new List<Friends>();
            result.AddRange(friends.Select
               (C => new Friends()
               {
                   ConnectionId = C.ConnectionId,
                   Email = C.User2.Email,
                   FirstName = C.User2.FirstName,
                   LastName = C.User2.LastName,
                   ImageUrl = C.User2.ImageUrl,
                   UserName = C.User2.UserName,
                   LastMessage = C.Messages.Select(m => m.MessageText).LastOrDefault(),
                   IsSeen = C.Messages.Select(M => M.Seen).LastOrDefault(),
                   SentAt = C.Messages.Select(M => M.CreatedAt).LastOrDefault(),
                   SenderUserName = C.Messages.Select(m => m.SenderId).LastOrDefault(),
                   UnseenMessagesCount = C.Messages.Where(M => !M.Seen).ToList().Count()
               }
               ).ToList());

            friends = await _unitOfWork.Connections
                .GetAllAsyncNoTraking(C => C.User2ID == user.Id, new[] { "User1", "Messages" });

            result.AddRange(friends.Select
                (C => new Friends()
                {
                    ConnectionId = C.ConnectionId,
                    Email = C.User1.Email,
                    FirstName = C.User1.FirstName,
                    LastName = C.User1.LastName,
                    ImageUrl = C.User1.ImageUrl,
                    UserName = C.User1.UserName,
                    LastMessage = C.Messages.Select(m => m.MessageText).LastOrDefault(),
                    IsSeen = C.Messages.Select(M => M.Seen).LastOrDefault(),
                    SentAt = C.Messages.Select(M => M.CreatedAt).LastOrDefault(),
                    SenderUserName = C.Messages.Select(m => m.SenderId).LastOrDefault(),
                    UnseenMessagesCount = C.Messages.Where(M=> !M.Seen).ToList().Count()

                }
                ).ToList());

            foreach (var r in result) {
                if (r.SenderUserName != null)
                {
                    var temp = await _unitOfWork.Users.GetAsyncTraking(U => U.Id == r.SenderUserName);
                    r.SenderUserName = temp.UserName;
                }
            }
            return result.OrderByDescending(R=>R.SentAt);

        }

        public async Task<IEnumerable<Friends>> GetFriendSearch(string userName, string search)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == userName);
            if (user == null)
                return null;
            var userSearch = await _unitOfWork.Users.GetAllAsyncNoTraking(U=> U.UserName.Contains(search));
            var friends = await _unitOfWork.Connections
                .GetAllAsyncNoTraking(C => C.User1ID == user.Id && userSearch.Select(s => s.Id).Contains(C.User2ID) , new[] { "User2", "Messages" });
            var result = new List<Friends>();
            result.AddRange(friends.Select
               (C => new Friends()
               {
                   ConnectionId = C.ConnectionId,
                   Email = C.User2.Email,
                   FirstName = C.User2.FirstName,
                   LastName = C.User2.LastName,
                   ImageUrl = C.User2.ImageUrl,
                   UserName = C.User2.UserName,
                   LastMessage = C.Messages.Select(m => m.MessageText).LastOrDefault(),
                   IsSeen = C.Messages.Select(M => M.Seen).LastOrDefault(),
                   SentAt = C.Messages.Select(M => M.CreatedAt).LastOrDefault(),
                   SenderUserName = C.Messages.Select(m => m.SenderId).LastOrDefault(),
                   UnseenMessagesCount = C.Messages.Where(M => !M.Seen).ToList().Count()


               }
               ).ToList());

            friends = await _unitOfWork.Connections
                .GetAllAsyncNoTraking(C => C.User2ID == user.Id && userSearch.Select(s => s.Id).Contains(C.User1ID), new[] { "User1", "Messages" });

            result.AddRange(friends.Select
                (C => new Friends()
                {
                    ConnectionId = C.ConnectionId,
                    Email = C.User1.Email,
                    FirstName = C.User1.FirstName,
                    LastName = C.User1.LastName,
                    ImageUrl = C.User1.ImageUrl,
                    UserName = C.User1.UserName,
                    LastMessage = C.Messages.Select(m => m.MessageText).LastOrDefault(),
                    IsSeen = C.Messages.Select(M => M.Seen).LastOrDefault(),
                    SentAt = C.Messages.Select(M => M.CreatedAt).LastOrDefault(),
                    SenderUserName = C.Messages.Select(m => m.SenderId).LastOrDefault(),
                    UnseenMessagesCount = C.Messages.Where(M => !M.Seen).ToList().Count()
                }
                ).ToList());
            foreach (var r in result)
            {
                if (r.SenderUserName != null)
                {
                    var temp = await _unitOfWork.Users.GetAsyncTraking(U => U.Id == r.SenderUserName);
                    r.SenderUserName = temp.UserName;
                }
            }
            return result;
        }
    }
}
