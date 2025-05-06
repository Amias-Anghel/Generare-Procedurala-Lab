using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GeneratorUtils
{
    /*
        gets a patern
        returns a string

        * = vowel
        @ = consonants
        # = double vowel

        other chars stay unchanged
    */
    public static string GenNameFromPattern(string pattern) {
        char[] vowels = {'a', 'e', 'i', 'o', 'u', 'y'};
        char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l'
        , 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        
        StringBuilder builder = new StringBuilder();

        foreach(char c in pattern) {
            switch(c) {
                case '@': // consonant
                    builder.Append(consonants[Random.Range(0, consonants.Length)]);
                    break;
                case '*': // vowel
                    builder.Append(vowels[Random.Range(0, vowels.Length)]);
                    break;
                case '#': // double vowel
                    char v = vowels[Random.Range(0, vowels.Length)];
                    builder.Append(v);
                    builder.Append(v);
                    break;
                default:
                    builder.Append(c);
                    break;
            }
        }
        builder[0] = char.ToUpper(builder[0]);
        return builder.ToString();
    }

    public static string GetTitle() {
        string[] titles = {
            "Great", "Storm Rider", "Ice Breaker", "Shadow Dancer", "Heartbroken", "Heart Breaker",
            "Bird Watcher", "Midnight Rider", "Dawn Breaker", "Chaos Maker", "Warrior Spirit",
            "Secret Keeper", "Quiet Thunder", "Shadow Shifter", "Harmony Seeker", "Bright Spark",
            "Gentle Giant", "Ocean's Whisper", "Sky Dancer", "Earth Shaker", "Iron Will", "Lost Explorer",
            "Ocean's Fury", "Silent Guardian", "Sun Chaser", "Soul Keeper", "Daydreamer", "Hope Dealer",
            "Cloud Surfer", "Game Changer", "Rule Breaker", "Fearless Wanderer"
        };

        return titles[Random.Range(0, titles.Length)];
    }

    public static string GenNameWithTitle(string pattern) {
        if (Random.value > 0.5f) {
            return GetTitle() + " " + GenNameFromPattern(pattern);
        } else {
            return GenNameFromPattern(pattern) + " the " + GetTitle();
        }
    }
}
