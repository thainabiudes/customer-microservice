using Customers.API.MessageBus;

namespace Customers.API.Messages
{
    public class MessageVO : BaseMessage
    {
      
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string BodyEmail { get; set; }
    }
}
