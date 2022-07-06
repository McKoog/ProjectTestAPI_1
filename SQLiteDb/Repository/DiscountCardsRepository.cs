namespace ProjectTestAPI_1.SQLiteDb.Repository
{
    public class DiscountCardsRepository
    {
        private DiscountCardsContext db;
        public DiscountCardsRepository()
        {
            db = new DiscountCardsContext();
        }

        public bool checkCardExist(ulong cardNumber)
        {
           var info = db.discountCards.ToList();
           var disCard = db.discountCards.ToList().Find(x => x.CardNumber == cardNumber);
           if(disCard != null)
           {
            return true;
           }
           else
           {
            return false;
           }
        }

        public double getBalance(ulong cardNumber)
        {
            return db.discountCards.ToList().Find(x => x.CardNumber == cardNumber)!.Balance;
        }
    }
}