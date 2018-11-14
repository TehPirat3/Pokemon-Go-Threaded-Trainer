using Pokemon_Go_Threaded_Trainer.Interfaces;
using PokemonGo.RocketAPI.Enums;
using System;

namespace Pokemon_Go_Threaded_Trainer.Classes
{
    public class Login : ILogin
    {
        private readonly ISession _session;

        public Login(ISession session)
        {
            _session = session;
        }

        public async void DoLogin()
        {
            try
            {
                if (_session.Settings.AuthType != AuthType.Ptc)
                    await _session.Client.Login.DoLogin();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
