using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NotebookApp
{
    class Notebook
    {
        private static Note _EditNote(Note note)
        {
            while (true)
            {
                Console.WriteLine("What would you like to add/change? (0/[default] - skip");
                Console.WriteLine($"1. Last name\n" +
                                  $"2. First name\n" +
                                  $"3. Middle name\n" +
                                  $"4. Phone number\n" +
                                  $"5. Country\n" +
                                  $"6. Birthday\n" +
                                  $"7. Organization" +
                                  $"8. Position\n" +
                                  $"9. Additional notes");
                string input = Console.ReadLine();
                if (input == "0" || string.IsNullOrEmpty(input))
                {
                    break;
                }

                string str;
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter new last name:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.LastName = str;
                            break;
                        }

                        Console.WriteLine("Field can't be empty");
                        break;
                    
                    case "2":
                        Console.WriteLine("Enter new first name:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.FirstName = str;
                            break;
                        }

                        Console.WriteLine("Field can't be empty");
                        break;
                    
                    case "3":
                        Console.WriteLine("Enter new middle name:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.MiddleName = str;
                        }
                        else
                        {
                            note.MiddleName = null;
                        }
                        break;
                    
                    case "4":
                        str = Console.ReadLine();
                        if (_IsDigit(str))
                        {
                            note.PhoneNumber = str;
                            break;
                        }
            
                        Console.WriteLine("Can't be empty and should be a number");
                        
                        break;
                    
                    case "5":
                        Console.WriteLine("Enter new country:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.Country = str;
                            break;
                        }

                        Console.WriteLine("Field can't be empty");
                        break;
                    
                    case "6":
                        Console.WriteLine("Enter new birthday date (Preferably with \"/\" as a delimiter):");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.Birthday = str;
                        }
                        else
                        {
                            note.Birthday = null;
                        }

                        break;
                    
                    case "7":
                        Console.WriteLine("Enter new organization name:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.Organization = str;
                        }
                        else
                        {
                            note.Organization = null;
                        }
                        break;
                    
                    case "8":
                        Console.WriteLine("Enter new position:");
                        str = Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                        {
                            note.Position = str;
                        }
                        else
                        {
                            note.Position = null;
                        }
                        break;
                    
                    case "9":
                        Console.WriteLine("Enter new additional notes (type \"quit\" to quit): ");
                        string msg = "";
                        while (true)
                        {
                            str = Console.ReadLine();
                            if (str == "quit")
                            {
                                break;
                            }

                            msg += str;
                            msg += "\n";
                        }

                        if (!string.IsNullOrWhiteSpace(msg))
                        {
                            note.AdditionalInfo = msg;
                        }
                        else
                        {
                            note.AdditionalInfo = null;
                        }
                        
                        break;
                    
                    default:
                        Console.WriteLine("Unknown command, please try again");
                        break;
                    
                }
                
            }

            return note;
        }

        private static bool _IsDigit(string str)
        {
            Regex rgx = new Regex(@"^\d+$");
            str = str.Trim();
            if (!string.IsNullOrEmpty(str) && rgx.IsMatch(str.TrimStart('+')))
            {
                return true;
            }
            return false;
        }

        public static Dictionary<int, Note> book = new Dictionary<int, Note>();

        public static void PrintMenu()
        {
            Console.WriteLine($"\t[ Menu ]\n" +
                              $"1. Add new contact\n" +
                              $"2. Edit a contact\n" +
                              $"3. Delete a contact\n" +
                              $"4. View certain contact\n" +
                              $"5. Show contact list with main info\n" +
                              $"quit. Quit the program");
        }

        public static void CreateNewNote()
        {
            Note note = new Note();
            Console.WriteLine("Please enter last name:");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    note.LastName = input;
                    break;
                }

                Console.WriteLine("Can't be empty, try again");
            }
            
            Console.WriteLine("Please enter first name:");
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    note.FirstName = input;
                    break;
                }

                Console.WriteLine("Can't be empty, try again");
            }
            
            Console.WriteLine("Add middle name? ([enter] - skip)");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                note.MiddleName = input;
            }
            
            Console.WriteLine("Please enter phon number (You can use \"+\" at the start if needed):");
            while (true)
            {
                input = Console.ReadLine();
                if (_IsDigit(input))
                {
                    note.PhoneNumber = input;
                    break;
                }
                
                Console.WriteLine("Can't be empty and should be a number, try again");
            }

            Console.WriteLine("Please enter the country name:");
            while (true)
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    note.Country = input;
                    break;
                }

                Console.WriteLine("Can't be empty, try again");
            }

            note = _EditNote(note);
            
            book.Add(note.Id, note);
        }

        public static void EditNote()
        {
            Console.WriteLine("Enter a number of contact to edit:");
            string prekey = Console.ReadLine();
            if (int.TryParse(prekey, out int key))
            {
                if (book.ContainsKey(key))
                {
                    book[key] = _EditNote(book[key]);
                    return;
                }    
            }
            
            Console.WriteLine("You should enter a number of contact, try again");
        }

        public static void DeleteNote()
        {
            Console.WriteLine("Enter a number of contact to delete:");
            string prekey = Console.ReadLine();
            if (int.TryParse(prekey, out int key))
            {
                if (book.ContainsKey(key))
                {
                    book.Remove(key);
                    return;
                }    
            }
            
            Console.WriteLine("You should enter a number of contact, try again");
        }

        public static void ShowNote()
        {
            Console.WriteLine("Enter a number of contact to show:");
            string prekey = Console.ReadLine();
            if (int.TryParse(prekey, out int key))
            {
                if (book.ContainsKey(key))
                {
                    Console.WriteLine(book[key]);
                    return;
                }    
            }
            
            Console.WriteLine("You should enter a number of contact, try again");
        }

        public static void ShowAllNotes()
        {
            Console.WriteLine("Here's a list of all contacts:");
            foreach (var note in book)
            {
                Console.WriteLine($"{note.Key})\n" +
                                  $"\tLast name: {note.Value.LastName}\n" +
                                  $"\tFirst name: {note.Value.FirstName}\n" +
                                  $"\tPhone number: {note.Value.PhoneNumber}");
            }
        }
        
        
        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                string input = Console.ReadLine();
                if (input == "quit")
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                switch (input)
                {
                    case "1":
                        CreateNewNote();
                        break;
                    
                    case "2":
                        EditNote();
                        break;
                    
                    case "3":
                        DeleteNote();
                        break;
                    
                    case "4":
                        ShowNote();
                        break;
                    
                    case "5":
                        ShowAllNotes();
                        break;
                    
                    default:
                        Console.WriteLine("Unknown command, please try again");
                        break;
                }
            }
        }
    }

    class Note
    {
        private static int _Entries = 1;
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; } //Not mandatory
        public string PhoneNumber { get; set; } //Check for digits
        public string Country { get; set; }
        public string Birthday { get; set; } //Not mandatory;
        public string Organization { get; set; } //Not mandatory
        public string Position { get; set; } //Not mandatory
        public string AdditionalInfo { get; set; } //Not mandatory

        public Note()
        {
            Id = _Entries;
            _Entries++;
        }
        
        public override string ToString()
        {
            string str = $"{Id})\n" + 
                         $"\tLast name: {LastName}\n" +
                         $"\tFirst name: {FirstName}\n";
            if (!string.IsNullOrEmpty(MiddleName))
            {
                str += $"\tMiddle name: {MiddleName}\n";
            }

            str += $"\tPhone number: {PhoneNumber}\n" +
                   $"\tCountry: {Country}\n";
            
            if (!string.IsNullOrEmpty(Birthday)) 
            {
                str += $"\tBirthday: {Birthday}\n";
            }

            if (!string.IsNullOrEmpty(Organization))
            {
                str += $"\tOrganization: {Organization}\n";
            }

            if (!string.IsNullOrEmpty(Position))
            {
                str += $"\tPosition: {Position}\n";
            }

            if (!string.IsNullOrEmpty(AdditionalInfo))
            {
                str += $"\tAdditional notes: {AdditionalInfo}";
            }
            
            return str;
        }
    }
}
