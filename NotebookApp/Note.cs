namespace NotebookApp
{
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