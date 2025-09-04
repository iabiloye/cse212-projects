public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        /// Plan:
        /// 1. Will create a new array of doubles with the specified length.
        /// 2. will have to loop from 0 to length - 1.
        /// 3. For each index i, calculate the multiple: number * (i + 1).
        /// 4. Store the calculated multiple in the array at index i.
        /// 5. Return the array containing all multiples.

        /// Implementing the plan:
        double[] multiples = new double[length];
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        /// Handle full rotation (amount == data.Count)
        if (amount == data.Count || amount == 0)
            return;

        int n = data.Count;
        /// Get the last 'amount' elements
        var right = data.GetRange(n - amount, amount);
        /// Get the remaining elements
        var left = data.GetRange(0, n - amount);

        /// Clear and rebuild the list
        data.Clear();
        data.AddRange(right);
        data.AddRange(left);
    }
}
