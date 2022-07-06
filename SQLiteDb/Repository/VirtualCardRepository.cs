using ProjectTestAPI_1.Models;

namespace ProjectTestAPI_1.SQLiteDb.Repository
{
    public class VirtualCardRepository
    {
         private VirtualCardsContext db;
        public VirtualCardRepository()
        {
            db = new VirtualCardsContext();
        }

        public ulong getVirtualCardNumber(Guid cardToken)
        {
            return db.virtualCards.ToList().Find(x => x.CardToken == cardToken)!.CardNumber;
        }
        public VirtualCard confirmVirtualCardDB(ulong cardNumber)
        {
            var virtualCard = new VirtualCard(cardNumber,Guid.NewGuid());
            db.virtualCards.Add(virtualCard);
            db.SaveChanges();
            return virtualCard;
        }
        public bool checkVirtualCardExist(ulong cardNumber)
        {
            var virtCard = db.virtualCards.ToList().Find(x => x.CardNumber == cardNumber);
            if(virtCard != null)
            {
                return true;
            }
            {
                return false;
            }
        }

        public void deleteVirtualCard(Guid cardToken)
        {
            db.Remove(db.virtualCards.ToList().Find(x => x.CardToken == cardToken)!);
            db.SaveChanges();
        }
    }
}