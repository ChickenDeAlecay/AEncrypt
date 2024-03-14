namespace AEncrypt;

internal class Program
{
    private static void Main(string[] args)
    {
        var message = "Hello World!";

        Console.WriteLine("Original: " + message);

        var messageAsInt = EncodeMessage.ConvertToInt(message);

        Console.WriteLine("\nMessage converted to ints: " + messageAsInt);

        var encodedMessage = EncodeMessage.Encode(messageAsInt);

        Console.WriteLine("\nMessage encoded to morsecode translation set: " + encodedMessage);

        var decodedMessage = DecodeMessage.Decode(encodedMessage);

        Console.WriteLine("\nMessage decoded: " + decodedMessage);

        var finalMessage = DecodeMessage.BackToString(decodedMessage);

        Console.WriteLine("\nMessage converted back to original: " + finalMessage);
    }
}

internal static class EncodeMessage
{
    private static readonly string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string ConvertToInt(string message)
    {
        message.ToCharArray();

        var intList = string.Empty;
        var intWord = string.Empty;
        var intCharLength = string.Empty;

        foreach (var character in message)
            if (character.ToString().Equals(" "))
            {
                intList += intWord + " " + intCharLength + " ";
                intWord = string.Empty;
                intCharLength = string.Empty;
            }
            else
            {
                var intChar = Convert.ToInt32(character).ToString();
                intWord += intChar;
                intCharLength += intChar.Length.ToString();
            }

        intList += intWord + " " + intCharLength;

        return intList.Trim();
    }

    public static string Encode(string message)
    {
        var encodedMessage = string.Empty;
        var origionalMessage = message.Split(" ");

        var intMessage = new List<long>();
        foreach (var s in origionalMessage) intMessage.Add(long.Parse(s));

        foreach (var i in intMessage)
        {
            var messageInt = i;
            while (messageInt != 0)
            {
                encodedMessage += EncodeMessage.Chars[(int)(messageInt % 36)];
                messageInt /= 36;
            }

            encodedMessage += " ";
        }

        return encodedMessage.Trim();
    }
}

internal static class DecodeMessage
{
    private static readonly string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string Decode(string message)
    {
        long tempMessage = 0;
        var decodedMessage = string.Empty;

        var origionalMessage = message.Split(" ");

        var listMessage = new List<string>();
        foreach (var s in origionalMessage) listMessage.Add(s);

        foreach (var word in listMessage)
        {
            for (var i = 0; i < word.Length; i++)
                tempMessage += DecodeMessage.Chars.IndexOf(word[i]) * (long)Math.Pow(36, i);

            decodedMessage += tempMessage + " ";
            tempMessage = 0;
        }


        return decodedMessage.Trim();
    }

    public static string BackToString(string message)
    {
        var unEncodedMessage = string.Empty;
        var origionalMessage = message.Split(" ");

        var intMessage = new List<long>();
        foreach (var s in origionalMessage) intMessage.Add(long.Parse(s));

        for (var i = 0; i < intMessage.Count; i += 2)
        {
            var word = intMessage[i].ToString();
            var code = intMessage[i + 1].ToString().ToCharArray();
            foreach (var length in code)
            {
                int temp;
                var numLength = int.Parse(length.ToString());

                temp = int.Parse(word.Substring(0, numLength));
                unEncodedMessage += (char)temp;
                word = word.Remove(0, numLength);
            }

            unEncodedMessage += " ";
        }


        return unEncodedMessage.Trim();
    }
}