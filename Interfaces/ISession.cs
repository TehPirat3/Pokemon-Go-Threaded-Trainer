using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI;

namespace Pokemon_Go_Threaded_Trainer.Interfaces
{
    public interface ISession
    {
        ISettings Settings { get; set; }
        //Inventory Inventory { get; }
        Client Client { get; }
        GetPlayerResponse Profile { get; set; }
        ILogicSettings LogicSettings { get; }
        IEventDispatcher EventDispatcher { get; }
    }


    public class Session : ISession
    {
        public Session(ISettings settings, ILogicSettings logicSettings)
        {
            Settings = settings;
            LogicSettings = logicSettings;
            EventDispatcher = new EventDispatcher();
            //Reset(settings, LogicSettings);
        }

        public ISettings Settings { get; set; }

        //public Inventory Inventory { get; private set; }

        public Client Client { get; private set; }

        public GetPlayerResponse Profile { get; set; }

        public ILogicSettings LogicSettings { get; set; }

        public IEventDispatcher EventDispatcher { get; }

        /*public void Reset(ISettings settings, ILogicSettings logicSettings)
        {
            var _apiStrategy = new ApiFailureStrategy(this);
            Client = new Client(Settings, _apiStrategy);
            // ferox wants us to set this manually
            Inventory = new Inventory(Client, logicSettings);
        }*/
    }
}
