
using System.Text;



int userGuesses = 0;
string[] secretWords = SecretWords();
bool gameFlag = true;
int userChoice;
int userChoice2;
string userGuess;
int maxGuesses = 10;
bool innerFlag = true;

StringBuilder userIncorrectLetters = new StringBuilder();



Console.WriteLine("Welcome in Hangman game!.\n");
Console.WriteLine(" // Game description //");
Console.WriteLine("Guess a secret word by single letters or try to guess by a word.");
Console.WriteLine("If you enter incorrect letter 10 times you lose.");
Console.WriteLine("===========================================================\n");
do
{
    Console.WriteLine("MENU:");
    Console.WriteLine("1 -  Start a game");
    Console.WriteLine("0 -  Exit ");

    try
    {

        userChoice = int.Parse(Console.ReadLine());


        switch (userChoice)
        {
            case 0:
                Console.WriteLine("Goodbye.");
                gameFlag = false;
                break;
            case 1:
                StartGame();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter 1 or 0 to either start or exit game\n\n");
                Console.WriteLine("===========================================================\n");
                Console.ResetColor();
                break;
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Only numbers allowed. \nPlease enter 1 or 0 to either start or exit game\n");
        Console.WriteLine("===========================================================\n");
        Console.ResetColor();
    };




} while (gameFlag);

// Functions

void StartGame()
{
    Random random = new Random();
    int index = random.Next(secretWords.Length);
    string secret = secretWords[index];

    


    char[] SecretToArray = secret.ToCharArray();
    // char array for users correct letters
    char[] HiddenSecret;
    string underscore = "_";

    //Converting string Array secret to Hidden char Array
    HiddenSecret = Array.ConvertAll(SecretToArray, s => char.Parse(underscore));


    Console.WriteLine("Let's begin!");
    Console.WriteLine("What do you think a secret word is ? ");

    do
    {
        int chancesLeft = userChances(userGuesses, maxGuesses);

        Console.WriteLine("You have : "+ chancesLeft +" chances left." );
        Console.WriteLine("Your incorrect letters are: " + userIncorrectLetters);
        Console.WriteLine("Secret word is: ");
        Console.WriteLine(String.Join(" ", HiddenSecret));
        Console.WriteLine("\n");

        Console.WriteLine("1 - Guess a single letter");
        Console.WriteLine("2 - Guess a whole word");

        try
        {
            
            userChoice2 = int.Parse(Console.ReadLine());


            switch (userChoice2)
            {
                case 1:
                    Console.WriteLine("Enter your letter: ");
                    userGuess = Console.ReadLine();
                    
                    TryGuess(userGuess,SecretToArray,secret,HiddenSecret);
                    break;
                case 2:
                    Console.WriteLine("Enter your word: ");
                    userGuess = Console.ReadLine();
                    TryGuess(userGuess,SecretToArray,secret,HiddenSecret);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter 1 or 2\n");
                    Console.WriteLine("===========================================================\n");
                    Console.ResetColor();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Only numbers allowed. \nPlease enter 1 or 2\n");
            Console.WriteLine("===========================================================\n");
            Console.ResetColor();
        };


    } while (userGuesses < 10 && innerFlag == true);
    Console.WriteLine("Secret word was : " + secret);
    Console.WriteLine("Game Over. Please reload the game.\n");
}

void TryGuess(string userWord, char[] SecretToArray,string secret,char[] HiddenSecret)
{

    char[] userLetters = userIncorrectLetters.ToString().ToCharArray();
    char underscore = char.Parse("_");


    if (userWord.Length == 1)
    {
        char userChar = char.Parse(userWord);

        if (SecretToArray.Contains(userChar))
        {
            // if userchar is correct , match with the char in the secretword array , reveal it
            Console.WriteLine("You just guessed one letter! ");
            for(int i = 0; i < secret.Length; i++)
            {
                if(secret[i] == userChar)
                {
                    HiddenSecret[i] = userChar;
                }
            }
            // if hiddensecret no longer contains _
            if (!HiddenSecret.Contains(underscore))
            {
                innerFlag = false;
                Console.WriteLine("Secret word is: ");
                Console.WriteLine(String.Join(" ", HiddenSecret));
                Console.WriteLine("\n");

                Console.WriteLine("You Win! You have guessed a secret word!! Congrats!!");

            }

            

        }
        else if (SecretToArray.Contains(userChar) == false)
        {
            // if userchar dont match with the char in the secretword array save it in string builder
            Console.WriteLine("Incorrect letter! ");
            userIncorrectLetters.AppendFormat("{0}, ", userWord);
            userGuesses++;
            

            // if user entered same letter before
            if (userLetters.Contains(userChar))
            {
                Console.WriteLine("You have entered that letter before.Try with new letter.");
            }
            
        }
       

    }
    else if (userWord.Length >= 2)
    {

        if (userWord == secret)
        {
            innerFlag = false;
            // Reveal Secret and end game
            for (int i = 0; i < secret.Length; i++)
            {
                    HiddenSecret[i] = secret[i];
            }
            Console.WriteLine("Secret word is: ");
            Console.WriteLine(String.Join(" ", HiddenSecret));
            Console.WriteLine("\n");

       
            Console.WriteLine("You Win! You have guessed a secret word!! Congrats!!");
            
        }
        else if (userWord != secret)
        {
            // when user word do not match the secret
            Console.WriteLine("Please keep guessing");
            userGuesses++;
            
        }
    }
    else
    {
        Console.WriteLine("Please enter something.");
    }

    Console.WriteLine("===========================================================\n");
}

string[] SecretWords()
{
    string[] secretWords = { "Batman", "Superman", "Catwoman", "Spiderman", "Hugo", "Hulk", "Thor", "Frankenstein", "Transformer", "Deadpool" };
    return secretWords;
}
int userChances(int userChancesLeft, int maxGuesses)
{
    return maxGuesses - userChancesLeft;
}




