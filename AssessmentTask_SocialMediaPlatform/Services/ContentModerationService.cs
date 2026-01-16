namespace Social_Media.Services;

public class ContentModerationService
{
    private static readonly string[] BannedWords =
    {
        "monolith","spaghetticode","goto","hack","architrixs",
        "quickanddirty","cowboy","yo","globalvariable",
        "recursivehell","backdoor","hotfix","leakyabstraction",
        "mockup","singleton","silverbullet","technicaldebt"
    };

    public void Validate(string content)
    {
        var lower = content.ToLower();

        foreach (var word in BannedWords)
        {
            if (lower.Contains(word))
                throw new InvalidOperationException(
                    $"Content contains banned word: {word}"
                );
        }
    }
}
