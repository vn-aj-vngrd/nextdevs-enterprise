namespace Backend.FunctionalTests.Common;

public static class RandomDataExtensionMethods
{
    public static string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static long RandomNumber(int length)
    {
        var random = new Random();
        return random.Next(length);
    }
}