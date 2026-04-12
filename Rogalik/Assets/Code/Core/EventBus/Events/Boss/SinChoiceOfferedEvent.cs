namespace Core
{
    public struct SinChoiceOfferedEvent
    {
        public string SinType;
        public SinOfferContext Context;
    }

    public enum SinOfferContext
    {
        BossOffer,
        DeathOffer
    }   
}
