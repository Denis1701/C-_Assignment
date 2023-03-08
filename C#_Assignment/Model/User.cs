using System.Text.Json;
using System.Text.Json.Serialization;

namespace C__Assignment.Model
{
    public class User
    {
        public ICollection<string>? Name { get; set; }
        public ICollection<string>? Gender { get; set; }
        public ICollection<string>? Location { get; set; }
        public ICollection<string>? Email { get; set; }
        public ICollection<string>? Login { get; set; }
        public ICollection<string>? Registered { get; set; }
        public ICollection<string>? Dob { get; set; }
        public ICollection<string>? Phone { get; set; }
        public ICollection<string>? Cell { get; set; }
        public ICollection<string>? Id { get; set; }
        public ICollection<string>? Picture { get; set; }
        public ICollection<string>? Nat { get; set; }
        public override string ToString() => JsonSerializer.Serialize<User>(this);
    }
}
