using System;
using CodeBase.UI.CharacterSelect;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;

namespace CodeBase.Data
{
    [Serializable]
    public class ProgressData
    {
        public SettingsData SettingsData = new();
        public PlayerData PlayerData = new();
    }

    [Serializable]
    public class SettingsData
    {
        public bool IsSoundEnabled = true;
    }
    
    [Serializable]
    public class PlayerData
    {
        public CharacterData LastSelectedCharacter = new()
        {
            TypeId = CharacterTypeId.FirstHero,
        };

        public int LastSelectedCharacterIndex;
    }
}