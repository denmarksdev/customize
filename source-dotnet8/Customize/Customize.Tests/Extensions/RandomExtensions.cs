namespace Customize.Tests.Extensions
{
    public static class RandomExtensions
    {
        public static string GenerateStringNumbers(this Random random, int numberOfRandomNumbers)
        {
            char[] chars = new char[numberOfRandomNumbers];

            for (int i = 0; i < numberOfRandomNumbers; i++)
            {
                // Gera um número aleatório entre 0 e 9 e converte para char
                chars[i] = (char)('0' + random.Next(0, 10));
            }

            return new string(chars);
        }
    }
}