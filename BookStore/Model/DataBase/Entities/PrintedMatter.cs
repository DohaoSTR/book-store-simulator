using System;
using System.Collections.Generic;

namespace BookStore.Model.DataBase.Entities
{
    public class PrintedMatter : DataBaseEntity
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => _title = value.Length > 100 && value.Length <= 0
                ? throw new ArgumentOutOfRangeException("Заголовок должен быть " +
                    "длинной больше 0 символов и не превышать длину в 100 символов!")
                : value;
        }

        private string _annotation;
        public string Annotation
        {
            get => _annotation;
            set => _annotation = value ?? "";
        }

        private int _numberOfPages;
        public int NumberOfPages
        {
            get => _numberOfPages;
            set => _numberOfPages = value < 0
                ? throw new ArgumentOutOfRangeException("Количество страниц должно быть " +
                    "больше нуля!")
                : value;
        }

        private int _ageLimit;
        public int AgeLimit
        {
            get => _ageLimit;
            set => _ageLimit = value < 0
                ? throw new ArgumentOutOfRangeException("Возрастное ограничение должно быть " +
                    "больше нуля!")
                : value;
        }

        public DateTime ImprintDate { get; set; }

        public override string TableName => "printed_matter";

        public PrintedMatter(string title, string annotation, int numberOfPages, int ageLimit, DateTime imprintDate)
        {
            Title = title;
            NumberOfPages = numberOfPages;
            AgeLimit = ageLimit;
            ImprintDate = imprintDate;
            Annotation = annotation;
        }

        public PrintedMatter(long? id, string title, string annotation, int numberOfPages,
            int ageLimit, DateTime imprintDate)
            : base(id)
        {
            Title = title;
            NumberOfPages = numberOfPages;
            AgeLimit = ageLimit;
            ImprintDate = imprintDate;
            Annotation = annotation;
        }

        public PrintedMatter() { }

        public override Dictionary<string, object> GetDictionaryData()
        {
            return new Dictionary<string, object>() {
                { "title", Title },
                { "annotation", Annotation },
                { "number_of_pages", NumberOfPages },
                { "age_limit", AgeLimit},
                { "imprint_date", ImprintDate.ToString("yyyy-MM-dd") } };
        }

        public override List<DataBaseEntity> GetEntityData(List<Dictionary<string, string>> result)
        {
            List<DataBaseEntity> entities = new List<DataBaseEntity>();

            foreach (Dictionary<string, string> g in result)
            {
                DataBaseEntity entity = new PrintedMatter(Convert.ToInt32(g["id"]), g["title"], g["annotation"], Convert.ToInt32(g["number_of_pages"]),
                    Convert.ToInt32(g["age_limit"]), Convert.ToDateTime(g["imprint_date"]));

                entities.Add(entity);
            }

            return entities;
        }
    }
}
