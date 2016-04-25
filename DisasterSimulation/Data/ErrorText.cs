class ErrorText
{
    public byte alphaValue;
    public string stringToDisplay;
    public float displayDuration, displayDurationRemaining;
    public bool showText;
    public static ErrorText CurrentErrorText = new ErrorText();
    public void ShowErrorText(string textToDisplay, float displayDuration)
    {
        CurrentErrorText.showText = true;
        CurrentErrorText.alphaValue = 255;
        CurrentErrorText.stringToDisplay = textToDisplay;
        CurrentErrorText.displayDuration = displayDuration;
        CurrentErrorText.displayDurationRemaining = displayDuration;
    }
}
