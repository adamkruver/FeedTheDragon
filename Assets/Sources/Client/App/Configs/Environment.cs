using System;
using System.Collections.Generic;

namespace Sources.Client.App.Configs
{
    [Serializable]
    public class Environment
    {
        public Dictionary<string, string> View;
        public Dictionary<string, string> UI;
        public Dictionary<string, string> Fishes;
        public Dictionary<string, string> AudioPlayers;
        public AudioClipsConfig AudioClips;
    }
}