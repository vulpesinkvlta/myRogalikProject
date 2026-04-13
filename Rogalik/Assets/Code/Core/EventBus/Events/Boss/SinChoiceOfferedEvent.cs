namespace Core
{
    public struct SinChoiceOfferedEvent
    {
        public SinsConfig Sin;
        public SinOfferContext Context;
    }

    public enum SinOfferContext
    {
        BossOffer,
        DeathOffer
    }   
}
