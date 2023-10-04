namespace PasswordGenerator.CustomClasses;

using System;

public class CustomRandom
{
    public int Seed { get; set; }
    
    public CustomRandom(int seed)
    {
        Seed = seed;
    }
    
    public int Next()
    {
        Seed = (Seed * 9301 + 49297) % 233280;
        return Seed;
    }
    
    public int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentException("minValue cannot be greater than maxValue");
        }
        
        int range = maxValue - minValue;
        return minValue + Next() % range;
    }
    public int Next(int maxValue)
    {
        return Next(0, maxValue);
    }
}
