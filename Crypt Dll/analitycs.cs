using System.Collections.Generic;

namespace crypt_dll_aplication
{
    public class analiticValue
    {
        public char letter;
        public int frequency;
        public analiticValue(char letterValue)
        {
            letter = letterValue;
        }
    }
    public static class analiticsValues
    {
        public enum sortingOptions
        {
            byFrequency,
            byLetter
        }
        public static List<analiticValue> values = new List<analiticValue>();

        // add the value
        public static void Add(analiticValue val)
        {
            values.Add(val);
        }

        // sort the letters in the list
        // TODO : change it in order to sort by frequency or letter position
        public static void Sort()
        {
            List<int> valueList = new List<int>();
            List<analiticValue> output = new List<analiticValue>();
            foreach (analiticValue value in values)
            {
                valueList.Add(value.frequency);
            }
            valueList.Sort();
            int[] valueInt = valueList.ToArray();
            valueList = null;
            for (int i = 0; i < valueInt.Length; i++)
            {
                analiticValue letterFoudnd = getLetter(valueInt[i]);
                if (letterFoudnd != null)
                {
                    output.Add(letterFoudnd);
                }
            }
            values = output;
        }
        public static analiticValue getLetter(int frequency)
        {
            foreach (analiticValue letter in values)
            {
                if (letter.frequency == frequency)
                {
                    return letter;
                }
            }
            return null;
        }
        public static int valueExist(char selectedLetter)
        {

            foreach (analiticValue letter in values)
            {
                if (letter.letter == selectedLetter)
                {
                    return values.IndexOf(letter);
                }
            }

            return -1;
        }
    }
    public static class analitycs
    {
        //analize the frequency of the letters
        public static analiticValue[] analizeFrequency(string input)
        {
            int totalLetters = 0;
            foreach (char letter in input)
            {
                totalLetters++;
                int pos = analiticsValues.valueExist(letter);
                if (pos != -1)
                {
                    analiticsValues.values[pos].frequency += 1;
                }
                else
                {
                    analiticValue newLetter = new analiticValue(letter);
                    newLetter.frequency = 1;
                    analiticsValues.Add(newLetter);
                }
            }
            analiticsValues.Sort();
            return analiticsValues.values.ToArray();
        }


    }
}
