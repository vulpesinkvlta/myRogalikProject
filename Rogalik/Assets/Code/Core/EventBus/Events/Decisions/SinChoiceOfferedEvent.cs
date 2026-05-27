namespace Core
{
    public struct SinChoiceOfferedEvent
    {
        public SinsConfig Sin;
        public SinOfferContext Context;

        public SinChoiceOfferedEvent(SinsConfig sin, SinOfferContext context)
        {
            Sin = sin;
            Context = context;
        }
    }

    public enum SinOfferContext
    {
        BossOffer,
        DeathOffer
    }   
}
