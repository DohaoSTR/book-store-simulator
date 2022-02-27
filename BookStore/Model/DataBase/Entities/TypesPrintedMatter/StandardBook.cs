using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class StandardBook : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public ConformityCertificate ConformityCertificate { get; set; }

        public StandardBook(int printedMatter, int conformityCertificate)
        {
            PrintedMatter = (PrintedMatter)GetEntity(printedMatter, new PrintedMatter());
            ConformityCertificate = (ConformityCertificate)GetEntity(conformityCertificate, new ConformityCertificate());
        }

        public StandardBook(long? id,
            int printedMatter, int conformityCertificate) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(printedMatter, new PrintedMatter());
            ConformityCertificate = (ConformityCertificate)GetEntity(conformityCertificate, new ConformityCertificate());
        }

        public StandardBook() { }

        public override string TableName => "standard_book";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_conformity_certificate", ConformityCertificate.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new StandardBook(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]),
                    Convert.ToInt32(g["id_conformity_certificate"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
