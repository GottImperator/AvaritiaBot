using AvaritiaBot.Helper;
using AvaritiaBot.Models;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaritiaBot.Modules
{
    /// <summary>
    /// This Module is used to manipulate the Characters.
    /// </summary>
    [Name("Character")]
    public class CharacterModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// The database helper used by this module.
        /// </summary>
        private static CharacterDatabaseHelper DbHelper = new CharacterDatabaseHelper();
        /// <summary>
        /// Adds a character into the database.
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <param name="ap">The attack points</param>
        /// <param name="aAp">The awakend attack points</param>
        /// <param name="dp">The defense points</param>
        /// <param name="level">The current level</param>
        /// <param name="className">The className</param>
        /// <param name="gearUrl">The url of the gear</param>
        /// <returns></returns>
        [Command("addChar"), Priority(0)]
        [Summary("Adds a character to your character list")]
        public async Task AddChar(string name, int ap, int aAp, int dp, int level, string className, string gearUrl)
        {
            try
            {
                bool isActive = false;
                // Set the character default on on if the user doesn't have one
                if (DbHelper.GetCharactersByDiscordId(Context.User.Id).Count == 0)
                {
                    isActive = true;
                }
                Character character = new Character()
                {
                    OwnerID = base.Context.User.Id,
                    Name = name,
                    Ap = ap,
                    AAp = aAp,
                    Dp = dp,
                    Url = gearUrl,
                    Level = level,
                    IsActive = isActive,
                    Class = (uint)ClassHelper.GetClassFromString(className)
                };
                // Add the character
                CharacterModule.DbHelper.AddCharacter(character);
                await ReplyAsync($"{base.Context.User.Mention} I have added {name} to the database!");
            }
            catch (Exception exc)
            {
                await ReplyAsync($"{base.Context.User.Mention}: {exc.Message}");
            }
        }
        /// <summary>
        /// Deletes a character witch you own.
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <returns></returns>
        [Command("deleteChar"), Priority(0)]
        [Summary("Deletes a character from the database")]
        public async Task DeleteChar(string name)
        {
            List<Character> characters = DbHelper.GetCharacterByName(name);
            if (characters.Count == 0)
            {
                await ReplyAsync($"{base.Context.User.Mention}: Can't find a character with the name **{name}**.");
            }
            else
            {
                if (characters[0].OwnerID == Context.User.Id)
                {
                    DbHelper.DeleteChar(characters[0]);
                    await ReplyAsync($"{base.Context.User.Mention}:Deleted the character **{name}**!");
                }
                else
                {
                    await ReplyAsync($"{base.Context.User.Mention}:You don't own the character **{name}**!");
                }
            }
        }
        /// <summary>
        /// Deletes a character.
        /// </summary>
        /// <param name="user">The owner of the character</param>
        /// <param name="name">The name of the character</param>
        /// <returns></returns>
        [Command("deleteChar"), Priority(1)]
        [Summary("Deletes a char from the database")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DeleteCharUser(SocketUser user, string name)
        {
            List<Character> characters = DbHelper.GetCharacterByName(name);
            if (characters.Count == 0)
            {
                await ReplyAsync($"{base.Context.User.Mention}: Can't find a character with the name **{name}**.");
            }
            else
            {
                if (characters[0].OwnerID == user.Id)
                {
                    DbHelper.DeleteChar(characters[0]);
                    await ReplyAsync($"{base.Context.User.Mention}:Deleted the character **{name}**!");
                }
                else
                {
                    await ReplyAsync($"{base.Context.User.Mention}:The user doesn't not own the character**{name}**!");
                }
            }
        }
        /// <summary>
        /// Gets a character from the database.
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <returns></returns>
        [Command("getChar"), Priority(0)]
        [Summary("Gets a character from the database")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetChar(string name)
        {
            List<Character> characters = DbHelper.GetCharacterByName(name);
            foreach (Character character in characters)
            {
                await ReplyAsync("", false, CharacterInfoCardHelper.GetCharacterInfoCard(character, base.Context.Client.GetUser(character.OwnerID), base.Context.Guild).Build());
            }
            if (characters.Count == 0)
            {
                await ReplyAsync($"{base.Context.User.Mention}: Can't find a character with the name **{name}**.");
            }
        }
        /// <summary>
        /// Gets all characters for a specific user from the database
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns></returns>
        [Command("getChars"), Priority(0)]
        [Summary("Gets a list of all chararcters for a specific user from the database")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetChars(SocketUser user)
        {
            List<Character> characters = DbHelper.GetCharactersByDiscordId(user.Id);
            foreach (Character character in characters)
            {
                await ReplyAsync("", false, CharacterInfoCardHelper.GetCharacterInfoCard(character, base.Context.Client.GetUser(character.OwnerID), base.Context.Guild).Build());
            }
            if (characters.Count == 0)
            {
                await ReplyAsync($"{base.Context.User.Mention}: Can't find characters for {user.Username}.");
            }
        }
        /// <summary>
        /// Sets a character as the active on which is the main.
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <returns></returns>
        [Command("setCharActive"), Priority(0)]
        [Summary("Sets the used character active")]
        public async Task SetCharActive(string name)
        {
            List<Character> characters = DbHelper.GetCharacterByName(name);
            if (characters.Count == 1)
            {
                foreach (Character character in DbHelper.GetCharactersByDiscordId(base.Context.User.Id))
                {
                    character.IsActive = false;
                    DbHelper.UpdateCharacter(character);
                }
                characters[0].IsActive = true;
                DbHelper.UpdateCharacter(characters[0]);
                await ReplyAsync($"{base.Context.User.Mention}: The character **{name}** is now your active one.");
            }
            else
            {
                await ReplyAsync($"{base.Context.User.Mention}: Can't find a character with the name **{name}**.");
            }
        }
        /// <summary>
        /// List your active character.
        /// </summary>
        /// <returns></returns>
        [Command("char"), Priority(0)]
        [Summary("Gets your active character")]
        public async Task Char()
        {
            bool foundChar = false;
            foreach (Character character in DbHelper.GetCharactersByDiscordId(base.Context.User.Id))
            {
                if (character.IsActive)
                {
                    foundChar = true;
                    await ReplyAsync("", false, CharacterInfoCardHelper.GetCharacterInfoCard(character, base.Context.Client.GetUser(character.OwnerID), base.Context.Guild).Build());
                }

            }
            if (!foundChar)
            {
                await ReplyAsync($"{base.Context.User.Mention}: You don't have an active character. Use setCharActive to set one.");
            }
        }
        /// <summary>
        /// List all of your characters.
        /// </summary>
        /// <returns></returns>
        [Command("chars"), Priority(0)]
        [Summary("Gets all your characters")]
        public async Task Chars()
        {
            List<Character> characters = DbHelper.GetCharactersByDiscordId(base.Context.User.Id);
            foreach (Character character in characters)
            {
                await ReplyAsync("", false, CharacterInfoCardHelper.GetCharacterInfoCard(character, base.Context.Client.GetUser(character.OwnerID), base.Context.Guild).Build());
            }
            if (characters.Count == 0)
            {
                await ReplyAsync($"{base.Context.User.Mention}: You don't have any characters.");
            }
        }
        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="ap">The attack points</param>
        /// <param name="aAp">The awakend attack points</param>
        /// <param name="dp">The defense points</param>
        /// <param name="level">The level</param>
        /// <param name="className">The class</param>
        /// <param name="gearUrl">A screenshot of the gear</param>
        /// <returns></returns>
        [Command("updateChar"), Priority(0)]
        [Summary("Updates a character within your character list")]
        public async Task UpdateChar(string name, int ap, int aAp, int dp, int level, string className, string gearUrl)
        {
            try
            {
                List<Character> characters = CharacterModule.DbHelper.GetCharacterByName(name);
                if (characters.Count == 1)
                {
                    if (characters[0].OwnerID != Context.User.Id)
                    {
                        throw new Exception($"You don't own the character {name}!");
                    }
                    Character character = new Character()
                    {
                        OwnerID = base.Context.User.Id,
                        Name = name,
                        Ap = ap,
                        AAp = aAp,
                        Dp = dp,
                        Url = gearUrl,
                        Level = level,
                        DbId = characters[0].DbId,
                        IsActive = characters[0].IsActive,
                        Class = (uint)ClassHelper.GetClassFromString(className)
                    };
                    CharacterModule.DbHelper.UpdateCharacter(character);
                    await ReplyAsync($"{base.Context.User.Mention} Changed your character {name}!");
                }
                else
                {
                    throw new Exception($"There is no character in the database matching **{name}**");
                }
            }
            catch (Exception exc)
            {
                await ReplyAsync($"{base.Context.User.Mention}: {exc.Message}");
            }
        }
        /// <summary>
        /// Updates the screenshot of your gear
        /// </summary>
        /// <param name="name">The name of the character</param>
        /// <param name="gearUrl">The url of the gear</param>
        /// <returns></returns>
        [Command("updateChar"), Priority(1)]
        [Summary("Updates a character screenshot within your character list")]
        public async Task UpdateCharScreenshot(string name, string gearUrl)
        {
            try
            {
                List<Character> characters = CharacterModule.DbHelper.GetCharacterByName(name);
                if (characters.Count == 1)
                {
                    if (characters[0].OwnerID != Context.User.Id)
                    {
                        throw new Exception($"You don't own the character {name}!");
                    }
                    characters[0].Url = gearUrl;
                    CharacterModule.DbHelper.UpdateCharacter(characters[0]);
                    await ReplyAsync($"{base.Context.User.Mention} Changed your character {name}!");
                }
                else
                {
                    throw new Exception($"There is no character in the database matching **{name}**");
                }
            }
            catch (Exception exc)
            {
                await ReplyAsync($"{base.Context.User.Mention}: {exc.Message}");
            }
        }
    }
}
