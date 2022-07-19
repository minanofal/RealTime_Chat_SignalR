using ChatAppV._0._0._2.Core.DTOS.Inputs.Message;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Message;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Api.HubConfig
{ 
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task JionChat(Guid userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ToString());
            

        }
        public async Task RealTimeProfile(string UserName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, UserName);
        }
        //public async Task sendMessage(MessageInputDto dto)
        //{
        //    //var message = await _unitOfWork.messages.SendMessage(dto);
   
        //    await Clients.Groups(dto.ConnectionId.ToString()).SendAsync("ResiveMessage",message);
        //}

    }
}
