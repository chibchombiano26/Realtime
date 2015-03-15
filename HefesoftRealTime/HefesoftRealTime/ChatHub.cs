using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Hefesoft.Standard.Util.table;
using System.Threading.Tasks;

namespace HefesoftRealTime
{
    public class ChatHub : Hub
    {
        public void Send(string who, string message)
        {
            var name = Context.User.Identity.Name;

            if(string.IsNullOrEmpty(name))
            {
                name = "no indicado";
            }

            //Se debe cambiar el primero por who
            //Y hay que crear un sistema de autenticacion para que el nombre llegue en el identity
            var result = HefesoftRealTime.Estaticas.Table_Storage.obtenerTableStorage().send(name, message, name, Clients);

            if(result.tieneElementos)
            {
                foreach (var item in result.Listado)
                {                    
                    Clients.Client(item.RowKey).broadcastMessage(name, message);
                }
            }
        }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            
            if(string.IsNullOrEmpty(name))
            {
                name = "no indicado";
            }

            HefesoftRealTime.Estaticas.Table_Storage.obtenerTableStorage().insert(name, this.Context);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;

            if (string.IsNullOrEmpty(name))
            {
                name = "no indicado";
            }

            HefesoftRealTime.Estaticas.Table_Storage.obtenerTableStorage().delete(name, this.Context);
            return base.OnDisconnected(stopCalled);
        }
    }
}