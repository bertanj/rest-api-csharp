namespace RestWithASPNET.Data.Dto.V1
{

    using System;
    using System.Text.Json.Serialization; 
    using System.Xml.Serialization;

    
    public class PersonDto
    {

        [JsonPropertyOrder(1)]
        public long Id { get; set; }


        [JsonPropertyOrder(2)]
        [JsonPropertyName("first_name")]
        [XmlElement("first_name")] 
        public string FirstName { get; set; }


        [JsonPropertyOrder(3)]
        [JsonPropertyName("last_name")]
        [XmlElement("last_name")]
        public string LastName { get; set; }

        [JsonPropertyOrder(4)]
        public string Gender { get; set; }
       
        [JsonPropertyOrder(5)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string PhoneNumber { get; set; }

        [JsonPropertyOrder(6)]
        public DateTime BirthDate { get; set; }

        [JsonPropertyOrder(7)]
        public string Address { get; set; }


        [JsonPropertyOrder(8)]
        [JsonIgnore] 
        public string SensitiveData { get; set; }

    
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            PersonDto other = (PersonDto)obj;
            return Id == other.Id &&
                    FirstName == other.FirstName &&
                    LastName == other.LastName &&
                    PhoneNumber == other.PhoneNumber &&
                    BirthDate == other.BirthDate &&
                    Address == other.Address &&
                    Gender == other.Gender;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, PhoneNumber, BirthDate, Address, Gender);
        }
    }
}

