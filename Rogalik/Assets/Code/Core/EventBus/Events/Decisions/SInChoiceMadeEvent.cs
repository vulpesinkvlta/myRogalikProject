namespace Core
{
    public struct SInChoiceMadeEvent
    {
        public SinsConfig Sin;
        public bool Accepted;
        public SinOfferContext Context;

        public SInChoiceMadeEvent(SinsConfig sin, bool accepted, SinOfferContext context)
        {
            Sin = sin;
            Accepted = accepted;
            Context = context;
        }
    }
}
