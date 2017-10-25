using AvaritiaBot.Enums;
using AvaritiaBot.Models;
using Discord;
using Discord.WebSocket;

namespace AvaritiaBot.Helper
{
    public static class CharacterInfoCardHelper
    {
        public static EmbedBuilder GetCharacterInfoCard(Character character, SocketUser owner, SocketGuild guild)
        {
            int higherAp = character.Ap > character.AAp ? character.Ap : character.AAp;
            EmbedAuthorBuilder author = new EmbedAuthorBuilder()
            {
                Name = owner.Username,
                IconUrl = owner.GetAvatarUrl(),
            };
            EmbedFooterBuilder footer = new EmbedFooterBuilder()
            {
                Text = character.IsActive ? "This character is the main - Character Data from" : "This character is not active - Character Data from",
                IconUrl = guild.IconUrl
            };
            var builder = new EmbedBuilder()
            {
                Color = new Color(0, 191, 255),
                Description = $"```prolog\r\nName: {character.Name}\r\nLevel: {character.Level}\r\nAp: {character.Ap}\r\nAAp: {character.AAp}\r\nDp: {character.Dp}\r\nGS: {higherAp + character.Dp}\r\nScreenShot: {character.Url}```",
                ImageUrl = character.Url,
                Author = author,
                ThumbnailUrl = ClassHelper.GetClassImageUrl((Classes)character.Class),
                Footer = footer,
                Timestamp = character.LastUpdate
            };
            return builder;
        }
    }
}
