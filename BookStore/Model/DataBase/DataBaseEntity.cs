using System.Collections.Generic;
using System.Linq;

namespace BookStore.Model.DataBase
{
    public abstract class DataBaseEntity
    {
        public static IDataAdapter DataAdapter { get; set; }

        public DataBaseEntity(long? id)
        {
            Id = id;
        }

        public DataBaseEntity()
        {
            Id = null;
        }

        public long? Id { get; set; }

        public abstract string TableName { get; }

        public abstract Dictionary<string, object> GetDictionaryData();

        public abstract List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result);

        public static DataBaseEntity GetEntity(long? id, DataBaseEntity dataBaseEntity)
        {
            string query = "select * from `" + dataBaseEntity.TableName + "` where id=" + id + ";";

            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(query);

            DataBaseEntity entity = dataBaseEntity.GetEntityData(result).First();

            return entity;
        }

    }
}
