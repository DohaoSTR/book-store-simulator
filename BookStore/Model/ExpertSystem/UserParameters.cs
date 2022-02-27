namespace BookStore.Model.ExpertSystem
{
    public enum TypeOfBook
    {
        Journal,
        Brochure,
        SchoolBook,
        StandardBook
    }

    public enum TypeOfEdition
    {
        Paper,
        Electronic
    }

    public class UserParameters
    {
        public TypeOfEdition TypeOfEdition { get; private set; }

        public int Age { get; private set; }

        public int MinPrice { get; private set; }

        public int MaxPrice { get; private set; }

        public UserParameters(TypeOfEdition typeOfEdition, int age, int minPrice, int maxPrice)
        {
            TypeOfEdition = typeOfEdition;
            Age = age;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
