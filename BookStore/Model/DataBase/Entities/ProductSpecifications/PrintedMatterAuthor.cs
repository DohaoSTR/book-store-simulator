using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PrintedMatterAuthor : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public Author Author { get; set; }

        public PrintedMatterAuthor(int idPrintedMatter, int idAuthor)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Author = (Author)GetEntity(idAuthor, new Author());
        }

        public PrintedMatterAuthor(long? id,
            int idPrintedMatter, int idAuthor) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Author = (Author)GetEntity(idAuthor, new Author());
        }

        public PrintedMatterAuthor() { }

        public override string TableName => "printed_matter_author";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_author", Author.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PrintedMatterAuthor(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["id_author"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
