using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MailingLabel
{
    internal class Program
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            bool tester = false;
            Console.WriteLine("Welcome to Mailing Label Console App... Follow the intructions on screen.");
            Console.Write("What's your name?: ");
            string userName = Console.ReadLine();
            int userAge = 0;
            while (tester == false)
            {
                Console.Write("What's your age?: ");
                string userTestAge = Console.ReadLine();
                bool userTest = int.TryParse(userTestAge, out userAge);
                if (userTest)
                {
                    tester = true;
                }
                else
                {
                    Console.WriteLine("This is not a pure number");
                    continue;
                }
            }
            tester = false;
            string userEmail = "";
            while (tester == false)
            {
                Console.Write("What's your email?: ");
                userEmail = Console.ReadLine();
                if (IsValidEmail(userEmail)) 
                {
                    tester = true;
                }
                else
                {
                    Console.WriteLine("This is not a legit email.");
                    continue;
                }
            }
            tester = false;
            int userNumber = 0;
            while (tester == false)
            {
                Console.Write("What's your Mobile number?: ");
                string userTestNumber = Console.ReadLine();
                bool userTest = int.TryParse(userTestNumber, out userNumber);
                if (userTest)
                {
                    tester = true;
                }
                else
                {
                    Console.WriteLine("This is not a pure number");
                    continue;
                }
            }
            tester = false;
            Console.Write("What is you address?: ");
            string userAddress = Console.ReadLine();
            Console.Write("Which country do you come from?: ");
            string userCountry = Console.ReadLine();
            int userPostNumber = 0;
            while (tester == false)
            {
                Console.Write("What's your Postnumber?: ");
                string userTestPostnumber = Console.ReadLine();
                bool userTest = int.TryParse(userTestPostnumber, out userPostNumber);
                if (userTest)
                {
                    tester = true;
                }
                else
                {
                    Console.WriteLine("This is not a pure number");
                    continue;
                }
            }
            tester = false;
            Console.Write("What city do you live in?: ");
            string userCity = Console.ReadLine();
            Console.WriteLine($"\nSo your name is {userName}\nYou emailadress is: {userEmail}\nYou are {userAge} years old\nYour Phone Number is: {userNumber}\nYou come from {userCountry}\nFrom the city of {userCity}\nAt the address of {userAddress}\nYour postnumber is {userPostNumber}\n\nThanks for using Mailing Label Console App!");
            Console.ReadKey();
        }
    }
}
