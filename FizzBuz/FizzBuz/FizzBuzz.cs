namespace FizzBuz
{

    public interface IFizzBuzz
    {
        string GetFizzBuzzString(string value);
    }

    public  class FizzBuzz : IFizzBuzz
    {
        public string GetFizzBuzzString(string value)
        {
            int result;

            if(!int.TryParse(value, out result))
                return "Not a number";

            if (result % 5 == 0 && result % 3 == 0)
                return "fizzbuzz";
            
            if (result % 5 == 0)
                return "buzz";
            
            if (result % 3 == 0)
                return "fizz";

            return value;
        }
    }
}