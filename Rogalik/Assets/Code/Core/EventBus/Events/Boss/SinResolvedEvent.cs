namespace Core
{
    public struct SinResolvedEvent
    {
        public SinsConfig Sin;
        public SinResolutionType Result;
    }
}
public enum SinResolutionType
{
    Accepted, 
    Purified    
}

/*
 * Сценарий	IsPurified
 * Принял грех	false
 * Отказался + победил	true
 * Отказался + проиграл	false 
 */