namespace Random.Script;

using System;
using System.Linq;
using Ships.Model;
using Random;
class RandomScript{
    public static string GetRandomTableName(){
        int length = 10; // specify desired length of random string
        // define characters to be used in the random string
        const string chars = "abcdefghijklmnopqrstuvwxyz";

        // create instance of Random class
        var random = new Random();

        // generate random string using LINQ Select method
        string randomString = new string(Enumerable.Repeat(chars, length)
                                                .Select(s => s[random.Next(s.Length)])
                                                .ToArray());
        return randomString;
    }
}