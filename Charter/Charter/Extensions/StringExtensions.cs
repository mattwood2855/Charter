using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Charter.Extensions
{
    public static class StringExtensions
    {
        // Taken from https://stackoverflow.com/questions/188510/how-to-format-a-string-as-a-telephone-number-in-c-sharp
        public static string FormatPhoneNumber(this string value)
        {
            value = new Regex(@"\D").Replace(value, string.Empty);
            value = value.TrimStart('1');

            if (value.Length == 0)
                value = string.Empty;
            else if (value.Length < 3)
                value = string.Format("({0})", value.Substring(0, value.Length));
            else if (value.Length < 7)
                value = string.Format("({0}) {1}", value.Substring(0, 3), value.Substring(3, value.Length - 3));
            else if (value.Length < 11)
                value = string.Format("({0}) {1}-{2}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6));
            else if (value.Length > 10)
            {
                value = value.Remove(value.Length - 1, 1);
                value = string.Format("({0}) {1}-{2}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6));
            }
            return value;
        }

        public static bool HasConsecutiveRepeatingSequences(this string completeString)
        {
            // Determine max sequence length
            var maxSequenceLength = completeString.Length / 2;

            // Try all sequence lengths possible
            for(int sequenceLength = 1; sequenceLength <= maxSequenceLength; sequenceLength++)
            {
                // Move through the string
                for(int index = 0; index < completeString.Length; index++)
                {
                    // If the sequence is larger than the string at this index
                    if (index + sequenceLength > completeString.Length || index + sequenceLength + sequenceLength > completeString.Length)
                        // Skip
                        continue;

                    // Get the first block
                    var firstBlock = completeString.Substring(index, sequenceLength);

                    // Get the next block
                    var secondBlock = completeString.Substring(index + sequenceLength, sequenceLength);

                    // If they match
                    if (firstBlock == secondBlock)
                        return true;
                }
            }

            return false;
        }
    }
}
