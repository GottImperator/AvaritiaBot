using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace AvaritiaBot.Modules
{
    [Name("Announcement")]
    public class AnnouncementModule : ModuleBase<SocketCommandContext>
    {
        [Command("announce"), Priority(0)]
        [Summary("Announce an event to the guild")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Announce(SocketTextChannel channel, [Remainder]string announcement)
        {
            await channel.SendMessageAsync(announcement);
        }
        [Command("announceBoss"), Priority(0)]
        [Summary("Announce an guild boss to the guild")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AnnounceGuildBoss(SocketTextChannel channel, string date,string summonTime, string startTime, string bdoChannel)
        {
            string response = string.Format("@Member We're gonna be doing Kamasylvia Guild Bosses on {0}.\r\n\r\nChannel: {1}\r\n\r\nMeet at Mirumok Ruins at {2} CEST(Or get summoned around that time)\r\n\r\n**Bosses start at {3} CEST** \r\n\r\n__What you need: 1 hour + free of time, potions -Musket if possible.__", date, bdoChannel, summonTime, startTime);
            await channel.SendMessageAsync(response);
        }
    }
}
