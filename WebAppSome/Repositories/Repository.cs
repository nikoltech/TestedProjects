namespace WebAppSome.Repositories
{
    using WebAppSome.Interfaces;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }
    }
}
