using BookStore.Model.DataBase.Entities;
using BookStore.Model.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Model.DataBase
{
    public class DataModel
    {
        public IDataAdapter DataAdapter { get; private set; } = new MySQLDataAdapter();

        private readonly ConnectionSettings _connectionSettings;

        public DataModel(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public bool Connect()
        {
            bool isConnected = DataAdapter.Connect(_connectionSettings);
            DataBaseEntity.DataAdapter = DataAdapter;
            return isConnected;
        }

        public long AddEntity(DataBaseEntity entity)
        {
            return DataAdapter.InsertRow(entity.TableName, entity.GetDictionaryData());
        }

        public bool RemoveEntity(DataBaseEntity entity)
        {
            return DataAdapter.DeleteRow(entity.TableName, (long)entity.Id);
        }

        public bool UpdateEntity(DataBaseEntity entity)
        {
            return DataAdapter.UpdateRow(entity.TableName, (long)entity.Id, entity.GetDictionaryData());
        }

        public List<DataBaseEntity> GetDataBaseEntities(DataBaseEntity entity)
        {
            string query = "select * from `" + entity.TableName + "`;";
            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(query);

            return entity.GetEntityData(result);
        }

        public List<PrintedMatter> GetRecommendedEntities(UserParameters userParameters)
        {
            List<DataBaseEntity> printedMatters = GetDataBaseEntities(new PrintedMatter());
            List<PrintedMatter> recommendedEntities = new List<PrintedMatter>();

            foreach (PrintedMatter element in printedMatters)
            {
                if (userParameters.TypeOfEdition == TypeOfEdition.Electronic)
                {
                    try
                    {
                        ElectronicEdition electronicEdition = (ElectronicEdition)GetEntity(element.Id, new ElectronicEdition());

                        if (electronicEdition.Price < userParameters.MaxPrice &&
                            electronicEdition.Price > userParameters.MinPrice &&
                            electronicEdition.PrintedMatter.AgeLimit < userParameters.Age)
                        {
                            recommendedEntities.Add(element);
                        }
                    }
                    catch (Exception) { }
                }
                else if (userParameters.TypeOfEdition == TypeOfEdition.Paper)
                {
                    try
                    {
                        PaperEdition paperEdition = (PaperEdition)GetEntity(element.Id, new PaperEdition());

                        if (paperEdition.Price < userParameters.MaxPrice &&
                            paperEdition.Price > userParameters.MinPrice &&
                            paperEdition.PrintedMatter.AgeLimit < userParameters.Age)
                        {
                            recommendedEntities.Add(element);
                        }
                    }
                    catch (Exception) { }
                }
            }

            return recommendedEntities;
        }

        public DataBaseEntity GetEntity(long? idPrintedMatter, DataBaseEntity dataBaseEntity)
        {
            string query = "select * from `" + dataBaseEntity.TableName + "` where id_printed_matter=" + idPrintedMatter + ";";

            List<Dictionary<string, string>> result = DataAdapter.GetQueryResult(query);

            DataBaseEntity entity = dataBaseEntity.GetEntityData(result).First();

            return entity;
        }
    }
}