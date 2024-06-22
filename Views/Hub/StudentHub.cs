using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class StudentHub : Hub
{
    public async Task SendStudentRecord(string data)
    {
        await Clients.All.SendAsync("ReceiveStudentRecord", data);
    }
}
