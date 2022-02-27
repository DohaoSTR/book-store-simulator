using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PrintedMatterGenre : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public Genre Genre { get; set; }

        public PrintedMatterGenre(int idPrintedMatter, int idGenre)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Genre = (Genre)GetEntity(idGenre, new Genre());
        }

        public PrintedMatterGenre(long? id,
            int idPrintedMatter, int idGenre) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Genre = (Genre)GetEntity(idGenre, new Genre());
        }

        public PrintedMatterGenre() { }

        public override string TableName => "printed_matter_genre";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_genre", Genre.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PrintedMatterGenre(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["id_genre"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
