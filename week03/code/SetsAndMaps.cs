using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
public static string[] FindPairs(string[] words)
{
    var set = new HashSet<string>(words);
    var result = new List<string>();

    foreach (var word in words)
    {
        /// This should skip special case like "aa"
        if (word[0] == word[1]) continue;

        var reversed = new string(new[] { word[1], word[0] });

        if (set.Contains(reversed))
        {
            result.Add($"{word} & {reversed}");
            set.Remove(word);     /// to prevent duplicates
            set.Remove(reversed); // to avoid "am & ma" or if & fi twice
        }
    }

    return result.ToArray();
}


    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
             if (fields.Length < 4) continue; /// malformed lines skip

        var degree = fields[3].Trim();

        if (!string.IsNullOrEmpty(degree))
        {
            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
{
    /// Helper to clean up the words: remove spaces + lowercase
    string Clean(string s) => new string(
        s.Where(char.IsLetterOrDigit) 
         /// keep only letters/numbers
         .Select(char.ToLower)
         .ToArray()
    );

    var w1 = Clean(word1);
    var w2 = Clean(word2);

    if (w1.Length != w2.Length) return false;

    var dict = new Dictionary<char, int>();

    /// Count characters in word1
    foreach (var c in w1)
    {
        if (dict.ContainsKey(c))
            dict[c]++;
        else
            dict[c] = 1;
    }
    /// Subtract counts using word2
    foreach (var c in w2)
    {
        if (!dict.ContainsKey(c)) return false;
            /// letter not found
            dict[c]--;

        if (dict[c] == 0)
            dict.Remove(c);
    }
    /// If dictionary is empty then all counts matched
    return dict.Count == 0;
}


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.if (featureCollection?.Features == null) 
         if (featureCollection?.Features == null)
            return Array.Empty<string>();

        var result = featureCollection.Features
        .Where(f => f.Properties != null)
        .Select(f =>
        {
            var place = f.Properties.Place ?? "Unknown location";
            var mag = f.Properties.Mag.HasValue ? f.Properties.Mag.Value.ToString("0.##") : "N/A";
            return $"{place} - Mag {mag}";
        })
        .ToArray();
        // 3. Return an array of these string descriptions.
        return result;
    }
}