using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PrintedMatterISBN : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public ISBN ISBN { get; set; }

        public PrintedMatterISBN(int idPrintedMatter, int idIsbn)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            ISBN = (ISBN)GetEntity(idIsbn, new ISBN());
        }

        public PrintedMatterISBN(long? id,
            int idPrintedMatter, int idIsbn) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            ISBN = (ISBN)GetEntity(idIsbn, new ISBN());
        }

        public PrintedMatterISBN() { }

        public override string TableName => "printed_matter_isbn";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_isbn", ISBN.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PrintedMatterISBN(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["id_isbn"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
