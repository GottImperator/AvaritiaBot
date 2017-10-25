using AvaritiaBot.Enums;
using System;

namespace AvaritiaBot.Helper
{
    public static class ClassHelper
    {
        public static Classes GetClassFromString(string className)
        {
            
            switch (className.ToLower())
            {
                case "valk":
                case "valkyrie":
                    return Classes.Valkyrie;
                case "warrior":
                    return Classes.Warrior;
                case "maehwa":
                case "baehwa":
                    return Classes.Maehwa;
                case "ninja":
                    return Classes.Ninja;
                case "kuno":
                case "kunoichi":
                    return Classes.Kunoichi;
                case "dk":
                case "darkknight":
                    return Classes.DarkKnight;
                case "berserker":
                case "zerker":
                    return Classes.Berseker;
                case "musa":
                case "blader":
                    return Classes.Musa;
                case "tamer":
                    return Classes.Tamer;
                case "striker":
                    return Classes.Striker;
                case "ranger":
                case "archer":
                    return Classes.Ranger;
                case "sorc":
                case "sorceress":
                    return Classes.Sorceress;
                case "witch":
                    return Classes.Witch;
                case "wizard":
                case "wizzard":
                    return Classes.Wizard;
            }
            throw new Exception($"Can't find class with the name {className}");
        }
        public static string GetClassImageUrl(Classes playeClass)
        {
            switch (playeClass)
            {
                case Classes.Berseker:
                    return @"https://i.imgur.com/fBWtFx1.png";
                case Classes.DarkKnight:
                    return @"https://i.imgur.com/pVvYKxa.png";
                case Classes.Kunoichi:
                    return @"https://i.imgur.com/8JPzRDr.png";
                case Classes.Maehwa:
                    return @"https://i.imgur.com/XIxFFe2.png";
                case Classes.Musa:
                    return @"https://i.imgur.com/v7q1POt.png";
                case Classes.Ninja:
                    return @"https://i.imgur.com/J1TeRtZ.png";
                case Classes.Ranger:
                    return @"https://i.imgur.com/1ugIcTz.png";
                case Classes.Sorceress:
                    return @"https://i.imgur.com/fqC3el3.png";
                case Classes.Tamer:
                    return @"https://i.imgur.com/dekpS73.png";
                case Classes.Valkyrie:
                    return @"https://i.imgur.com/hscVFJa.png";
                case Classes.Warrior:
                    return @"https://i.imgur.com/WEB2jzf.png";
                case Classes.Witch:
                    return @"https://i.imgur.com/IqPt0v5.png";
                case Classes.Wizard:
                    return @"https://i.imgur.com/8R4htz7.png";
                case Classes.Striker:
                case Classes.Mystic:
                default:
                    return @"https://cdn.discordapp.com/attachments/335072275273613314/364475352703303681/b7a.png";
            }
        }
    }
}
