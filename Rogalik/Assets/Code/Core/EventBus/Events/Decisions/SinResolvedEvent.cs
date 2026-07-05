using UnityEngine;

namespace Core
{
    public readonly struct SinResolvedEvent
    {
        public readonly SinsConfig Sin;
        public readonly SinResolutionType Result;
        public SinResolvedEvent(SinsConfig sin, SinResolutionType resolution)
        {
            Sin = sin;
            Result = resolution;
        }
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