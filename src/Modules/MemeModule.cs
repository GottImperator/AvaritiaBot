using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AvaritiaBot.Modules
{
    [Name("Meme")]
    public class MemeModule : ModuleBase<SocketCommandContext>
    {
        [Command("staranise")]
        [Summary("How do I make it?")]
        public async Task StarAnise()
        {
            await ReplyAsync($"Star anise can be used whole or ground. When whole, it usually is added to liquids destined for a slow simmer or braise. It usually is removed and discarded from the dish before serving.  - Ask Infe for further info/elaberation");
        }
        [Command("microwave")]
        [Summary("Potato?")]
        public async Task Microwave()
        {
            await ReplyAsync($"https://thumbs.gfycat.com/WindyHeartfeltKid-max-1mb.gif");
        }
        [Command("wow")]
        [Summary("Wow?")]
        public async Task Wow()
        {
            await ReplyAsync($"http://i.makeagif.com/media/7-31-2015/jL0b6u.gif");
        }
        [Command("bigenough")]
        [Summary("Is this guild big enough?")]
        public async Task BigEnough()
        {
            await ReplyAsync($"https://youtu.be/CCVdQ8xXBfk");
        }
        [Command("baby")]
        [Summary("Is it tasteful?")]
        public async Task Baby()
        {
            await ReplyAsync($"http://static.tvtropes.org/pmwiki/pub/images/EatsBabies2_4006.JPG");
            await ReplyAsync($"Rare picture of Daeriel in the 14th century");
        }
        [Command("daeriel")]
        [Summary("What is it?")]
        public async Task Daeriel()
        {
            await ReplyAsync($"A Daeriel (from Koine Greek όρχεων ξωτικό) is a supernatural and often malevolent being prevalent in religion, occultism, literature, fiction, mythology and folklore. In Ancient Near Eastern religions as well as in the Abrahamic traditions, including ancient and medieval Christian daeriellogy, a Daeriel is considered a harmful spiritual entity, below the heavenly planes which may cause demonic possession, calling for an exorcism. In Western occultism and Renaissance magic, which grew out of an amalgamation of Greco - Roman magic, Jewish Aggadah and Christian daeriellogy, a Daeriel is believed to be a spiritual entity that may be conjured and controlled.");
        }
        [Command("SKRRRRRA")]
        [Summary("THE TING GOES SKRRRRRA")]
        public async Task SkrrrrrA()
        {
            await ReplyAsync($"https://youtu.be/g09bOoPGdTE");
        }
        [Command("Yee")]
        [Summary("Yee")]
        public async Task Yee()
        {
            await ReplyAsync($"https://youtu.be/q6EoRBvdVPQ");
        }
        [Command("Bagger288")]
        [Summary("Music video")]
        public async Task Bagger()
        {
            await ReplyAsync($"https://youtu.be/azEvfD4C6ow");
        }
        [Command("OhGodDammit")]
        [Summary("Dammit")]
        public async Task OhGodDammit()
        {
            await ReplyAsync($"https://youtu.be/KaeYczuhDqw");
        }
    }
}
