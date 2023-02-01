using System.Collections.Generic;

namespace PlayerDatabase
{
    public class PlayerDatabase
    {
        private Dictionary<uint, Player> _idToPlayer;

        public PlayerDatabase(List<Player> players)
        {
            _idToPlayer = new Dictionary<uint, Player>();

            foreach (var player in players)
                _idToPlayer.Add(player.Id, player);;
        }

        public PlayerDatabase() => _idToPlayer = new Dictionary<uint, Player>();

        public bool Add(Player player)
        {
            if (_idToPlayer.ContainsKey(player.Id))
                return false;

            _idToPlayer.Add(player.Id, player);
            return true;
        }

        public bool Remove(uint id)
        {
            if (_idToPlayer.ContainsKey(id))
            {
                _idToPlayer.Remove(id);
                return true;
            }

            return false;
        }

        public bool Ban(uint id)
        {
            if (_idToPlayer.ContainsKey(id))
            {
                _idToPlayer[id].Ban();
                return true;
            }

            return false;
        }

        public bool UnBan(uint id)
        {
            if (_idToPlayer.ContainsKey(id))
            {
                _idToPlayer[id].UnBan();
                return true;
            }

            return false;
        }

        public void ShowInformation()
        {
            foreach (var item in _idToPlayer)
                item.Value.ShowInformation();
        }
    }
}
