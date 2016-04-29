/****************************************************************************************/
/*
/* FILE NAME: ErrorText.cs
/*
/* DESCRIPTION: Displays error text for specified duration when user performs certain invalid actions
/*
/* REFERENCE: 
/*
/*      DATE                 BY                 DESCRIPTION
/* ========               =======               =============
/* 4/25/2016            Khaleel Harris          Created the class
/* 
/*
/*
/*
/****************************************************************************************/

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
