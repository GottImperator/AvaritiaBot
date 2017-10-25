using AvaritiaBot.Models;
using DBreeze;
using DBreeze.Transactions;
using DBreeze.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AvaritiaBot.Helper
{
    public class CharacterDatabaseHelper
    {
        DBreezeEngine engine = null;
        public CharacterDatabaseHelper()
        {
            if (engine == null) {
                engine = new DBreezeEngine(Path.Combine(AppContext.BaseDirectory, "db","characters"));
            }
            DBreeze.Utils.CustomSerializator.ByteArraySerializator = (object o) => { return NetJSON.NetJSON.Serialize(o).To_UTF8Bytes(); };


            DBreeze.Utils.CustomSerializator.ByteArrayDeSerializator = (byte[] bt, Type t) => { return NetJSON.NetJSON.Deserialize(t, bt.UTF8_GetString()); };
        }

        public void AddCharacter(Character character)
        {
             if(GetCharacterByName(character.Name).Count == 0)
            {
                using (Transaction transaction = engine.GetTransaction())
                {
                    transaction.SynchronizeTables("characters");
                    if (character.DbId == 0)
                    {
                        character.DbId = transaction.ObjectGetNewIdentity<long>("characters");
                    }
                    character.LastUpdate = DateTime.Now;
                    transaction.ObjectInsert("characters", new DBreeze.Objects.DBreezeObject<Character>
                    {
                        NewEntity = true,
                        Entity = character,
                        Indexes = new List<DBreeze.Objects.DBreezeIndex>
                        {
                            new DBreeze.Objects.DBreezeIndex(1,character.DbId) { PrimaryIndex = true },
                            new DBreeze.Objects.DBreezeIndex(2,character.OwnerID) { PrimaryIndex = false }
                        }
                    }, false);
                    transaction.TextInsert("TS_Characters", character.DbId.ToBytes(), character.Name);
                    transaction.Commit();
                }
            }
            else
            {
                throw new Exception($"The character {character.Name} does already exists.");
            }
        }
        public List<Character> GetCharacterByName(string Name)
        {
            List<Character> characters = new List<Character>();
            using (Transaction transaction = engine.GetTransaction())
            {
                foreach (var doc in transaction.TextSearch("TS_Characters").BlockAnd(Name).GetDocumentIDs())
                {
                    var obj = transaction.Select<byte[], byte[]>("characters", 1.ToIndex(doc)).ObjectGet<Character>();
                    if (obj != null && obj.Entity.Name.Equals(Name))
                    {
                        characters.Add(obj.Entity);
                    }
                }

            }
            return characters;
        }
        public void DeleteChar(Character character)
        {
            using (Transaction transaction = engine.GetTransaction())
            {
                transaction.ObjectRemove("characters", 1.ToIndex(character.DbId));
                transaction.Commit();
            }
        }
        public void UpdateCharacter(Character character)
        {
            if (GetCharacterByName(character.Name).Count == 1)
            {
                using (Transaction transaction = engine.GetTransaction())
                {
                    transaction.SynchronizeTables("characters");
                    if (character.DbId == 0)
                    {
                        character.DbId = transaction.ObjectGetNewIdentity<long>("characters");
                    }
                    character.LastUpdate = DateTime.Now;
                    transaction.ObjectInsert("characters", new DBreeze.Objects.DBreezeObject<Character>
                    {
                        NewEntity = false,
                        Entity = character,
                        Indexes = new List<DBreeze.Objects.DBreezeIndex>
                        {
                            new DBreeze.Objects.DBreezeIndex(1,character.DbId) { PrimaryIndex = true },
                            new DBreeze.Objects.DBreezeIndex(2,character.OwnerID) { PrimaryIndex = false }
                        }
                    }, false);
                    transaction.TextInsert("TS_Characters", character.DbId.ToBytes(), character.Name);
                    transaction.Commit();
                }
            } else
            {
                throw new Exception($"There is no character in the database matching **{character.Name}**");
            }
        }

        public List<Character> GetCharactersByDiscordId(ulong discordId)
        {
            using (Transaction transaction = engine.GetTransaction())
            {
                List<Character> characters = new List<Character>();
                foreach (var row in transaction.SelectForward<byte[], byte[]>("characters"))
                {
                   var character = row.ObjectGet<Character>();
                   if (character != null && character.Entity.OwnerID == discordId)
                    {
                        if(characters.Find(x => x.DbId == character.Entity.DbId) == null)
                        {
                            characters.Add(character.Entity);
                        }
                    }
                }
                return characters;
            }
        }
    }
}
