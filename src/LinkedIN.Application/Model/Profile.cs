using LinkedIN.Model.People;
namespace LinkedIN.Application.Model
{
    public class Profile
    {
        public Profile(Person l_profile)
        {
            this.person = l_profile;
        }
        public Person person { get; set; }
        public int firstDegreeConnections { get; set; }
        public int secondDegreeConnections { get; set; }
        public int[] likes { get; set; }
        public int[] comments { get; set; }
        public int[] hits { get; set; }

    }
}