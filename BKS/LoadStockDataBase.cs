namespace BKS
{
    public abstract class LoadStockDataBase
    {
        private Guid _UserId;

        public abstract void LoadStockData(Guid UserId);
    }
}