using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PrintedMatterPublisher : DataBaseEntity
    {
        public PrintedMatter PrintedMatter { get; set; }

        public Publisher Publisher { get; set; }

        public PrintedMatterPublisher(int idPrintedMatter, int idPublisher)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Publisher = (Publisher)GetEntity(idPublisher, new Publisher());
        }

        public PrintedMatterPublisher(long? id, int idPrintedMatter, int idPublisher) : base(id)
        {
            PrintedMatter = (PrintedMatter)GetEntity(idPrintedMatter, new PrintedMatter());
            Publisher = (Publisher)GetEntity(idPublisher, new Publisher());
        }

        public PrintedMatterPublisher() { }

        public override string TableName => "printed_matter_publisher";

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "id_printed_matter", PrintedMatter.Id },
                { "id_publisher", Publisher.Id }
            };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PrintedMatterPublisher(Convert.ToInt32(g["id"]),
                    Convert.ToInt32(g["id_printed_matter"]), Convert.ToInt32(g["id_publisher"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
