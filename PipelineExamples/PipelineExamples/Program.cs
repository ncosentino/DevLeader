Console.WriteLine(
    "Enter some text to analyze:");
var input = Console.ReadLine();

TextCleaner cleaner = text =>
{
    // Remove punctuation and
    // convert to lower case
    var cleanedText = new string(text
        .Where(c => !char.IsPunctuation(c))
        .ToArray());
    return cleanedText.ToLower();
};
WordCounter counter = cleanedText =>
{
    var wordFrequency = new Dictionary<string, int>();
    var words = cleanedText.Split(' ');

    foreach (var word in words)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            continue;
        }

        if (wordFrequency.TryGetValue(
            word,
            out int value))
        {
            wordFrequency[word] = ++value;
        }
        else
        {
            wordFrequency[word] = 1;
        }
    }

    return wordFrequency;
};
TextSummarizer summarizer = wordFrequency =>
{
    // Summarize by picking top
    // 3 frequent words
    var topWords = wordFrequency
        .OrderByDescending(kvp => kvp.Value)
        .Take(3)
        .Select(kvp => kvp.Key);
    return 
        $"Top words: " +
        $"{string.Join(", ", topWords)}";
};

await Task
    .Run(() =>
    {
        var cleanedText = cleaner(input);
        return cleanedText;
    })
    .ContinueWith(Task =>
    {
        var wordFrequency = counter(Task.Result);
        return wordFrequency;
    })
    .ContinueWith(Task =>
    {
        var summary = summarizer(Task.Result);
        return summary;
    })
    .ContinueWith(Task =>
    {
        Console.WriteLine(Task.Result);
    });


public delegate string TextCleaner(
    string input);
public delegate Dictionary<string, int> WordCounter(
    string input);
public delegate string TextSummarizer(
    Dictionary<string, int> wordFrequency);